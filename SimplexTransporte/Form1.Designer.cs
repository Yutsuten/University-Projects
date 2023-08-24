namespace SimplexTransporte
{
    partial class Form1
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTransporte = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btCalc = new System.Windows.Forms.Button();
            this.lbDestino = new System.Windows.Forms.Label();
            this.lbOferta = new System.Windows.Forms.Label();
            this.cbDestino = new System.Windows.Forms.ComboBox();
            this.cbOferta = new System.Windows.Forms.ComboBox();
            this.dgTransp = new System.Windows.Forms.DataGridView();
            this.tabTransbordo = new System.Windows.Forms.TabPage();
            this.tabAlocacao = new System.Windows.Forms.TabPage();
            this.btCalc3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTar = new System.Windows.Forms.ComboBox();
            this.cbServ = new System.Windows.Forms.ComboBox();
            this.dgAloc = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabTransporte.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransp)).BeginInit();
            this.tabTransbordo.SuspendLayout();
            this.tabAlocacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAloc)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabTransporte);
            this.tabControl.Controls.Add(this.tabTransbordo);
            this.tabControl.Controls.Add(this.tabAlocacao);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(859, 405);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // tabTransporte
            // 
            this.tabTransporte.Controls.Add(this.groupBox1);
            this.tabTransporte.Controls.Add(this.btCalc);
            this.tabTransporte.Controls.Add(this.lbDestino);
            this.tabTransporte.Controls.Add(this.lbOferta);
            this.tabTransporte.Controls.Add(this.cbDestino);
            this.tabTransporte.Controls.Add(this.cbOferta);
            this.tabTransporte.Controls.Add(this.dgTransp);
            this.tabTransporte.Location = new System.Drawing.Point(4, 22);
            this.tabTransporte.Name = "tabTransporte";
            this.tabTransporte.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransporte.Size = new System.Drawing.Size(851, 379);
            this.tabTransporte.TabIndex = 0;
            this.tabTransporte.Text = "Transporte";
            this.tabTransporte.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(301, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 51);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ajuda:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(242, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "- Coloque custo alto para transportes indesejados.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "- Crie nós ficticios para equilibrar o problema";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "- Demandas nas colunas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "- Ofertas nas linhas";
            // 
            // btCalc
            // 
            this.btCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCalc.Location = new System.Drawing.Point(687, 9);
            this.btCalc.Name = "btCalc";
            this.btCalc.Size = new System.Drawing.Size(158, 41);
            this.btCalc.TabIndex = 5;
            this.btCalc.Text = "Calcular!";
            this.btCalc.UseVisualStyleBackColor = true;
            this.btCalc.Click += new System.EventHandler(this.btCalc_Click);
            // 
            // lbDestino
            // 
            this.lbDestino.AutoSize = true;
            this.lbDestino.Location = new System.Drawing.Point(151, 7);
            this.lbDestino.Name = "lbDestino";
            this.lbDestino.Size = new System.Drawing.Size(113, 13);
            this.lbDestino.TabIndex = 4;
            this.lbDestino.Text = "Numero de Demandas";
            // 
            // lbOferta
            // 
            this.lbOferta.AutoSize = true;
            this.lbOferta.Location = new System.Drawing.Point(16, 7);
            this.lbOferta.Name = "lbOferta";
            this.lbOferta.Size = new System.Drawing.Size(96, 13);
            this.lbOferta.TabIndex = 3;
            this.lbOferta.Text = "Numero de Ofertas";
            // 
            // cbDestino
            // 
            this.cbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestino.FormattingEnabled = true;
            this.cbDestino.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbDestino.Location = new System.Drawing.Point(154, 26);
            this.cbDestino.Name = "cbDestino";
            this.cbDestino.Size = new System.Drawing.Size(121, 21);
            this.cbDestino.TabIndex = 2;
            this.cbDestino.SelectedIndexChanged += new System.EventHandler(this.cbDestino_SelectedIndexChanged);
            // 
            // cbOferta
            // 
            this.cbOferta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOferta.FormattingEnabled = true;
            this.cbOferta.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbOferta.Location = new System.Drawing.Point(16, 26);
            this.cbOferta.Name = "cbOferta";
            this.cbOferta.Size = new System.Drawing.Size(121, 21);
            this.cbOferta.TabIndex = 1;
            this.cbOferta.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dgTransp
            // 
            this.dgTransp.AllowUserToAddRows = false;
            this.dgTransp.AllowUserToDeleteRows = false;
            this.dgTransp.AllowUserToResizeColumns = false;
            this.dgTransp.AllowUserToResizeRows = false;
            this.dgTransp.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgTransp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransp.ColumnHeadersVisible = false;
            this.dgTransp.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgTransp.Location = new System.Drawing.Point(2, 60);
            this.dgTransp.MultiSelect = false;
            this.dgTransp.Name = "dgTransp";
            this.dgTransp.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgTransp.RowHeadersVisible = false;
            this.dgTransp.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgTransp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgTransp.ShowCellErrors = false;
            this.dgTransp.ShowCellToolTips = false;
            this.dgTransp.ShowEditingIcon = false;
            this.dgTransp.ShowRowErrors = false;
            this.dgTransp.Size = new System.Drawing.Size(843, 313);
            this.dgTransp.TabIndex = 0;
            // 
            // tabTransbordo
            // 
            this.tabTransbordo.Controls.Add(this.label11);
            this.tabTransbordo.Location = new System.Drawing.Point(4, 22);
            this.tabTransbordo.Name = "tabTransbordo";
            this.tabTransbordo.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransbordo.Size = new System.Drawing.Size(851, 379);
            this.tabTransbordo.TabIndex = 1;
            this.tabTransbordo.Text = "Transbordo";
            this.tabTransbordo.UseVisualStyleBackColor = true;
            // 
            // tabAlocacao
            // 
            this.tabAlocacao.Controls.Add(this.groupBox2);
            this.tabAlocacao.Controls.Add(this.btCalc3);
            this.tabAlocacao.Controls.Add(this.label5);
            this.tabAlocacao.Controls.Add(this.label6);
            this.tabAlocacao.Controls.Add(this.cbTar);
            this.tabAlocacao.Controls.Add(this.cbServ);
            this.tabAlocacao.Controls.Add(this.dgAloc);
            this.tabAlocacao.Location = new System.Drawing.Point(4, 22);
            this.tabAlocacao.Name = "tabAlocacao";
            this.tabAlocacao.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlocacao.Size = new System.Drawing.Size(851, 379);
            this.tabAlocacao.TabIndex = 2;
            this.tabAlocacao.Text = "Alocação";
            this.tabAlocacao.UseVisualStyleBackColor = true;
            this.tabAlocacao.Click += new System.EventHandler(this.tabAlocacao_Click);
            // 
            // btCalc3
            // 
            this.btCalc3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCalc3.Location = new System.Drawing.Point(687, 9);
            this.btCalc3.Name = "btCalc3";
            this.btCalc3.Size = new System.Drawing.Size(158, 41);
            this.btCalc3.TabIndex = 11;
            this.btCalc3.Text = "Calcular!";
            this.btCalc3.UseVisualStyleBackColor = true;
            this.btCalc3.Click += new System.EventHandler(this.btCalc3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Numero de Tarefas";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Numero de Servidores";
            // 
            // cbTar
            // 
            this.cbTar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTar.FormattingEnabled = true;
            this.cbTar.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13"});
            this.cbTar.Location = new System.Drawing.Point(154, 26);
            this.cbTar.Name = "cbTar";
            this.cbTar.Size = new System.Drawing.Size(121, 21);
            this.cbTar.TabIndex = 8;
            this.cbTar.SelectedIndexChanged += new System.EventHandler(this.cbTar_SelectedIndexChanged);
            // 
            // cbServ
            // 
            this.cbServ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServ.FormattingEnabled = true;
            this.cbServ.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13"});
            this.cbServ.Location = new System.Drawing.Point(16, 26);
            this.cbServ.Name = "cbServ";
            this.cbServ.Size = new System.Drawing.Size(121, 21);
            this.cbServ.TabIndex = 7;
            this.cbServ.SelectedIndexChanged += new System.EventHandler(this.cbServ_SelectedIndexChanged);
            // 
            // dgAloc
            // 
            this.dgAloc.AllowUserToAddRows = false;
            this.dgAloc.AllowUserToDeleteRows = false;
            this.dgAloc.AllowUserToResizeColumns = false;
            this.dgAloc.AllowUserToResizeRows = false;
            this.dgAloc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgAloc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAloc.ColumnHeadersVisible = false;
            this.dgAloc.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgAloc.Location = new System.Drawing.Point(2, 60);
            this.dgAloc.MultiSelect = false;
            this.dgAloc.Name = "dgAloc";
            this.dgAloc.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgAloc.RowHeadersVisible = false;
            this.dgAloc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgAloc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgAloc.ShowCellErrors = false;
            this.dgAloc.ShowCellToolTips = false;
            this.dgAloc.ShowEditingIcon = false;
            this.dgAloc.ShowRowErrors = false;
            this.dgAloc.Size = new System.Drawing.Size(843, 313);
            this.dgAloc.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(301, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 51);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ajuda:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(132, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(195, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "- Objetivo: minimizar custo de servidores";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "- Servidores nas linhas";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "- Tarefas nas Colunas";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(134, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "em tarefas";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(315, 18);
            this.label11.TabIndex = 0;
            this.label11.Text = "Método não implementado nesta versão.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 429);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Trabalho 3";
            this.tabControl.ResumeLayout(false);
            this.tabTransporte.ResumeLayout(false);
            this.tabTransporte.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransp)).EndInit();
            this.tabTransbordo.ResumeLayout(false);
            this.tabTransbordo.PerformLayout();
            this.tabAlocacao.ResumeLayout(false);
            this.tabAlocacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAloc)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabTransporte;
        private System.Windows.Forms.TabPage tabTransbordo;
        private System.Windows.Forms.TabPage tabAlocacao;
        private System.Windows.Forms.DataGridView dgTransp;
        private System.Windows.Forms.Button btCalc;
        private System.Windows.Forms.Label lbDestino;
        private System.Windows.Forms.Label lbOferta;
        private System.Windows.Forms.ComboBox cbDestino;
        private System.Windows.Forms.ComboBox cbOferta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCalc3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbTar;
        private System.Windows.Forms.ComboBox cbServ;
        private System.Windows.Forms.DataGridView dgAloc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

