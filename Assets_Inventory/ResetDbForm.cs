using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ComponentFactory.Krypton.Toolkit;

namespace Assets_Inventory
{
    public partial class ResetDbForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public ResetDbForm()
        {
            InitializeComponent();
        }

        private void ResetDbForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
