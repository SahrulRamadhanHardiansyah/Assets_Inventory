using Assets_Inventory.Models;
using Newtonsoft.Json;
using System;

namespace Assets_Inventory.Helper
{
    public static class AuditHelper
    {
        public static string GetCurrentUsername()
        {
            return AuthManager.CurrentUsername ?? "System";
        }

        public static string SerializeObject(object obj)
        {
            if (obj == null) return null;
            try
            {
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = 3
                });
            }
            catch { return null; }
        }

        // Spec exact signature
        public static void Log(string table, string recordPK, string action, string oldJson, string newJson, string modul)
        {
            Log(table, recordPK, action, (object)oldJson, (object)newJson, modul, null);
        }

        // Convenience overload for object params (Sprint 4 UI)
        public static void Log(string table, string recordPK, string action, object oldObj, object newObj, string modul, string description = null)
        {
            try
            {
                string oldJson = oldObj == null ? null : (oldObj is string s1 ? s1 : SerializeObject(oldObj));
                string newJson = newObj == null ? null : (newObj is string s2 ? s2 : SerializeObject(newObj));
                var username = GetCurrentUsername();
                var userId = AuthManager.CurrentUserId > 0 ? (int?)AuthManager.CurrentUserId : null;
                using (var db = new AppDbContext())
                {
                    db.AuditLog.Add(new AuditLog
                    {
                        TableName = table,
                        RecordPK = recordPK,
                        Action = action,
                        OldJson = oldJson,
                        NewJson = newJson,
                        Modul = modul,
                        IdPengguna = userId,
                        Username = username,
                        Timestamp = DateTime.Now,
                        Description = description
                    });
                    db.SaveChanges();
                }
            }
            catch { }
        }
    }
}
