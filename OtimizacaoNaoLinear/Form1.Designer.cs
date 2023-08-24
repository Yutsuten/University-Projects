namespace Otimizacao_NaoLinear
{
    partial class MainForm
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
            this.Aba = new System.Windows.Forms.TabControl();
            this.tabMono = new System.Windows.Forms.TabPage();
            this.nudMaxIt = new System.Windows.Forms.NumericUpDown();
            this.lbMaxIteracoes = new System.Windows.Forms.Label();
            this.nudPasso = new System.Windows.Forms.NumericUpDown();
            this.lbPasso = new System.Windows.Forms.Label();
            this.nudPrecisao = new System.Windows.Forms.NumericUpDown();
            this.lbPrecisao = new System.Windows.Forms.Label();
            this.lbSitFunc = new System.Windows.Forms.Label();
            this.rtbCalculo = new System.Windows.Forms.RichTextBox();
            this.lbCalculos = new System.Windows.Forms.Label();
            this.btCalcula = new System.Windows.Forms.Button();
            this.lbIntervalo = new System.Windows.Forms.Label();
            this.nudB = new System.Windows.Forms.NumericUpDown();
            this.nudA = new System.Windows.Forms.NumericUpDown();
            this.lbA = new System.Windows.Forms.Label();
            this.lbStrIntervalo = new System.Windows.Forms.Label();
            this.tbFuncao = new System.Windows.Forms.TextBox();
            this.lbFuncao = new System.Windows.Forms.Label();
            this.gbMetodos = new System.Windows.Forms.GroupBox();
            this.rbNewton = new System.Windows.Forms.RadioButton();
            this.rbBissecao = new System.Windows.Forms.RadioButton();
            this.rbFibonacci = new System.Windows.Forms.RadioButton();
            this.rbSecaoAurea = new System.Windows.Forms.RadioButton();
            this.rbDicotomica = new System.Windows.Forms.RadioButton();
            this.rbBuscaUniforme = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lbSemDerivadas = new System.Windows.Forms.Label();
            this.tabMult = new System.Windows.Forms.TabPage();
            this.lbPassIter = new System.Windows.Forms.Label();
            this.nudPassIter = new System.Windows.Forms.NumericUpDown();
            this.lbSitFunc2 = new System.Windows.Forms.Label();
            this.rtbCalculo2 = new System.Windows.Forms.RichTextBox();
            this.lbCalculos2 = new System.Windows.Forms.Label();
            this.btCalcula2 = new System.Windows.Forms.Button();
            this.nudMaxIt2 = new System.Windows.Forms.NumericUpDown();
            this.lbMaxIteracoes2 = new System.Windows.Forms.Label();
            this.lbPontoD = new System.Windows.Forms.Label();
            this.tbPontoD = new System.Windows.Forms.TextBox();
            this.lbPontoF = new System.Windows.Forms.Label();
            this.tbPontoF = new System.Windows.Forms.TextBox();
            this.lbPontoB = new System.Windows.Forms.Label();
            this.tbPontob = new System.Windows.Forms.TextBox();
            this.lbPontoE = new System.Windows.Forms.Label();
            this.tbPontoE = new System.Windows.Forms.TextBox();
            this.lbPontoC = new System.Windows.Forms.Label();
            this.tbPontoC = new System.Windows.Forms.TextBox();
            this.lbPontoA = new System.Windows.Forms.Label();
            this.tbPontoA = new System.Windows.Forms.TextBox();
            this.lbPontoInicial = new System.Windows.Forms.Label();
            this.nudPrecisao2 = new System.Windows.Forms.NumericUpDown();
            this.lbPrecisao2 = new System.Windows.Forms.Label();
            this.tbFuncao2 = new System.Windows.Forms.TextBox();
            this.lbDigitaFunc = new System.Windows.Forms.Label();
            this.nudNumVar = new System.Windows.Forms.NumericUpDown();
            this.lbNumVar = new System.Windows.Forms.Label();
            this.gbMetodosMulti = new System.Windows.Forms.GroupBox();
            this.rbDavidon = new System.Windows.Forms.RadioButton();
            this.rbGradConjGen = new System.Windows.Forms.RadioButton();
            this.rbFletcher = new System.Windows.Forms.RadioButton();
            this.rbGradConj = new System.Windows.Forms.RadioButton();
            this.rbNewton2 = new System.Windows.Forms.RadioButton();
            this.rbPassoDesc = new System.Windows.Forms.RadioButton();
            this.rbHookeJeeves = new System.Windows.Forms.RadioButton();
            this.rbCoordCicl = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabMultRes = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.Aba.SuspendLayout();
            this.tabMono.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPasso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecisao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudA)).BeginInit();
            this.gbMetodos.SuspendLayout();
            this.tabMult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassIter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIt2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecisao2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumVar)).BeginInit();
            this.gbMetodosMulti.SuspendLayout();
            this.tabMultRes.SuspendLayout();
            this.SuspendLayout();
            // 
            // Aba
            // 
            this.Aba.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Aba.Controls.Add(this.tabMono);
            this.Aba.Controls.Add(this.tabMult);
            this.Aba.Controls.Add(this.tabMultRes);
            this.Aba.Location = new System.Drawing.Point(7, 8);
            this.Aba.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Aba.Name = "Aba";
            this.Aba.SelectedIndex = 0;
            this.Aba.Size = new System.Drawing.Size(618, 507);
            this.Aba.TabIndex = 0;
            // 
            // tabMono
            // 
            this.tabMono.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabMono.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabMono.Controls.Add(this.nudMaxIt);
            this.tabMono.Controls.Add(this.lbMaxIteracoes);
            this.tabMono.Controls.Add(this.nudPasso);
            this.tabMono.Controls.Add(this.lbPasso);
            this.tabMono.Controls.Add(this.nudPrecisao);
            this.tabMono.Controls.Add(this.lbPrecisao);
            this.tabMono.Controls.Add(this.lbSitFunc);
            this.tabMono.Controls.Add(this.rtbCalculo);
            this.tabMono.Controls.Add(this.lbCalculos);
            this.tabMono.Controls.Add(this.btCalcula);
            this.tabMono.Controls.Add(this.lbIntervalo);
            this.tabMono.Controls.Add(this.nudB);
            this.tabMono.Controls.Add(this.nudA);
            this.tabMono.Controls.Add(this.lbA);
            this.tabMono.Controls.Add(this.lbStrIntervalo);
            this.tabMono.Controls.Add(this.tbFuncao);
            this.tabMono.Controls.Add(this.lbFuncao);
            this.tabMono.Controls.Add(this.gbMetodos);
            this.tabMono.Location = new System.Drawing.Point(4, 26);
            this.tabMono.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMono.Name = "tabMono";
            this.tabMono.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMono.Size = new System.Drawing.Size(610, 477);
            this.tabMono.TabIndex = 0;
            this.tabMono.Text = "Otimização Monovariável";
            // 
            // nudMaxIt
            // 
            this.nudMaxIt.Location = new System.Drawing.Point(209, 216);
            this.nudMaxIt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxIt.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMaxIt.Name = "nudMaxIt";
            this.nudMaxIt.Size = new System.Drawing.Size(78, 24);
            this.nudMaxIt.TabIndex = 4;
            this.nudMaxIt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lbMaxIteracoes
            // 
            this.lbMaxIteracoes.AutoSize = true;
            this.lbMaxIteracoes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbMaxIteracoes.Location = new System.Drawing.Point(206, 196);
            this.lbMaxIteracoes.Name = "lbMaxIteracoes";
            this.lbMaxIteracoes.Size = new System.Drawing.Size(125, 17);
            this.lbMaxIteracoes.TabIndex = 17;
            this.lbMaxIteracoes.Text = "Máximo de Iterações:";
            // 
            // nudPasso
            // 
            this.nudPasso.DecimalPlaces = 3;
            this.nudPasso.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudPasso.Location = new System.Drawing.Point(455, 47);
            this.nudPasso.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPasso.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudPasso.Name = "nudPasso";
            this.nudPasso.Size = new System.Drawing.Size(90, 24);
            this.nudPasso.TabIndex = 5;
            this.nudPasso.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPasso.ValueChanged += new System.EventHandler(this.nudPasso_ValueChanged);
            // 
            // lbPasso
            // 
            this.lbPasso.AutoSize = true;
            this.lbPasso.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPasso.Location = new System.Drawing.Point(452, 27);
            this.lbPasso.Name = "lbPasso";
            this.lbPasso.Size = new System.Drawing.Size(93, 17);
            this.lbPasso.TabIndex = 11;
            this.lbPasso.Text = "Valor do passo:";
            // 
            // nudPrecisao
            // 
            this.nudPrecisao.DecimalPlaces = 4;
            this.nudPrecisao.Enabled = false;
            this.nudPrecisao.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudPrecisao.Location = new System.Drawing.Point(455, 123);
            this.nudPrecisao.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrecisao.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudPrecisao.Name = "nudPrecisao";
            this.nudPrecisao.Size = new System.Drawing.Size(90, 24);
            this.nudPrecisao.TabIndex = 6;
            this.nudPrecisao.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // lbPrecisao
            // 
            this.lbPrecisao.AutoSize = true;
            this.lbPrecisao.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPrecisao.Location = new System.Drawing.Point(452, 103);
            this.lbPrecisao.Name = "lbPrecisao";
            this.lbPrecisao.Size = new System.Drawing.Size(58, 17);
            this.lbPrecisao.TabIndex = 15;
            this.lbPrecisao.Text = "Precisão:";
            // 
            // lbSitFunc
            // 
            this.lbSitFunc.AutoSize = true;
            this.lbSitFunc.ForeColor = System.Drawing.Color.Red;
            this.lbSitFunc.Location = new System.Drawing.Point(206, 78);
            this.lbSitFunc.Name = "lbSitFunc";
            this.lbSitFunc.Size = new System.Drawing.Size(0, 17);
            this.lbSitFunc.TabIndex = 14;
            // 
            // rtbCalculo
            // 
            this.rtbCalculo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.rtbCalculo.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbCalculo.DetectUrls = false;
            this.rtbCalculo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCalculo.Location = new System.Drawing.Point(9, 309);
            this.rtbCalculo.Name = "rtbCalculo";
            this.rtbCalculo.ReadOnly = true;
            this.rtbCalculo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbCalculo.Size = new System.Drawing.Size(591, 159);
            this.rtbCalculo.TabIndex = 8;
            this.rtbCalculo.Text = "< Nenhum cálculo feito até o momento. >";
            // 
            // lbCalculos
            // 
            this.lbCalculos.AutoSize = true;
            this.lbCalculos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbCalculos.Location = new System.Drawing.Point(6, 289);
            this.lbCalculos.Name = "lbCalculos";
            this.lbCalculos.Size = new System.Drawing.Size(57, 17);
            this.lbCalculos.TabIndex = 12;
            this.lbCalculos.Text = "Cálculos:";
            // 
            // btCalcula
            // 
            this.btCalcula.Enabled = false;
            this.btCalcula.Font = new System.Drawing.Font("Meiryo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCalcula.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btCalcula.Location = new System.Drawing.Point(421, 200);
            this.btCalcula.Name = "btCalcula";
            this.btCalcula.Size = new System.Drawing.Size(155, 47);
            this.btCalcula.TabIndex = 7;
            this.btCalcula.Text = "Calcular!";
            this.btCalcula.UseVisualStyleBackColor = true;
            this.btCalcula.Click += new System.EventHandler(this.btCalcula_Click);
            // 
            // lbIntervalo
            // 
            this.lbIntervalo.AutoSize = true;
            this.lbIntervalo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbIntervalo.Location = new System.Drawing.Point(268, 155);
            this.lbIntervalo.Name = "lbIntervalo";
            this.lbIntervalo.Size = new System.Drawing.Size(62, 17);
            this.lbIntervalo.TabIndex = 9;
            this.lbIntervalo.Text = "0 ≤ x ≤ 3";
            // 
            // nudB
            // 
            this.nudB.DecimalPlaces = 4;
            this.nudB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudB.Location = new System.Drawing.Point(317, 123);
            this.nudB.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudB.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudB.Name = "nudB";
            this.nudB.Size = new System.Drawing.Size(78, 24);
            this.nudB.TabIndex = 3;
            this.nudB.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudB.ValueChanged += new System.EventHandler(this.nudB_ValueChanged);
            // 
            // nudA
            // 
            this.nudA.DecimalPlaces = 4;
            this.nudA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudA.Location = new System.Drawing.Point(209, 123);
            this.nudA.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudA.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudA.Name = "nudA";
            this.nudA.Size = new System.Drawing.Size(78, 24);
            this.nudA.TabIndex = 2;
            this.nudA.ValueChanged += new System.EventHandler(this.nudA_ValueChanged);
            // 
            // lbA
            // 
            this.lbA.AutoSize = true;
            this.lbA.Font = new System.Drawing.Font("Meiryo", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbA.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbA.Location = new System.Drawing.Point(294, 128);
            this.lbA.Name = "lbA";
            this.lbA.Size = new System.Drawing.Size(17, 20);
            this.lbA.TabIndex = 6;
            this.lbA.Text = "a";
            // 
            // lbStrIntervalo
            // 
            this.lbStrIntervalo.AutoSize = true;
            this.lbStrIntervalo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbStrIntervalo.Location = new System.Drawing.Point(206, 103);
            this.lbStrIntervalo.Name = "lbStrIntervalo";
            this.lbStrIntervalo.Size = new System.Drawing.Size(113, 17);
            this.lbStrIntervalo.TabIndex = 3;
            this.lbStrIntervalo.Text = "Intervalo de busca:";
            // 
            // tbFuncao
            // 
            this.tbFuncao.Location = new System.Drawing.Point(209, 47);
            this.tbFuncao.Name = "tbFuncao";
            this.tbFuncao.Size = new System.Drawing.Size(186, 24);
            this.tbFuncao.TabIndex = 1;
            this.tbFuncao.TextChanged += new System.EventHandler(this.tbFuncao_TextChanged);
            // 
            // lbFuncao
            // 
            this.lbFuncao.AutoSize = true;
            this.lbFuncao.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbFuncao.Location = new System.Drawing.Point(206, 27);
            this.lbFuncao.Name = "lbFuncao";
            this.lbFuncao.Size = new System.Drawing.Size(146, 17);
            this.lbFuncao.TabIndex = 1;
            this.lbFuncao.Text = "Digite aqui a função f(x):";
            // 
            // gbMetodos
            // 
            this.gbMetodos.Controls.Add(this.rbNewton);
            this.gbMetodos.Controls.Add(this.rbBissecao);
            this.gbMetodos.Controls.Add(this.rbFibonacci);
            this.gbMetodos.Controls.Add(this.rbSecaoAurea);
            this.gbMetodos.Controls.Add(this.rbDicotomica);
            this.gbMetodos.Controls.Add(this.rbBuscaUniforme);
            this.gbMetodos.Controls.Add(this.label2);
            this.gbMetodos.Controls.Add(this.lbSemDerivadas);
            this.gbMetodos.Location = new System.Drawing.Point(6, 7);
            this.gbMetodos.Name = "gbMetodos";
            this.gbMetodos.Size = new System.Drawing.Size(182, 240);
            this.gbMetodos.TabIndex = 0;
            this.gbMetodos.TabStop = false;
            this.gbMetodos.Text = "Métodos";
            // 
            // rbNewton
            // 
            this.rbNewton.AutoSize = true;
            this.rbNewton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbNewton.Location = new System.Drawing.Point(19, 212);
            this.rbNewton.Name = "rbNewton";
            this.rbNewton.Size = new System.Drawing.Size(67, 21);
            this.rbNewton.TabIndex = 7;
            this.rbNewton.Text = "Newton";
            this.rbNewton.UseVisualStyleBackColor = true;
            this.rbNewton.CheckedChanged += new System.EventHandler(this.rbNewton_CheckedChanged);
            // 
            // rbBissecao
            // 
            this.rbBissecao.AutoSize = true;
            this.rbBissecao.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbBissecao.Location = new System.Drawing.Point(19, 185);
            this.rbBissecao.Name = "rbBissecao";
            this.rbBissecao.Size = new System.Drawing.Size(71, 21);
            this.rbBissecao.TabIndex = 6;
            this.rbBissecao.Text = "Bisseção";
            this.rbBissecao.UseVisualStyleBackColor = true;
            this.rbBissecao.CheckedChanged += new System.EventHandler(this.rbBissecao_CheckedChanged);
            // 
            // rbFibonacci
            // 
            this.rbFibonacci.AutoSize = true;
            this.rbFibonacci.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbFibonacci.Location = new System.Drawing.Point(19, 121);
            this.rbFibonacci.Name = "rbFibonacci";
            this.rbFibonacci.Size = new System.Drawing.Size(129, 21);
            this.rbFibonacci.TabIndex = 5;
            this.rbFibonacci.Text = "Busca de Fibonacci";
            this.rbFibonacci.UseVisualStyleBackColor = true;
            this.rbFibonacci.CheckedChanged += new System.EventHandler(this.rbFibonacci_CheckedChanged);
            // 
            // rbSecaoAurea
            // 
            this.rbSecaoAurea.AutoSize = true;
            this.rbSecaoAurea.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbSecaoAurea.Location = new System.Drawing.Point(19, 94);
            this.rbSecaoAurea.Name = "rbSecaoAurea";
            this.rbSecaoAurea.Size = new System.Drawing.Size(93, 21);
            this.rbSecaoAurea.TabIndex = 4;
            this.rbSecaoAurea.Text = "Seção Áurea";
            this.rbSecaoAurea.UseVisualStyleBackColor = true;
            this.rbSecaoAurea.CheckedChanged += new System.EventHandler(this.rbSecaoAurea_CheckedChanged);
            // 
            // rbDicotomica
            // 
            this.rbDicotomica.AutoSize = true;
            this.rbDicotomica.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbDicotomica.Location = new System.Drawing.Point(19, 67);
            this.rbDicotomica.Name = "rbDicotomica";
            this.rbDicotomica.Size = new System.Drawing.Size(122, 21);
            this.rbDicotomica.TabIndex = 3;
            this.rbDicotomica.Text = "Busca Dicotômica";
            this.rbDicotomica.UseVisualStyleBackColor = true;
            this.rbDicotomica.CheckedChanged += new System.EventHandler(this.rbDicotomica_CheckedChanged);
            // 
            // rbBuscaUniforme
            // 
            this.rbBuscaUniforme.AutoSize = true;
            this.rbBuscaUniforme.Checked = true;
            this.rbBuscaUniforme.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbBuscaUniforme.Location = new System.Drawing.Point(19, 40);
            this.rbBuscaUniforme.Name = "rbBuscaUniforme";
            this.rbBuscaUniforme.Size = new System.Drawing.Size(112, 21);
            this.rbBuscaUniforme.TabIndex = 2;
            this.rbBuscaUniforme.TabStop = true;
            this.rbBuscaUniforme.Text = "Busca Uniforme";
            this.rbBuscaUniforme.UseVisualStyleBackColor = true;
            this.rbBuscaUniforme.CheckedChanged += new System.EventHandler(this.rbBuscaUniforme_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(6, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Com derivadas:";
            // 
            // lbSemDerivadas
            // 
            this.lbSemDerivadas.AutoSize = true;
            this.lbSemDerivadas.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbSemDerivadas.Location = new System.Drawing.Point(6, 20);
            this.lbSemDerivadas.Name = "lbSemDerivadas";
            this.lbSemDerivadas.Size = new System.Drawing.Size(92, 17);
            this.lbSemDerivadas.TabIndex = 0;
            this.lbSemDerivadas.Text = "Sem derivadas:";
            // 
            // tabMult
            // 
            this.tabMult.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabMult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabMult.Controls.Add(this.lbPassIter);
            this.tabMult.Controls.Add(this.nudPassIter);
            this.tabMult.Controls.Add(this.lbSitFunc2);
            this.tabMult.Controls.Add(this.rtbCalculo2);
            this.tabMult.Controls.Add(this.lbCalculos2);
            this.tabMult.Controls.Add(this.btCalcula2);
            this.tabMult.Controls.Add(this.nudMaxIt2);
            this.tabMult.Controls.Add(this.lbMaxIteracoes2);
            this.tabMult.Controls.Add(this.lbPontoD);
            this.tabMult.Controls.Add(this.tbPontoD);
            this.tabMult.Controls.Add(this.lbPontoF);
            this.tabMult.Controls.Add(this.tbPontoF);
            this.tabMult.Controls.Add(this.lbPontoB);
            this.tabMult.Controls.Add(this.tbPontob);
            this.tabMult.Controls.Add(this.lbPontoE);
            this.tabMult.Controls.Add(this.tbPontoE);
            this.tabMult.Controls.Add(this.lbPontoC);
            this.tabMult.Controls.Add(this.tbPontoC);
            this.tabMult.Controls.Add(this.lbPontoA);
            this.tabMult.Controls.Add(this.tbPontoA);
            this.tabMult.Controls.Add(this.lbPontoInicial);
            this.tabMult.Controls.Add(this.nudPrecisao2);
            this.tabMult.Controls.Add(this.lbPrecisao2);
            this.tabMult.Controls.Add(this.tbFuncao2);
            this.tabMult.Controls.Add(this.lbDigitaFunc);
            this.tabMult.Controls.Add(this.nudNumVar);
            this.tabMult.Controls.Add(this.lbNumVar);
            this.tabMult.Controls.Add(this.gbMetodosMulti);
            this.tabMult.Location = new System.Drawing.Point(4, 26);
            this.tabMult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMult.Name = "tabMult";
            this.tabMult.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMult.Size = new System.Drawing.Size(610, 477);
            this.tabMult.TabIndex = 1;
            this.tabMult.Text = "Otimização Multivariável sem Restrições";
            // 
            // lbPassIter
            // 
            this.lbPassIter.AutoSize = true;
            this.lbPassIter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPassIter.Location = new System.Drawing.Point(314, 235);
            this.lbPassIter.Name = "lbPassIter";
            this.lbPassIter.Size = new System.Drawing.Size(81, 17);
            this.lbPassIter.TabIndex = 38;
            this.lbPassIter.Text = "Nºpassos/iter";
            this.lbPassIter.Visible = false;
            // 
            // nudPassIter
            // 
            this.nudPassIter.Location = new System.Drawing.Point(317, 255);
            this.nudPassIter.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudPassIter.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudPassIter.Name = "nudPassIter";
            this.nudPassIter.Size = new System.Drawing.Size(78, 24);
            this.nudPassIter.TabIndex = 37;
            this.nudPassIter.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPassIter.Visible = false;
            // 
            // lbSitFunc2
            // 
            this.lbSitFunc2.AutoSize = true;
            this.lbSitFunc2.ForeColor = System.Drawing.Color.Red;
            this.lbSitFunc2.Location = new System.Drawing.Point(206, 78);
            this.lbSitFunc2.Name = "lbSitFunc2";
            this.lbSitFunc2.Size = new System.Drawing.Size(0, 17);
            this.lbSitFunc2.TabIndex = 36;
            // 
            // rtbCalculo2
            // 
            this.rtbCalculo2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.rtbCalculo2.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbCalculo2.DetectUrls = false;
            this.rtbCalculo2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCalculo2.Location = new System.Drawing.Point(9, 309);
            this.rtbCalculo2.Name = "rtbCalculo2";
            this.rtbCalculo2.ReadOnly = true;
            this.rtbCalculo2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbCalculo2.Size = new System.Drawing.Size(591, 159);
            this.rtbCalculo2.TabIndex = 34;
            this.rtbCalculo2.Text = "< Nenhum cálculo feito até o momento. >";
            // 
            // lbCalculos2
            // 
            this.lbCalculos2.AutoSize = true;
            this.lbCalculos2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbCalculos2.Location = new System.Drawing.Point(6, 289);
            this.lbCalculos2.Name = "lbCalculos2";
            this.lbCalculos2.Size = new System.Drawing.Size(57, 17);
            this.lbCalculos2.TabIndex = 35;
            this.lbCalculos2.Text = "Cálculos:";
            // 
            // btCalcula2
            // 
            this.btCalcula2.Enabled = false;
            this.btCalcula2.Font = new System.Drawing.Font("Meiryo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCalcula2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btCalcula2.Location = new System.Drawing.Point(421, 235);
            this.btCalcula2.Name = "btCalcula2";
            this.btCalcula2.Size = new System.Drawing.Size(155, 47);
            this.btCalcula2.TabIndex = 33;
            this.btCalcula2.Text = "Calcular!";
            this.btCalcula2.UseVisualStyleBackColor = true;
            this.btCalcula2.Click += new System.EventHandler(this.btCalcula2_Click);
            // 
            // nudMaxIt2
            // 
            this.nudMaxIt2.Location = new System.Drawing.Point(214, 255);
            this.nudMaxIt2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxIt2.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudMaxIt2.Name = "nudMaxIt2";
            this.nudMaxIt2.Size = new System.Drawing.Size(78, 24);
            this.nudMaxIt2.TabIndex = 31;
            this.nudMaxIt2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lbMaxIteracoes2
            // 
            this.lbMaxIteracoes2.AutoSize = true;
            this.lbMaxIteracoes2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbMaxIteracoes2.Location = new System.Drawing.Point(211, 235);
            this.lbMaxIteracoes2.Name = "lbMaxIteracoes2";
            this.lbMaxIteracoes2.Size = new System.Drawing.Size(125, 17);
            this.lbMaxIteracoes2.TabIndex = 32;
            this.lbMaxIteracoes2.Text = "Máximo de Iterações:";
            // 
            // lbPontoD
            // 
            this.lbPontoD.AutoSize = true;
            this.lbPontoD.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPontoD.Location = new System.Drawing.Point(295, 160);
            this.lbPontoD.Name = "lbPontoD";
            this.lbPontoD.Size = new System.Drawing.Size(28, 17);
            this.lbPontoD.TabIndex = 30;
            this.lbPontoD.Text = "d =";
            // 
            // tbPontoD
            // 
            this.tbPontoD.Enabled = false;
            this.tbPontoD.Location = new System.Drawing.Point(325, 157);
            this.tbPontoD.Name = "tbPontoD";
            this.tbPontoD.Size = new System.Drawing.Size(48, 24);
            this.tbPontoD.TabIndex = 29;
            this.tbPontoD.Text = "0";
            this.tbPontoD.TextChanged += new System.EventHandler(this.tbPontoD_TextChanged);
            // 
            // lbPontoF
            // 
            this.lbPontoF.AutoSize = true;
            this.lbPontoF.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPontoF.Location = new System.Drawing.Point(295, 190);
            this.lbPontoF.Name = "lbPontoF";
            this.lbPontoF.Size = new System.Drawing.Size(25, 17);
            this.lbPontoF.TabIndex = 28;
            this.lbPontoF.Text = "f =";
            // 
            // tbPontoF
            // 
            this.tbPontoF.Enabled = false;
            this.tbPontoF.Location = new System.Drawing.Point(325, 187);
            this.tbPontoF.Name = "tbPontoF";
            this.tbPontoF.Size = new System.Drawing.Size(48, 24);
            this.tbPontoF.TabIndex = 27;
            this.tbPontoF.Text = "0";
            this.tbPontoF.TextChanged += new System.EventHandler(this.tbPontoF_TextChanged);
            // 
            // lbPontoB
            // 
            this.lbPontoB.AutoSize = true;
            this.lbPontoB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPontoB.Location = new System.Drawing.Point(295, 130);
            this.lbPontoB.Name = "lbPontoB";
            this.lbPontoB.Size = new System.Drawing.Size(28, 17);
            this.lbPontoB.TabIndex = 26;
            this.lbPontoB.Text = "b =";
            // 
            // tbPontob
            // 
            this.tbPontob.Location = new System.Drawing.Point(325, 127);
            this.tbPontob.Name = "tbPontob";
            this.tbPontob.Size = new System.Drawing.Size(48, 24);
            this.tbPontob.TabIndex = 25;
            this.tbPontob.Text = "0";
            this.tbPontob.TextChanged += new System.EventHandler(this.tbPontob_TextChanged);
            // 
            // lbPontoE
            // 
            this.lbPontoE.AutoSize = true;
            this.lbPontoE.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPontoE.Location = new System.Drawing.Point(211, 190);
            this.lbPontoE.Name = "lbPontoE";
            this.lbPontoE.Size = new System.Drawing.Size(27, 17);
            this.lbPontoE.TabIndex = 24;
            this.lbPontoE.Text = "e =";
            // 
            // tbPontoE
            // 
            this.tbPontoE.Enabled = false;
            this.tbPontoE.Location = new System.Drawing.Point(241, 187);
            this.tbPontoE.Name = "tbPontoE";
            this.tbPontoE.Size = new System.Drawing.Size(48, 24);
            this.tbPontoE.TabIndex = 23;
            this.tbPontoE.Text = "0";
            this.tbPontoE.TextChanged += new System.EventHandler(this.tbPontoE_TextChanged);
            // 
            // lbPontoC
            // 
            this.lbPontoC.AutoSize = true;
            this.lbPontoC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPontoC.Location = new System.Drawing.Point(211, 160);
            this.lbPontoC.Name = "lbPontoC";
            this.lbPontoC.Size = new System.Drawing.Size(27, 17);
            this.lbPontoC.TabIndex = 22;
            this.lbPontoC.Text = "c =";
            // 
            // tbPontoC
            // 
            this.tbPontoC.Enabled = false;
            this.tbPontoC.Location = new System.Drawing.Point(241, 157);
            this.tbPontoC.Name = "tbPontoC";
            this.tbPontoC.Size = new System.Drawing.Size(48, 24);
            this.tbPontoC.TabIndex = 21;
            this.tbPontoC.Text = "0";
            this.tbPontoC.TextChanged += new System.EventHandler(this.tbPontoC_TextChanged);
            // 
            // lbPontoA
            // 
            this.lbPontoA.AutoSize = true;
            this.lbPontoA.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPontoA.Location = new System.Drawing.Point(211, 130);
            this.lbPontoA.Name = "lbPontoA";
            this.lbPontoA.Size = new System.Drawing.Size(27, 17);
            this.lbPontoA.TabIndex = 20;
            this.lbPontoA.Text = "a =";
            // 
            // tbPontoA
            // 
            this.tbPontoA.Location = new System.Drawing.Point(241, 127);
            this.tbPontoA.Name = "tbPontoA";
            this.tbPontoA.Size = new System.Drawing.Size(48, 24);
            this.tbPontoA.TabIndex = 19;
            this.tbPontoA.Text = "0";
            this.tbPontoA.TextChanged += new System.EventHandler(this.tbPontoA_TextChanged);
            // 
            // lbPontoInicial
            // 
            this.lbPontoInicial.AutoSize = true;
            this.lbPontoInicial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPontoInicial.Location = new System.Drawing.Point(211, 107);
            this.lbPontoInicial.Name = "lbPontoInicial";
            this.lbPontoInicial.Size = new System.Drawing.Size(80, 17);
            this.lbPontoInicial.TabIndex = 18;
            this.lbPontoInicial.Text = "Ponto inicial:";
            // 
            // nudPrecisao2
            // 
            this.nudPrecisao2.DecimalPlaces = 4;
            this.nudPrecisao2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudPrecisao2.Location = new System.Drawing.Point(455, 123);
            this.nudPrecisao2.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrecisao2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudPrecisao2.Name = "nudPrecisao2";
            this.nudPrecisao2.Size = new System.Drawing.Size(90, 24);
            this.nudPrecisao2.TabIndex = 16;
            this.nudPrecisao2.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // lbPrecisao2
            // 
            this.lbPrecisao2.AutoSize = true;
            this.lbPrecisao2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPrecisao2.Location = new System.Drawing.Point(452, 103);
            this.lbPrecisao2.Name = "lbPrecisao2";
            this.lbPrecisao2.Size = new System.Drawing.Size(58, 17);
            this.lbPrecisao2.TabIndex = 17;
            this.lbPrecisao2.Text = "Precisão:";
            // 
            // tbFuncao2
            // 
            this.tbFuncao2.Location = new System.Drawing.Point(209, 47);
            this.tbFuncao2.Name = "tbFuncao2";
            this.tbFuncao2.Size = new System.Drawing.Size(186, 24);
            this.tbFuncao2.TabIndex = 5;
            this.tbFuncao2.TextChanged += new System.EventHandler(this.tbFuncao2_TextChanged);
            // 
            // lbDigitaFunc
            // 
            this.lbDigitaFunc.AutoSize = true;
            this.lbDigitaFunc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbDigitaFunc.Location = new System.Drawing.Point(206, 27);
            this.lbDigitaFunc.Name = "lbDigitaFunc";
            this.lbDigitaFunc.Size = new System.Drawing.Size(157, 17);
            this.lbDigitaFunc.TabIndex = 6;
            this.lbDigitaFunc.Text = "Digite aqui a função f(a,b):";
            // 
            // nudNumVar
            // 
            this.nudNumVar.Location = new System.Drawing.Point(455, 47);
            this.nudNumVar.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudNumVar.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumVar.Name = "nudNumVar";
            this.nudNumVar.Size = new System.Drawing.Size(90, 24);
            this.nudNumVar.TabIndex = 3;
            this.nudNumVar.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumVar.ValueChanged += new System.EventHandler(this.nudNumVar_ValueChanged);
            // 
            // lbNumVar
            // 
            this.lbNumVar.AutoSize = true;
            this.lbNumVar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbNumVar.Location = new System.Drawing.Point(452, 27);
            this.lbNumVar.Name = "lbNumVar";
            this.lbNumVar.Size = new System.Drawing.Size(124, 17);
            this.lbNumVar.TabIndex = 2;
            this.lbNumVar.Text = "Número de variáveis:";
            // 
            // gbMetodosMulti
            // 
            this.gbMetodosMulti.Controls.Add(this.rbDavidon);
            this.gbMetodosMulti.Controls.Add(this.rbGradConjGen);
            this.gbMetodosMulti.Controls.Add(this.rbFletcher);
            this.gbMetodosMulti.Controls.Add(this.rbGradConj);
            this.gbMetodosMulti.Controls.Add(this.rbNewton2);
            this.gbMetodosMulti.Controls.Add(this.rbPassoDesc);
            this.gbMetodosMulti.Controls.Add(this.rbHookeJeeves);
            this.gbMetodosMulti.Controls.Add(this.rbCoordCicl);
            this.gbMetodosMulti.Controls.Add(this.label3);
            this.gbMetodosMulti.Controls.Add(this.label4);
            this.gbMetodosMulti.Location = new System.Drawing.Point(6, 7);
            this.gbMetodosMulti.Name = "gbMetodosMulti";
            this.gbMetodosMulti.Size = new System.Drawing.Size(182, 275);
            this.gbMetodosMulti.TabIndex = 1;
            this.gbMetodosMulti.TabStop = false;
            this.gbMetodosMulti.Text = "Métodos";
            // 
            // rbDavidon
            // 
            this.rbDavidon.AutoSize = true;
            this.rbDavidon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbDavidon.Location = new System.Drawing.Point(19, 246);
            this.rbDavidon.Name = "rbDavidon";
            this.rbDavidon.Size = new System.Drawing.Size(158, 21);
            this.rbDavidon.TabIndex = 11;
            this.rbDavidon.Text = "Davidon-Fletcher-Powell";
            this.rbDavidon.UseVisualStyleBackColor = true;
            this.rbDavidon.CheckedChanged += new System.EventHandler(this.rbDavidon_CheckedChanged);
            // 
            // rbGradConjGen
            // 
            this.rbGradConjGen.AutoSize = true;
            this.rbGradConjGen.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbGradConjGen.Location = new System.Drawing.Point(19, 165);
            this.rbGradConjGen.Name = "rbGradConjGen";
            this.rbGradConjGen.Size = new System.Drawing.Size(145, 21);
            this.rbGradConjGen.TabIndex = 10;
            this.rbGradConjGen.Text = "Grad. C. Generalizado";
            this.rbGradConjGen.UseVisualStyleBackColor = true;
            this.rbGradConjGen.CheckedChanged += new System.EventHandler(this.rbGradConjGen_CheckedChanged);
            // 
            // rbFletcher
            // 
            this.rbFletcher.AutoSize = true;
            this.rbFletcher.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbFletcher.Location = new System.Drawing.Point(19, 219);
            this.rbFletcher.Name = "rbFletcher";
            this.rbFletcher.Size = new System.Drawing.Size(120, 21);
            this.rbFletcher.TabIndex = 9;
            this.rbFletcher.Text = "Fletcher e Reeves";
            this.rbFletcher.UseVisualStyleBackColor = true;
            this.rbFletcher.CheckedChanged += new System.EventHandler(this.rbFletcher_CheckedChanged);
            // 
            // rbGradConj
            // 
            this.rbGradConj.AutoSize = true;
            this.rbGradConj.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbGradConj.Location = new System.Drawing.Point(19, 138);
            this.rbGradConj.Name = "rbGradConj";
            this.rbGradConj.Size = new System.Drawing.Size(141, 21);
            this.rbGradConj.TabIndex = 8;
            this.rbGradConj.Text = "Gradiente Conjugado";
            this.rbGradConj.UseVisualStyleBackColor = true;
            // 
            // rbNewton2
            // 
            this.rbNewton2.AutoSize = true;
            this.rbNewton2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbNewton2.Location = new System.Drawing.Point(19, 192);
            this.rbNewton2.Name = "rbNewton2";
            this.rbNewton2.Size = new System.Drawing.Size(67, 21);
            this.rbNewton2.TabIndex = 7;
            this.rbNewton2.Text = "Newton";
            this.rbNewton2.UseVisualStyleBackColor = true;
            // 
            // rbPassoDesc
            // 
            this.rbPassoDesc.AutoSize = true;
            this.rbPassoDesc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbPassoDesc.Location = new System.Drawing.Point(19, 111);
            this.rbPassoDesc.Name = "rbPassoDesc";
            this.rbPassoDesc.Size = new System.Drawing.Size(78, 21);
            this.rbPassoDesc.TabIndex = 6;
            this.rbPassoDesc.Text = "Gradiente";
            this.rbPassoDesc.UseVisualStyleBackColor = true;
            // 
            // rbHookeJeeves
            // 
            this.rbHookeJeeves.AutoSize = true;
            this.rbHookeJeeves.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbHookeJeeves.Location = new System.Drawing.Point(19, 67);
            this.rbHookeJeeves.Name = "rbHookeJeeves";
            this.rbHookeJeeves.Size = new System.Drawing.Size(108, 21);
            this.rbHookeJeeves.TabIndex = 3;
            this.rbHookeJeeves.Text = "Hooke e Jeeves";
            this.rbHookeJeeves.UseVisualStyleBackColor = true;
            // 
            // rbCoordCicl
            // 
            this.rbCoordCicl.AutoSize = true;
            this.rbCoordCicl.Checked = true;
            this.rbCoordCicl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbCoordCicl.Location = new System.Drawing.Point(19, 40);
            this.rbCoordCicl.Name = "rbCoordCicl";
            this.rbCoordCicl.Size = new System.Drawing.Size(139, 21);
            this.rbCoordCicl.TabIndex = 2;
            this.rbCoordCicl.TabStop = true;
            this.rbCoordCicl.Text = "Coordenadas Cíclicas";
            this.rbCoordCicl.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Com derivadas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sem derivadas:";
            // 
            // tabMultRes
            // 
            this.tabMultRes.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabMultRes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabMultRes.Controls.Add(this.label1);
            this.tabMultRes.Location = new System.Drawing.Point(4, 26);
            this.tabMultRes.Name = "tabMultRes";
            this.tabMultRes.Size = new System.Drawing.Size(610, 477);
            this.tabMultRes.TabIndex = 2;
            this.tabMultRes.Text = "Otimização Multivariável com Restrições";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(64, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(492, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Não implementado nesta versão!";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(630, 520);
            this.Controls.Add(this.Aba);
            this.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Programação Não Linear";
            this.Text = "Minimizador de Funções não-lineares";
            this.Aba.ResumeLayout(false);
            this.tabMono.ResumeLayout(false);
            this.tabMono.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPasso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecisao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudA)).EndInit();
            this.gbMetodos.ResumeLayout(false);
            this.gbMetodos.PerformLayout();
            this.tabMult.ResumeLayout(false);
            this.tabMult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassIter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIt2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecisao2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumVar)).EndInit();
            this.gbMetodosMulti.ResumeLayout(false);
            this.gbMetodosMulti.PerformLayout();
            this.tabMultRes.ResumeLayout(false);
            this.tabMultRes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Aba;
        private System.Windows.Forms.TabPage tabMono;
        private System.Windows.Forms.TabPage tabMult;
        private System.Windows.Forms.TabPage tabMultRes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbA;
        private System.Windows.Forms.Label lbStrIntervalo;
        private System.Windows.Forms.TextBox tbFuncao;
        private System.Windows.Forms.Label lbFuncao;
        private System.Windows.Forms.NumericUpDown nudB;
        private System.Windows.Forms.NumericUpDown nudA;
        private System.Windows.Forms.Label lbIntervalo;
        private System.Windows.Forms.Label lbPasso;
        private System.Windows.Forms.NumericUpDown nudPasso;
        private System.Windows.Forms.RichTextBox rtbCalculo;
        private System.Windows.Forms.Label lbCalculos;
        private System.Windows.Forms.Button btCalcula;
        private System.Windows.Forms.Label lbSitFunc;
        private System.Windows.Forms.NumericUpDown nudPrecisao;
        private System.Windows.Forms.Label lbPrecisao;
        private System.Windows.Forms.NumericUpDown nudMaxIt;
        private System.Windows.Forms.Label lbMaxIteracoes;
        private System.Windows.Forms.GroupBox gbMetodos;
        private System.Windows.Forms.RadioButton rbNewton;
        private System.Windows.Forms.RadioButton rbBissecao;
        private System.Windows.Forms.RadioButton rbFibonacci;
        private System.Windows.Forms.RadioButton rbSecaoAurea;
        private System.Windows.Forms.RadioButton rbDicotomica;
        private System.Windows.Forms.RadioButton rbBuscaUniforme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbSemDerivadas;
        private System.Windows.Forms.NumericUpDown nudPrecisao2;
        private System.Windows.Forms.Label lbPrecisao2;
        private System.Windows.Forms.TextBox tbFuncao2;
        private System.Windows.Forms.Label lbDigitaFunc;
        private System.Windows.Forms.NumericUpDown nudNumVar;
        private System.Windows.Forms.Label lbNumVar;
        private System.Windows.Forms.GroupBox gbMetodosMulti;
        private System.Windows.Forms.RadioButton rbNewton2;
        private System.Windows.Forms.RadioButton rbPassoDesc;
        private System.Windows.Forms.RadioButton rbHookeJeeves;
        private System.Windows.Forms.RadioButton rbCoordCicl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbPontoA;
        private System.Windows.Forms.TextBox tbPontoA;
        private System.Windows.Forms.Label lbPontoE;
        private System.Windows.Forms.TextBox tbPontoE;
        private System.Windows.Forms.Label lbPontoC;
        private System.Windows.Forms.TextBox tbPontoC;
        private System.Windows.Forms.Label lbPontoD;
        private System.Windows.Forms.TextBox tbPontoD;
        private System.Windows.Forms.Label lbPontoF;
        private System.Windows.Forms.TextBox tbPontoF;
        private System.Windows.Forms.Label lbPontoB;
        private System.Windows.Forms.TextBox tbPontob;
        private System.Windows.Forms.RichTextBox rtbCalculo2;
        private System.Windows.Forms.Label lbCalculos2;
        private System.Windows.Forms.Button btCalcula2;
        private System.Windows.Forms.NumericUpDown nudMaxIt2;
        private System.Windows.Forms.Label lbMaxIteracoes2;
        private System.Windows.Forms.Label lbPontoInicial;
        private System.Windows.Forms.RadioButton rbDavidon;
        private System.Windows.Forms.RadioButton rbGradConjGen;
        private System.Windows.Forms.RadioButton rbFletcher;
        private System.Windows.Forms.RadioButton rbGradConj;
        private System.Windows.Forms.Label lbSitFunc2;
        private System.Windows.Forms.Label lbPassIter;
        private System.Windows.Forms.NumericUpDown nudPassIter;


    }
}

