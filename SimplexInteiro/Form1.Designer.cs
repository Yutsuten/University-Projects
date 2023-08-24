namespace Simplex
{
    partial class jnPrincipal
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbVar = new System.Windows.Forms.ComboBox();
            this.cbRes = new System.Windows.Forms.ComboBox();
            this.gbObj = new System.Windows.Forms.GroupBox();
            this.rbMax = new System.Windows.Forms.RadioButton();
            this.rbMin = new System.Windows.Forms.RadioButton();
            this.dgObj = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgRes = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.res = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btCalc = new System.Windows.Forms.Button();
            this.x0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbObj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Numero de Variáveis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numero de Restrições";
            // 
            // cbVar
            // 
            this.cbVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbVar.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbVar.Location = new System.Drawing.Point(15, 25);
            this.cbVar.Name = "cbVar";
            this.cbVar.Size = new System.Drawing.Size(102, 21);
            this.cbVar.TabIndex = 3;
            this.cbVar.SelectedIndexChanged += new System.EventHandler(this.cbVar_SelectedIndexChanged);
            // 
            // cbRes
            // 
            this.cbRes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRes.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cbRes.Location = new System.Drawing.Point(126, 25);
            this.cbRes.Name = "cbRes";
            this.cbRes.Size = new System.Drawing.Size(102, 21);
            this.cbRes.TabIndex = 4;
            this.cbRes.SelectedIndexChanged += new System.EventHandler(this.cbRes_SelectedIndexChanged);
            // 
            // gbObj
            // 
            this.gbObj.Controls.Add(this.rbMax);
            this.gbObj.Controls.Add(this.rbMin);
            this.gbObj.Location = new System.Drawing.Point(295, 9);
            this.gbObj.Name = "gbObj";
            this.gbObj.Size = new System.Drawing.Size(160, 46);
            this.gbObj.TabIndex = 5;
            this.gbObj.TabStop = false;
            this.gbObj.Text = "Objetivo";
            // 
            // rbMax
            // 
            this.rbMax.AutoSize = true;
            this.rbMax.Location = new System.Drawing.Point(80, 19);
            this.rbMax.Name = "rbMax";
            this.rbMax.Size = new System.Drawing.Size(71, 17);
            this.rbMax.TabIndex = 1;
            this.rbMax.TabStop = true;
            this.rbMax.Text = "Maximizar";
            this.rbMax.UseVisualStyleBackColor = true;
            // 
            // rbMin
            // 
            this.rbMin.AutoSize = true;
            this.rbMin.Checked = true;
            this.rbMin.Location = new System.Drawing.Point(6, 19);
            this.rbMin.Name = "rbMin";
            this.rbMin.Size = new System.Drawing.Size(68, 17);
            this.rbMin.TabIndex = 0;
            this.rbMin.TabStop = true;
            this.rbMin.Text = "Minimizar";
            this.rbMin.UseVisualStyleBackColor = true;
            // 
            // dgObj
            // 
            this.dgObj.AllowUserToAddRows = false;
            this.dgObj.AllowUserToDeleteRows = false;
            this.dgObj.AllowUserToResizeColumns = false;
            this.dgObj.AllowUserToResizeRows = false;
            this.dgObj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgObj.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.x0,
            this.x1,
            this.x2,
            this.x3,
            this.x4,
            this.x5,
            this.x6,
            this.x7});
            this.dgObj.Location = new System.Drawing.Point(15, 75);
            this.dgObj.MultiSelect = false;
            this.dgObj.Name = "dgObj";
            this.dgObj.RowHeadersVisible = false;
            this.dgObj.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgObj.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgObj.Size = new System.Drawing.Size(703, 89);
            this.dgObj.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Função Objetivo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Equações de Restrições";
            // 
            // dgRes
            // 
            this.dgRes.AllowUserToAddRows = false;
            this.dgRes.AllowUserToDeleteRows = false;
            this.dgRes.AllowUserToResizeColumns = false;
            this.dgRes.AllowUserToResizeRows = false;
            this.dgRes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.Sinal,
            this.res});
            this.dgRes.Location = new System.Drawing.Point(15, 196);
            this.dgRes.MultiSelect = false;
            this.dgRes.Name = "dgRes";
            this.dgRes.RowHeadersVisible = false;
            this.dgRes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgRes.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgRes.Size = new System.Drawing.Size(703, 155);
            this.dgRes.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "x1";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "x2";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "x3";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "x4";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "x5";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "x6";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "x7";
            this.dataGridViewTextBoxColumn7.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "x8";
            this.dataGridViewTextBoxColumn8.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn8.Width = 70;
            // 
            // Sinal
            // 
            this.Sinal.HeaderText = "Sinal";
            this.Sinal.MaxInputLength = 6;
            this.Sinal.Name = "Sinal";
            this.Sinal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Sinal.Width = 70;
            // 
            // res
            // 
            this.res.HeaderText = "b";
            this.res.MaxInputLength = 6;
            this.res.Name = "res";
            this.res.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.res.Width = 70;
            // 
            // btCalc
            // 
            this.btCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCalc.Location = new System.Drawing.Point(538, 373);
            this.btCalc.Name = "btCalc";
            this.btCalc.Size = new System.Drawing.Size(180, 51);
            this.btCalc.TabIndex = 10;
            this.btCalc.Text = "Calcular!";
            this.btCalc.UseVisualStyleBackColor = true;
            this.btCalc.Click += new System.EventHandler(this.btCalc_Click);
            // 
            // x0
            // 
            this.x0.HeaderText = "c1";
            this.x0.MaxInputLength = 6;
            this.x0.Name = "x0";
            this.x0.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x0.Width = 85;
            // 
            // x1
            // 
            this.x1.HeaderText = "c2";
            this.x1.MaxInputLength = 6;
            this.x1.Name = "x1";
            this.x1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x1.Width = 85;
            // 
            // x2
            // 
            this.x2.HeaderText = "c3";
            this.x2.MaxInputLength = 6;
            this.x2.Name = "x2";
            this.x2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x2.Width = 85;
            // 
            // x3
            // 
            this.x3.HeaderText = "c4";
            this.x3.MaxInputLength = 6;
            this.x3.Name = "x3";
            this.x3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x3.Width = 85;
            // 
            // x4
            // 
            this.x4.HeaderText = "c5";
            this.x4.MaxInputLength = 6;
            this.x4.Name = "x4";
            this.x4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x4.Width = 85;
            // 
            // x5
            // 
            this.x5.HeaderText = "c6";
            this.x5.MaxInputLength = 6;
            this.x5.Name = "x5";
            this.x5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x5.Width = 85;
            // 
            // x6
            // 
            this.x6.HeaderText = "c7";
            this.x6.MaxInputLength = 6;
            this.x6.Name = "x6";
            this.x6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x6.Width = 85;
            // 
            // x7
            // 
            this.x7.HeaderText = "c8";
            this.x7.MaxInputLength = 6;
            this.x7.Name = "x7";
            this.x7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x7.Width = 85;
            // 
            // jnPrincipal
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 436);
            this.Controls.Add(this.btCalc);
            this.Controls.Add(this.dgRes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgObj);
            this.Controls.Add(this.gbObj);
            this.Controls.Add(this.cbRes);
            this.Controls.Add(this.cbVar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "jnPrincipal";
            this.Text = "Simplex";
            this.gbObj.ResumeLayout(false);
            this.gbObj.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbVar;
        private System.Windows.Forms.ComboBox cbRes;
        private System.Windows.Forms.GroupBox gbObj;
        private System.Windows.Forms.RadioButton rbMax;
        private System.Windows.Forms.RadioButton rbMin;
        private System.Windows.Forms.DataGridView dgObj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgRes;
        private System.Windows.Forms.Button btCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn res;
        private System.Windows.Forms.DataGridViewTextBoxColumn x0;
        private System.Windows.Forms.DataGridViewTextBoxColumn x1;
        private System.Windows.Forms.DataGridViewTextBoxColumn x2;
        private System.Windows.Forms.DataGridViewTextBoxColumn x3;
        private System.Windows.Forms.DataGridViewTextBoxColumn x4;
        private System.Windows.Forms.DataGridViewTextBoxColumn x5;
        private System.Windows.Forms.DataGridViewTextBoxColumn x6;
        private System.Windows.Forms.DataGridViewTextBoxColumn x7;
    }
}

