using System;
using System.Windows.Forms;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class ImportOptionsForm : Form
    {
        public bool UpdateStock { get; private set; }
        public bool ReplaceStock { get; private set; }

        public ImportOptionsForm()
        {
            InitializeComponent();
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            UpdateStock = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnReplaceStock_Click(object sender, EventArgs e)
        {
            ReplaceStock = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

