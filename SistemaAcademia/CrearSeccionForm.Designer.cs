namespace SistemaAcademia
{
    partial class CrearSeccionForm
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
            comboBoxMateria = new ComboBox();
            BtnGuardado = new Button();
            label2 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            label5 = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // comboBoxMateria
            // 
            comboBoxMateria.FormattingEnabled = true;
            comboBoxMateria.Location = new Point(37, 408);
            comboBoxMateria.Name = "comboBoxMateria";
            comboBoxMateria.Size = new Size(337, 23);
            comboBoxMateria.TabIndex = 53;
            // 
            // BtnGuardado
            // 
            BtnGuardado.BackColor = Color.FromArgb(30, 30, 60);
            BtnGuardado.FlatAppearance.BorderSize = 0;
            BtnGuardado.FlatStyle = FlatStyle.Flat;
            BtnGuardado.Location = new Point(1, 828);
            BtnGuardado.Name = "BtnGuardado";
            BtnGuardado.Size = new Size(413, 150);
            BtnGuardado.TabIndex = 52;
            BtnGuardado.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.Font = new Font("Segoe UI", 35F);
            label2.ForeColor = Color.FromArgb(170, 245, 219);
            label2.Location = new Point(27, 334);
            label2.Name = "label2";
            label2.Size = new Size(357, 67);
            label2.TabIndex = 51;
            label2.Text = "Cupos:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(35, 202);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(340, 23);
            textBox3.TabIndex = 50;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.Font = new Font("Segoe UI", 35F);
            label4.ForeColor = Color.FromArgb(170, 245, 219);
            label4.Location = new Point(27, 134);
            label4.Name = "label4";
            label4.Size = new Size(357, 61);
            label4.TabIndex = 49;
            label4.Text = "ID:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(35, 304);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(340, 23);
            textBox1.TabIndex = 48;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.Font = new Font("Segoe UI", 35F);
            label3.ForeColor = Color.FromArgb(170, 245, 219);
            label3.Location = new Point(27, 232);
            label3.Name = "label3";
            label3.Size = new Size(357, 65);
            label3.TabIndex = 47;
            label3.Text = "Seccion:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.Font = new Font("Segoe UI", 60F);
            label5.ForeColor = Color.FromArgb(170, 245, 219);
            label5.Location = new Point(75, 7);
            label5.Name = "label5";
            label5.Size = new Size(258, 115);
            label5.TabIndex = 46;
            label5.Text = "Crear";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(37, 505);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(337, 23);
            comboBox1.TabIndex = 55;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.Font = new Font("Segoe UI", 35F);
            label1.ForeColor = Color.FromArgb(170, 245, 219);
            label1.Location = new Point(21, 438);
            label1.Name = "label1";
            label1.Size = new Size(369, 60);
            label1.TabIndex = 54;
            label1.Text = "Profesor:";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(30, 30, 60);
            button1.BackgroundImage = Properties.Resources.Boton_de_Guardado;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(18, 623);
            button1.Name = "button1";
            button1.Size = new Size(384, 94);
            button1.TabIndex = 56;
            button1.UseVisualStyleBackColor = false;
            // 
            // CrearSeccionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 60);
            ClientSize = new Size(414, 729);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(comboBoxMateria);
            Controls.Add(BtnGuardado);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label5);
            Name = "CrearSeccionForm";
            Text = "CrearSeccionForm";
            Load += CrearSeccionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxMateria;
        private Button BtnGuardado;
        private Label label2;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox1;
        private Label label3;
        private Label label5;
        private ComboBox comboBox1;
        private Label label1;
        private Button button1;
    }
}