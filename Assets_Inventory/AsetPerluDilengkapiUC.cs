using Assets_Inventory.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; 
using System.Linq;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class AsetPerluDilengkapiUC : UserControl
    {
        private AppDbContext db = new AppDbContext();

        public AsetPerluDilengkapiUC()
        {
            InitializeComponent();
        }

        private void AsetPerluDilengkapiUC_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var asetBelumLengkap = db.Aset
                    .Include(a => a.IdMasterBarangNavigation)
                    .Include(a => a.IdJurusanNavigation)
                    .Where(a =>
                        string.IsNullOrEmpty(a.NoSeri) ||      
                        a.IdRuang == null ||                  
                        a.IdLokasi == null ||                 
                        string.IsNullOrEmpty(a.Gambar)        
                    )
                    .Select(a => new
                    {
                        a.KodeInventaris,
                        NamaBarang = a.IdMasterBarangNavigation.NamaBarang,
                        a.Status,
                        Jurusan = a.IdJurusanNavigation != null ? a.IdJurusanNavigation.NamaJurusan : "Gudang",
                        KekuranganData = GetKekurangan(a)
                    })
                    .ToList();

                dg.DataSource = asetBelumLengkap;

                if (dg.Columns.Count > 0)
                {
                    dg.Columns["KodeInventaris"].HeaderText = "Kode Inventaris";
                    dg.Columns["NamaBarang"].HeaderText = "Nama Barang";
                    dg.Columns["KekuranganData"].HeaderText = "Data Kurang";
                }

                lblTotal.Text = $"Total Aset Perlu Dilengkapi: {asetBelumLengkap.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetKekurangan(Aset a)
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(a.NoSeri)) list.Add("No Seri");
            if (a.IdRuang == null) list.Add("Ruang");
            if (a.IdLokasi == null) list.Add("Lokasi");
            if (string.IsNullOrEmpty(a.Gambar)) list.Add("Gambar");
            return string.Join(", ", list);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dg.CurrentRow != null)
            {
                var kodeInv = dg.CurrentRow.Cells["KodeInventaris"].Value.ToString();

                var aset = db.Aset
                    .Include(a => a.IdMasterBarangNavigation)
                    .FirstOrDefault(a => a.KodeInventaris == kodeInv);

                if (aset != null)
                {
                    List<Aset> asetList = new List<Aset> { aset };

                    KelengkapanAsetForm formLengkap = new KelengkapanAsetForm(asetList);
                    MainForm parentForm = this.ParentForm as MainForm;

                    if (formLengkap.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        if (parentForm != null) parentForm.CekNotifikasiAset();
                    }
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih data aset dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}