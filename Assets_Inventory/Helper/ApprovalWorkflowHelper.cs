using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets_Inventory.Helper
{
    public static class ApprovalWorkflowHelper
    {
        // ponytail: static helper avoids duplication between PersetujuanForm and ApprovalStepForm
        // upgrade path: move to service with DI when needed

        public static List<ApprovalWorkflowConfig> GetActiveConfigs(AppDbContext db, string workflowType)
        {
            if (db == null || string.IsNullOrEmpty(workflowType)) return new List<ApprovalWorkflowConfig>();
            return db.ApprovalWorkflowConfig.Where(c => c.WorkflowType == workflowType && c.IsActive).OrderBy(c => c.Level).ToList();
        }

        public static bool HasConfig(AppDbContext db, string workflowType)
        {
            if (db == null) return false;
            return db.ApprovalWorkflowConfig.Any(c => c.WorkflowType == workflowType && c.IsActive);
        }

        public static void EnsureSteps(AppDbContext db, string workflowType, string refId)
        {
            if (string.IsNullOrEmpty(refId)) return;
            var configs = GetActiveConfigs(db, workflowType);
            if (!configs.Any() && workflowType == "PermintaanHp")
                configs = GetActiveConfigs(db, "Permintaan"); // fallback

            if (!configs.Any()) return;

            var existingLevels = db.ApprovalStep.Where(s => s.WorkflowType == workflowType && s.RefId == refId).Select(s => s.Level).ToList();
            // also check fallback type for Hp
            if (workflowType == "PermintaanHp" && !existingLevels.Any())
            {
                var alt = db.ApprovalStep.Where(s => s.WorkflowType == "Permintaan" && s.RefId == refId).Select(s => s.Level).ToList();
                if (alt.Any()) return; // already created under Permintaan type
            }

            foreach (var cfg in configs)
            {
                if (existingLevels.Contains(cfg.Level)) continue;
                db.ApprovalStep.Add(new ApprovalStep
                {
                    WorkflowType = workflowType,
                    RefId = refId,
                    Level = cfg.Level,
                    IdPeranApprover = cfg.IdPeranApprover,
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                });
            }
            db.SaveChanges();
        }

        public static bool IsAllRequiredApproved(AppDbContext db, string workflowType, string refId)
        {
            var configs = GetActiveConfigs(db, workflowType);
            if (!configs.Any() && workflowType == "PermintaanHp")
                configs = GetActiveConfigs(db, "Permintaan");
            if (!configs.Any()) return true; // no config = considered approved

            var requiredLevels = configs.Where(c => c.IsRequired).Select(c => c.Level).ToList();
            if (!requiredLevels.Any()) requiredLevels = configs.Select(c => c.Level).ToList();

            var steps = db.ApprovalStep.Where(s => s.RefId == refId && (s.WorkflowType == workflowType || (workflowType == "PermintaanHp" && s.WorkflowType == "Permintaan"))).ToList();
            return requiredLevels.All(lvl => steps.Any(s => s.Level == lvl && s.Status == "Approved"));
        }

        public static bool HasRejectedRequired(AppDbContext db, string workflowType, string refId)
        {
            var configs = GetActiveConfigs(db, workflowType);
            if (!configs.Any() && workflowType == "PermintaanHp")
                configs = GetActiveConfigs(db, "Permintaan");
            var required = configs.Where(c => c.IsRequired).Select(c => c.Level).ToList();
            if (!required.Any()) required = configs.Select(c => c.Level).ToList();

            return db.ApprovalStep.Any(s => s.RefId == refId && required.Contains(s.Level) && s.Status == "Rejected");
        }
    }
}
