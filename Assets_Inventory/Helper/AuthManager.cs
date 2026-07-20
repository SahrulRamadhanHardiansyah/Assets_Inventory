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
        private static readonly object _sync = new object();
        private static Dictionary<string, ModulAkses> _hakAkses = new Dictionary<string, ModulAkses>(StringComparer.OrdinalIgnoreCase);

        public static int CurrentUserId { get; private set; }
        public static int CurrentRoleId { get; private set; }
        public static string CurrentUsername { get; private set; }
        public static string CurrentRole { get; private set; }
        public static DateTime LoginTime { get; private set; }
        private const int SessionTimeoutHours = 8;

        public static bool IsAuthenticated
        {
            get
            {
                lock (_sync)
                {
                    if (CurrentUserId == 0) return false;
                    if ((DateTime.Now - LoginTime).TotalHours >= SessionTimeoutHours) return false;
                    return true;
                }
            }
        }

        public static void SetUserSession(int userId, int roleId, string username)
        {
            lock (_sync)
            {
                CurrentUserId = userId;
                CurrentRoleId = roleId;
                CurrentUsername = username;
                LoginTime = DateTime.Now;
            }
            LoadPermissionsFromDatabase();
        }

        private static void LoadPermissionsFromDatabase()
        {
            lock (_sync)
            {
                _hakAkses.Clear();
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    var peran = db.Peran.Find(CurrentRoleId);
                    lock (_sync)
                    {
                        CurrentRole = peran != null ? peran.NamaPeran : "Unknown";
                    }

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

                    lock (_sync)
                    {
                        foreach (var item in aksesList)
                        {
                            if (!string.IsNullOrEmpty(item.NamaModul))
                            {
                                _hakAkses[item.NamaModul] = new ModulAkses
                                {
                                    HakBuat = item.HakBuat == true,
                                    HakBaca = item.HakBaca == true,
                                    HakUbah = item.HakUbah == true,
                                    HakHapus = item.HakHapus == true
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Do not expose raw exception to UI - log internally, show generic
                System.Diagnostics.Debug.WriteLine("Gagal memuat hak akses: " + ex.Message);
                // Use BeginInvoke to avoid cross-thread MessageBox if called from non-UI thread
                try
                {
                    MessageBox.Show("Gagal memuat hak akses. Silakan login ulang.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch { }
            }
        }

        public static ModulAkses GetAkses(string namaModul)
        {
            lock (_sync)
            {
                if (!IsAuthenticatedInternal())
                {
                    return new ModulAkses { HakBuat = false, HakBaca = false, HakUbah = false, HakHapus = false };
                }

                if (_hakAkses.TryGetValue(namaModul, out ModulAkses akses))
                {
                    // Return copy to prevent external mutation of internal dictionary
                    return new ModulAkses
                    {
                        HakBuat = akses.HakBuat,
                        HakBaca = akses.HakBaca,
                        HakUbah = akses.HakUbah,
                        HakHapus = akses.HakHapus
                    };
                }

                return new ModulAkses { HakBuat = false, HakBaca = false, HakUbah = false, HakHapus = false };
            }
        }

        // Internal without lock (callers already hold lock)
        private static bool IsAuthenticatedInternal()
        {
            if (CurrentUserId == 0) return false;
            if ((DateTime.Now - LoginTime).TotalHours >= SessionTimeoutHours) return false;
            return true;
        }

        // For preloading all permissions once per MainForm_Load
        public static Dictionary<string, ModulAkses> GetAllAkses()
        {
            lock (_sync)
            {
                return new Dictionary<string, ModulAkses>(_hakAkses, StringComparer.OrdinalIgnoreCase);
            }
        }

        public static void ClearSession()
        {
            lock (_sync)
            {
                CurrentUserId = 0;
                CurrentRoleId = 0;
                CurrentUsername = null;
                CurrentRole = null;
                LoginTime = DateTime.MinValue;
                _hakAkses.Clear();
            }
            try
            {
                Properties.Settings.Default.userId = 0;
                Properties.Settings.Default.Save();
            }
            catch { }
        }
    }
}
