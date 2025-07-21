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
    public partial class MantenimientoMateriaForm : Form
    {
        public MantenimientoMateriaForm()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            using (var borrarForm = new BorrarMateriaForm())
            {
                borrarForm.ShowDialog();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var crearForm = new CrearMateriaForm())
            {
                crearForm.ShowDialog();
            }
        }

        private void BtnRetroceso_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
