using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.Helper
{
    public class ModulAkses
    {
        public bool HakBuat { get; set; }
        public bool HakBaca { get; set; }
        public bool HakUbah { get; set; }
        public bool HakHapus { get; set; }
    }

    public static class AuthManager
    {
        private static Dictionary<string, ModulAkses> _hakAkses = new Dictionary<string, ModulAkses>(StringComparer.OrdinalIgnoreCase);

        public static int CurrentUserId { get; private set; }
        public static int CurrentRoleId { get; private set; }
        public static string CurrentUsername { get; private set; }
        public static string CurrentRole { get; private set; }

        public static void SetUserSession(int userId, int roleId, string username)
        {
            CurrentUserId = userId;
            CurrentRoleId = roleId;
            CurrentUsername = username;

            LoadPermissionsFromDatabase();
        }

        private static void LoadPermissionsFromDatabase()
        {
            _hakAkses.Clear();

            try
            {
                using (var db = new AppDbContext())
                {
                    var peran = db.Peran.Find(CurrentRoleId);
                    CurrentRole = peran != null ? peran.NamaPeran : "Unknown";

                    var aksesList = (from pa in db.PeranAkses
                                     join a in db.Akses on pa.IdAkses equals a.IdAkses
                                     where pa.IdPeran == CurrentRoleId
                                     select new
                                     {
                                         NamaModul = a.NamaModul,
                                         HakBuat = pa.HakBuat,
                                         HakBaca = pa.HakBaca,
                                         HakUbah = pa.HakUbah,
                                         HakHapus = pa.HakHapus
                                     }).ToList();

                    foreach (var item in aksesList)
                    {
                        if (!string.IsNullOrEmpty(item.NamaModul))
                        {
                            _hakAkses[item.NamaModul] = new ModulAkses
                            {
                                HakBuat = Convert.ToInt32(item.HakBuat) == 1,
                                HakBaca = Convert.ToInt32(item.HakBaca) == 1,
                                HakUbah = Convert.ToInt32(item.HakUbah) == 1,
                                HakHapus = Convert.ToInt32(item.HakHapus) == 1
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat hak akses dari database: " + ex.Message, "Error Middleware", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static ModulAkses GetAkses(string namaModul)
        {
            if (_hakAkses.TryGetValue(namaModul, out ModulAkses akses))
            {
                return akses;
            }

            return new ModulAkses { HakBuat = false, HakBaca = false, HakUbah = false, HakHapus = false };
        }

        public static void ClearSession()
        {
            CurrentUserId = 0;
            CurrentRoleId = 0;
            CurrentUsername = null;
            CurrentRole = null;
            _hakAkses.Clear();
        }
    }
}