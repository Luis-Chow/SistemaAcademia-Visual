namespace SistemaAcademia
{
    partial class MantenimientoCursoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            BtnDelete = new Button();
            BtnAdd = new Button();
            button1 = new Button();
            button2 = new Button();
            Checkbox = new DataGridViewCheckBoxColumn();
            IDMateria = new DataGridViewTextBoxColumn();
            Materia = new DataGridViewTextBoxColumn();
            Seccion = new DataGridViewTextBoxColumn();
            Profesor = new DataGridViewTextBoxColumn();
            Periodo = new DataGridViewTextBoxColumn();
            Horario = new DataGridViewTextBoxColumn();
            Aula = new DataGridViewTextBoxColumn();
            Edit = new DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(251, 192);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(209, 23);
            textBox1.TabIndex = 42;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Checkbox, IDMateria, Materia, Seccion, Profesor, Periodo, Horario, Aula, Edit });
            dataGridView1.Location = new Point(251, 221);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(943, 410);
            dataGridView1.TabIndex = 38;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.Font = new Font("Segoe UI", 60F);
            label1.ForeColor = Color.FromArgb(170, 245, 219);
            label1.Location = new Point(194, 9);
            label1.Name = "label1";
            label1.Size = new Size(1000, 115);
            label1.TabIndex = 36;
            label1.Text = "Mantenimiento de Curso";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // BtnDelete
            // 
            BtnDelete.BackColor = Color.FromArgb(30, 30, 60);
            BtnDelete.BackgroundImage = Properties.Resources.Boton_de_Borrar;
            BtnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            BtnDelete.FlatAppearance.BorderSize = 0;
            BtnDelete.FlatStyle = FlatStyle.Flat;
            BtnDelete.Location = new Point(951, 127);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(123, 83);
            BtnDelete.TabIndex = 46;
            BtnDelete.UseVisualStyleBackColor = false;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // BtnAdd
            // 
            BtnAdd.BackColor = Color.FromArgb(30, 30, 60);
            BtnAdd.BackgroundImage = Properties.Resources.BotonAdd;
            BtnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            BtnAdd.FlatAppearance.BorderSize = 0;
            BtnAdd.FlatStyle = FlatStyle.Flat;
            BtnAdd.Location = new Point(1080, 122);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(114, 93);
            BtnAdd.TabIndex = 45;
            BtnAdd.UseVisualStyleBackColor = false;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(30, 30, 60);
            button1.BackgroundImage = Properties.Resources.Boton_de_Retroceso;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(12, 633);
            button1.Name = "button1";
            button1.Size = new Size(104, 84);
            button1.TabIndex = 44;
            button1.UseVisualStyleBackColor = false;
            button1.Click += BtnRetroceso_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(30, 30, 60);
            button2.BackgroundImage = Properties.Resources.Boton_de_Guardado;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(535, 638);
            button2.Name = "button2";
            button2.Size = new Size(355, 75);
            button2.TabIndex = 43;
            button2.UseVisualStyleBackColor = false;
            // 
            // Checkbox
            // 
            Checkbox.HeaderText = "";
            Checkbox.Name = "Checkbox";
            Checkbox.ReadOnly = true;
            // 
            // IDMateria
            // 
            IDMateria.HeaderText = "ID de Materia";
            IDMateria.Name = "IDMateria";
            IDMateria.ReadOnly = true;
            // 
            // Materia
            // 
            Materia.HeaderText = "Materia";
            Materia.Name = "Materia";
            Materia.ReadOnly = true;
            // 
            // Seccion
            // 
            Seccion.HeaderText = "Sección";
            Seccion.Name = "Seccion";
            Seccion.ReadOnly = true;
            // 
            // Profesor
            // 
            Profesor.HeaderText = "Profesor";
            Profesor.Name = "Profesor";
            Profesor.ReadOnly = true;
            // 
            // Periodo
            // 
            Periodo.HeaderText = "Periodo";
            Periodo.Name = "Periodo";
            Periodo.ReadOnly = true;
            // 
            // Horario
            // 
            Horario.HeaderText = "Horario";
            Horario.Name = "Horario";
            Horario.ReadOnly = true;
            // 
            // Aula
            // 
            Aula.HeaderText = "Aula";
            Aula.Name = "Aula";
            Aula.ReadOnly = true;
            // 
            // Edit
            // 
            Edit.HeaderText = "Edit";
            Edit.Image = Properties.Resources.BotonEditar;
            Edit.Name = "Edit";
            Edit.ReadOnly = true;
            // 
            // MantenimientoCursoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 60);
            ClientSize = new Size(1344, 729);
            Controls.Add(BtnDelete);
            Controls.Add(BtnAdd);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "MantenimientoCursoForm";
            Text = "MantenimientoCursoForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private DataGridView dataGridView1;
        private Label label1;
        private Button BtnDelete;
        private Button BtnAdd;
        private Button button1;
        private Button button2;
        private DataGridViewCheckBoxColumn Checkbox;
        private DataGridViewTextBoxColumn IDMateria;
        private DataGridViewTextBoxColumn Materia;
        private DataGridViewTextBoxColumn Seccion;
        private DataGridViewTextBoxColumn Profesor;
        private DataGridViewTextBoxColumn Periodo;
        private DataGridViewTextBoxColumn Horario;
        private DataGridViewTextBoxColumn Aula;
        private DataGridViewImageColumn Edit;
    }
}