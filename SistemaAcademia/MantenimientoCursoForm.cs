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
    public partial class MantenimientoCursoForm : Form
    {
        public MantenimientoCursoForm()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var crearForm = new CrearCursoForm())
            {
                crearForm.ShowDialog();
            }
        }

        private void BtnRetroceso_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
