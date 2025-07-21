using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAcademia
{
    public partial class MantenimientoSeccionForm : Form
    {
        public MantenimientoSeccionForm()
        {
            InitializeComponent();
        }

        private void BtnRetroceso_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            using (var borrarForm = new BorrarSeccionForm())
            {
                borrarForm.ShowDialog();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var crearForm = new CrearSeccionForm())
            {
                crearForm.ShowDialog();
            }
        }
    }
}
