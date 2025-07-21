namespace SistemaAcademia
{
    partial class MantenimientoSeccionForm
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
            Seccion = new DataGridViewTextBoxColumn();
            Cupo = new DataGridViewTextBoxColumn();
            Profesor = new DataGridViewTextBoxColumn();
            Edit = new DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(367, 203);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(209, 23);
            textBox1.TabIndex = 35;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Checkbox, IDMateria, Seccion, Cupo, Profesor, Edit });
            dataGridView1.Location = new Point(367, 244);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(642, 388);
            dataGridView1.TabIndex = 31;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.Font = new Font("Segoe UI", 60F);
            label1.ForeColor = Color.FromArgb(170, 245, 219);
            label1.Location = new Point(211, 27);
            label1.Name = "label1";
            label1.Size = new Size(1000, 115);
            label1.TabIndex = 29;
            label1.Text = "Mantenimiento de Sección";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // BtnDelete
            // 
            BtnDelete.BackColor = Color.FromArgb(30, 30, 60);
            BtnDelete.BackgroundImage = Properties.Resources.Boton_de_Borrar;
            BtnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            BtnDelete.FlatAppearance.BorderSize = 0;
            BtnDelete.FlatStyle = FlatStyle.Flat;
            BtnDelete.Location = new Point(766, 150);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(123, 83);
            BtnDelete.TabIndex = 39;
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
            BtnAdd.Location = new Point(895, 145);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(114, 93);
            BtnAdd.TabIndex = 38;
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
            button1.TabIndex = 37;
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
            button2.Location = new Point(517, 638);
            button2.Name = "button2";
            button2.Size = new Size(355, 75);
            button2.TabIndex = 36;
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
            // Seccion
            // 
            Seccion.HeaderText = "Sección";
            Seccion.Name = "Seccion";
            Seccion.ReadOnly = true;
            // 
            // Cupo
            // 
            Cupo.HeaderText = "HC";
            Cupo.Name = "Cupo";
            Cupo.ReadOnly = true;
            // 
            // Profesor
            // 
            Profesor.HeaderText = "Profesor";
            Profesor.Name = "Profesor";
            Profesor.ReadOnly = true;
            // 
            // Edit
            // 
            Edit.HeaderText = "Edit";
            Edit.Image = Properties.Resources.BotonEditar;
            Edit.Name = "Edit";
            Edit.ReadOnly = true;
            // 
            // MantenimientoSeccionForm
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
            Name = "MantenimientoSeccionForm";
            Text = "MantenimientoSeccionForm";
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
        private DataGridViewTextBoxColumn Seccion;
        private DataGridViewTextBoxColumn Cupo;
        private DataGridViewTextBoxColumn Profesor;
        private DataGridViewImageColumn Edit;
    }
}