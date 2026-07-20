using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets_Inventory.Helper
{
    public static class NotificationService
    {
        public static void CheckOverduePeminjaman()
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                using (var db = new AppDbContext())
                {
                    // DB-level filter when possible, in-memory only for Date diff
                    var pending = db.Peminjaman.Where(p => p.StatusPeminjaman == AppConstants.PeminjamanSedangDipinjam).ToList()
                        .Where(p => (today - p.TanggalPinjam.Date).TotalDays > p.LamaPinjamHari).ToList();
                    foreach (var p in pending)
                    {
                        if (db.Notifikasi.Any(n => n.Type == "JatuhTempo" && n.RefId == p.NomorPeminjaman && n.CreatedAt >= today && n.CreatedAt < tomorrow)) continue;
                        db.Notifikasi.Add(new Notifikasi
                        {
                            Type = "JatuhTempo",
                            Title = "Peminjaman Jatuh Tempo",
                            Message = $"Peminjaman {p.NomorPeminjaman} oleh {p.NamaPeminjam} sudah lewat {p.LamaPinjamHari} hari.",
                            RefTable = "peminjaman",
                            RefId = p.NomorPeminjaman,
                            IsRead = false,
                            CreatedAt = DateTime.Now
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch { }
        }

        public static void CheckStokMinimal(int threshold = 5)
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                using (var db = new AppDbContext())
                {
                    var low = db.AsetHabisPakai.Where(a => a.StokAktual <= threshold || a.Stok <= threshold).ToList();
                    foreach (var a in low)
                    {
                        if (db.Notifikasi.Any(n => n.Type == "StokMinimal" && n.RefId == a.KodeBarang && n.CreatedAt >= today && n.CreatedAt < tomorrow)) continue;
                        db.Notifikasi.Add(new Notifikasi
                        {
                            Type = "StokMinimal",
                            Title = "Stok Minimal",
                            Message = $"Stok barang {a.KodeBarang} tinggal {a.StokAktual}/{a.Stok}.",
                            RefTable = "aset_habis_pakai",
                            RefId = a.KodeBarang,
                            IsRead = false,
                            CreatedAt = DateTime.Now
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch { }
        }

        public static int GetUnreadCount() { try { using (var db = new AppDbContext()) return db.Notifikasi.Count(n => !n.IsRead); } catch { return 0; } }

        public static void MarkAsRead(int id) { try { using (var db = new AppDbContext()) { var n = db.Notifikasi.Find(id); if (n == null) return; n.IsRead = true; db.SaveChanges(); } } catch { } }

        public static void MarkAllAsRead() { try { using (var db = new AppDbContext()) { var all = db.Notifikasi.Where(n => !n.IsRead).ToList(); foreach (var n in all) n.IsRead = true; db.SaveChanges(); } } catch { } }

        public static List<Notifikasi> GetAllUnread(int take = 50)
        {
            try { using (var db = new AppDbContext()) return db.Notifikasi.Where(n => !n.IsRead).OrderByDescending(n => n.CreatedAt).Take(take).ToList(); }
            catch { return new List<Notifikasi>(); }
        }

        // Legacy overload kept for old call sites
        public static List<Notifikasi> GetAllUnread() => GetAllUnread(50);
    }
}
