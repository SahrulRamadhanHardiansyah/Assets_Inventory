using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Inventory
{
    public partial class DbTableControl : UserControl
    {
        public int IdAkses { get; set; }

        public string NamaModul
        {
            get { return cbModul.Text; }
            set { cbModul.Text = value; }
        }

        public bool IsChecked
        {
            get { return cbModul.Checked; }
            set { cbModul.Checked = value; }
        }

        public DbTableControl()
        {
            InitializeComponent();
        }

        private void AksesItemControl_Load(object sender, EventArgs e)
        {

        }
    }
}
