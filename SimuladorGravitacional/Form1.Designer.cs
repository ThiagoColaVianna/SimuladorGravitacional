namespace SimuladorGravitacional
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            groupBox1 = new GroupBox();
            label5 = new Label();
            label2 = new Label();
            abrir_doc = new Button();
            Parar_bt = new Button();
            Iniciar_bt = new Button();
            VelY_Box = new TextBox();
            label11 = new Label();
            label10 = new Label();
            VelX_Box = new TextBox();
            label8 = new Label();
            PosY_Box = new TextBox();
            label7 = new Label();
            PosX_Box = new TextBox();
            TempoIteracao_Box = new TextBox();
            label4 = new Label();
            Iteracoes_Box = new TextBox();
            label3 = new Label();
            Corpos_Box = new TextBox();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(abrir_doc);
            groupBox1.Controls.Add(Parar_bt);
            groupBox1.Controls.Add(Iniciar_bt);
            groupBox1.Controls.Add(VelY_Box);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(VelX_Box);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(PosY_Box);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(PosX_Box);
            groupBox1.Controls.Add(TempoIteracao_Box);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(Iteracoes_Box);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(Corpos_Box);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Right;
            groupBox1.Location = new Point(583, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(225, 645);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(69, 134);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 26;
            label5.Text = "INFORMAÇÕES";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 19);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 25;
            label2.Text = "FUNCIONALIDADES";
            // 
            // abrir_doc
            // 
            abrir_doc.Location = new Point(56, 80);
            abrir_doc.Name = "abrir_doc";
            abrir_doc.Size = new Size(111, 24);
            abrir_doc.TabIndex = 24;
            abrir_doc.Text = "Carregar Arquivos";
            abrir_doc.UseVisualStyleBackColor = true;
            abrir_doc.Click += button1_Click;
            // 
            // Parar_bt
            // 
            Parar_bt.Location = new Point(138, 51);
            Parar_bt.Name = "Parar_bt";
            Parar_bt.Size = new Size(75, 23);
            Parar_bt.TabIndex = 23;
            Parar_bt.Text = "Parar";
            Parar_bt.UseVisualStyleBackColor = true;
            Parar_bt.Click += Parar_bt_Click;
            // 
            // Iniciar_bt
            // 
            Iniciar_bt.Location = new Point(6, 51);
            Iniciar_bt.Name = "Iniciar_bt";
            Iniciar_bt.Size = new Size(75, 23);
            Iniciar_bt.TabIndex = 22;
            Iniciar_bt.Text = "Iniciar";
            Iniciar_bt.UseVisualStyleBackColor = true;
            Iniciar_bt.Click += Iniciar_bt_Click;
            // 
            // VelY_Box
            // 
            VelY_Box.Location = new Point(138, 295);
            VelY_Box.Name = "VelY_Box";
            VelY_Box.ReadOnly = true;
            VelY_Box.Size = new Size(49, 23);
            VelY_Box.TabIndex = 21;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(138, 277);
            label11.Name = "label11";
            label11.Size = new Size(29, 15);
            label11.TabIndex = 20;
            label11.Text = "VelY";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(138, 224);
            label10.Name = "label10";
            label10.Size = new Size(29, 15);
            label10.TabIndex = 19;
            label10.Text = "VelX";
            // 
            // VelX_Box
            // 
            VelX_Box.Location = new Point(138, 242);
            VelX_Box.Name = "VelX_Box";
            VelX_Box.ReadOnly = true;
            VelX_Box.Size = new Size(49, 23);
            VelX_Box.TabIndex = 18;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 338);
            label8.Name = "label8";
            label8.Size = new Size(33, 15);
            label8.TabIndex = 15;
            label8.Text = "PosY";
            // 
            // PosY_Box
            // 
            PosY_Box.Location = new Point(10, 356);
            PosY_Box.Name = "PosY_Box";
            PosY_Box.ReadOnly = true;
            PosY_Box.Size = new Size(49, 23);
            PosY_Box.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 277);
            label7.Name = "label7";
            label7.Size = new Size(33, 15);
            label7.TabIndex = 13;
            label7.Text = "PosX";
            // 
            // PosX_Box
            // 
            PosX_Box.Location = new Point(10, 295);
            PosX_Box.Name = "PosX_Box";
            PosX_Box.ReadOnly = true;
            PosX_Box.Size = new Size(49, 23);
            PosX_Box.TabIndex = 12;
            // 
            // TempoIteracao_Box
            // 
            TempoIteracao_Box.Location = new Point(138, 184);
            TempoIteracao_Box.Name = "TempoIteracao_Box";
            TempoIteracao_Box.ReadOnly = true;
            TempoIteracao_Box.Size = new Size(49, 23);
            TempoIteracao_Box.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(102, 166);
            label4.Name = "label4";
            label4.Size = new Size(123, 15);
            label4.TabIndex = 6;
            label4.Text = "Tempo entre Iterações";
            // 
            // Iteracoes_Box
            // 
            Iteracoes_Box.Location = new Point(10, 242);
            Iteracoes_Box.Name = "Iteracoes_Box";
            Iteracoes_Box.ReadOnly = true;
            Iteracoes_Box.Size = new Size(49, 23);
            Iteracoes_Box.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 224);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 4;
            label3.Text = "Iterações";
            // 
            // Corpos_Box
            // 
            Corpos_Box.Location = new Point(10, 184);
            Corpos_Box.Name = "Corpos_Box";
            Corpos_Box.Size = new Size(49, 23);
            Corpos_Box.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 166);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 0;
            label1.Text = "Corpos";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaptionText;
            ClientSize = new Size(808, 645);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "SimuladorGravitacional";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private GroupBox groupBox1;
        private TextBox Corpos_Box;
        private Label label1;
        private TextBox TempoIteracao_Box;
        private Label label4;
        private TextBox Iteracoes_Box;
        private Label label3;
        private Label label8;
        private TextBox PosY_Box;
        private Label label7;
        private TextBox PosX_Box;
        private TextBox VelY_Box;
        private Label label11;
        private Label label10;
        private TextBox VelX_Box;
        private Button Iniciar_bt;
        private Button Parar_bt;
        private Button abrir_doc;
        private Label label5;
        private Label label2;
    }
}
