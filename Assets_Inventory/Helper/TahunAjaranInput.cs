using Assets_Inventory.Models;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory.Helper
{
    // Sumber tunggal input Tahun Ajaran berbasis relasi (tabel TahunAjaran), bukan teks bebas.
    public static class TahunAjaranInput
    {
        public static string Label(TahunAjaran ta) => $"{ta.TahunAjaran1} - Semester {ta.Semester}";

        // Isi ComboBox DropDownList dari tabel TahunAjaran. SelectedValue = IdTahunAjaran (int).
        // defaultSelectedId != null -> pilih item itu; kalau tidak ada, fallback ke yang IsActive.
        public static void Fill(AppDbContext db, ComboBox cmb, int? defaultSelectedId = null)
        {
            var list = db.TahunAjaran.ToList()
                .Select(ta => new { Id = ta.IdTahunAjaran, Label = Label(ta), ta.IsActive })
                .ToList();

            cmb.DataSource = list;
            cmb.DisplayMember = "Label";
            cmb.ValueMember = "Id";

            if (defaultSelectedId != null)
            {
                var match = list.FirstOrDefault(x => x.Id == defaultSelectedId.Value);
                if (match != null) { cmb.SelectedItem = match; return; }
            }

            var aktif = list.FirstOrDefault(x => x.IsActive == true);
            cmb.SelectedIndex = aktif != null ? list.IndexOf(aktif) : -1;
        }
    }
}
