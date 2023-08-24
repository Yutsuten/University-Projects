using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex
{
    public partial class jnPrincipal : Form
    {
        // VARIAVEIS GLOBAIS //

        int numVariaveis = 2, numRestricoes = 2;
        DataGridViewComboBoxCell MeuComboBox;
        List<float[]> respostaX = new List<float[]>();
        float menorZ;
        bool encontradoZ;

        // FIM DAS VARIAVEIS GLOBAIS //

        public jnPrincipal()
        {
            InitializeComponent();
            foreach (DataGridViewColumn column in dgObj.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            foreach (DataGridViewColumn column in dgRes.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < dgObj.ColumnCount; i++)
                dgObj.Columns[i].Width = 87;
            cbVar.SelectedIndex = 6;
            cbRes.SelectedIndex = 4;
            dgObj.Rows.Add(3);
            for (int i = 0; i < dgObj.ColumnCount; i++)
            {
                dgObj[i, 1] = new DataGridViewComboBoxCell();
                MeuComboBox = (DataGridViewComboBoxCell)this.dgObj[i, 1];
                MeuComboBox.DropDownWidth = MeuComboBox.Size.Width;
                MeuComboBox.FlatStyle = FlatStyle.Flat;
                MeuComboBox.Items.Add("Positiva");
                MeuComboBox.Items.Add("Neg/Pos");
                MeuComboBox.Value = "Positiva";

                dgObj[i, 2] = new DataGridViewComboBoxCell();
                MeuComboBox = (DataGridViewComboBoxCell)this.dgObj[i, 2];
                MeuComboBox.FlatStyle = FlatStyle.Flat;
                MeuComboBox.Items.Add("Real");
                MeuComboBox.Items.Add("Inteira");
                MeuComboBox.DropDownWidth = MeuComboBox.Size.Width;
                MeuComboBox.Value = "Real";
            }
        }

        private void cbVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ESCONDE OU NAO DETERMINADAS COLUNAS DO DATAGRID
            numVariaveis = cbVar.SelectedIndex + 2;
            for (int i = 2; i < 8; i++)
            {
                dgObj.Columns[i].Visible = i < numVariaveis;
                dgRes.Columns[i].Visible = i < numVariaveis;
            }
        }

        private void cbRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            numRestricoes = cbRes.SelectedIndex + 2;
            dgRes.Rows.Clear();
            dgRes.Rows.Add(numRestricoes);
        }

        private void btCalc_Click(object sender, EventArgs e)
        {
            int numVarNeg, retorno, aux2;
            int[,] varNeg;
            float[,] restricao;
            float[] objetivo;
            float maiorPeso = -1;
            String resposta;

            DateTime begin;
            TimeSpan difference;
            begin = DateTime.Now;

            respostaX.Clear();
            menorZ = 0;
            encontradoZ = false;
            
            #region PreencheVetorVariaveisNegativas
            {
                aux2 = numVariaveis;
                numVarNeg = 0;
                // CONTANDO O NUMERO DE VARIAVEIS NEGATIVAS
                for (int i = 0; i < numVariaveis; i++)
                    if (dgObj[i, 1].Value.ToString() == "Neg/Pos")
                        numVarNeg++;
                varNeg = new int[numVarNeg, 2];
                // PREENCHENDO O VETOR DE VARIAVEIS NEGATIVAS
                for (int i = 0; i < numVariaveis; i++)
                    if (dgObj[i, 1].Value.ToString() == "Neg/Pos")
                    {
                        varNeg[i, 0] = i;
                        varNeg[i, 1] = aux2++;
                    }
            }
            #endregion
            #region Objetivo_e_Restricao
            {
                objetivo = new float[numVariaveis + numVarNeg];
                restricao = new float[numRestricoes, numVariaveis + numVarNeg + 2];
                maiorPeso = -1;

                #region PreencheObjetivo
                try
                {
                    aux2 = 0;
                    for (int i = 0; i < numVariaveis; i++)
                    {
                        objetivo[i] = float.Parse(dgObj[i, 0].Value.ToString());
                        // CASO DE VARIAVEL QUE PODE SER NEGATIVA
                        if (aux2 < numVarNeg)
                            if (i == varNeg[aux2, 0])
                                objetivo[varNeg[aux2++, 1]] = -1 * objetivo[i];
                        if (maiorPeso < Math.Abs(objetivo[i]))
                            maiorPeso = Math.Abs(objetivo[i]);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Existem valores vazios na função objetivo, ou com caracteres inválidos.\nCorrija por favor.", "Erro!");
                    return;
                }
                #endregion

                #region PreencheRestricao
                try
                {
                    for (int i = 0; i < numRestricoes; i++)
                    {
                        // PEGANDO OS PRIMEIROS VALORES
                        int j;
                        aux2 = 0;
                        for (j = 0; j < numVariaveis; j++)
                        {
                            restricao[i, j] = float.Parse(dgRes[j, i].Value.ToString());
                            // CASO DE VARIAVEL QUE PODE SER NEGATIVA
                            if (aux2 < numVarNeg)
                                if (j == varNeg[aux2, 0])
                                    restricao[i, varNeg[aux2++, 1]] = -(restricao[i, j]);
                        }
                        // PEGANDO O SINAL E Y
                        if (dgRes[8, i].Value.ToString() == "<" || dgRes[8, i].Value.ToString() == "<=")
                            restricao[i, numVariaveis + numVarNeg] = -1;
                        else if (dgRes[8, i].Value.ToString() == "=" || dgRes[8, i].Value.ToString() == "==")
                            restricao[i, numVariaveis + numVarNeg] = 0;
                        else if (dgRes[8, i].Value.ToString() == ">" || dgRes[8, i].Value.ToString() == ">=")
                            restricao[i, numVariaveis + numVarNeg] = 1;
                        else // SE FOR NUMERO MESMO (-1; 0; 1)
                            restricao[i, numVariaveis + numVarNeg] = float.Parse(dgRes[8, i].Value.ToString());
                        restricao[i, numVariaveis + numVarNeg + 1] = float.Parse(dgRes[9, i].Value.ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Existem valores vazios ou com caracteres inválidos nas restrições.", "Erro!");
                    return;
                }
                #endregion
            }
            #endregion

            retorno = CalculoSimplex(numVarNeg, varNeg, restricao, objetivo, maiorPeso, numRestricoes);

            difference = DateTime.Now - begin;
            resposta = "(" + (int)difference.TotalMilliseconds + "ms) ";
            if (retorno == 3)
                MessageBox.Show(resposta + "A solução do problema está no infinito!", "Solução ilimitada!");
            else if (retorno == 2)
                MessageBox.Show(resposta + "Nao existe solução para este problema.", "Sem solução!");
            else
            {
                // MOSTRAR TODAS AS SOLUCOES ENCONTRADAS
                float z = 0;
                int numeroSolucao = 1;
                aux2 = 0;

                resposta += "Foram encontradas as seguintes soluções para este problema:\n";
                for (int j = 0; j < respostaX.Count; j++)
                {
                    resposta += "Solucao " + (numeroSolucao++) + ":\n";
                    z = 0;
                    for (int i = 0; i < numVariaveis; i++)
                    {
                        // COLOCANDO NA VARIAVEL DE RESPOSTA
                        resposta += "x" + (i + 1) + " = " + respostaX[j][i] + "\n";
                        z += respostaX[j][i] * objetivo[i];
                    }
                    resposta += "\n";
                    if (j > 8)
                    {
                        resposta += "Existem mais soluções, porem o programa irá parar aqui.";
                        break;
                    }
                }
                resposta += "z = " + z + "\n";
                MessageBox.Show(resposta, "Soluções encontradas!");
            }
        }

        int CalculoSimplex(int numVarNeg, int[,] varNeg, float[,] restricao, float[] objetivo, float maiorPeso, int nRestricoes)
        {
            float[] vetorSolucao = new float[numVariaveis];
            float[,] quadSimp;
            float atualZ;
            bool solucaoInteira = true, solucaoJaExiste;
            int multMin = 1, totVariaveis, retorno;
            List<float[]> solucoesX = new List<float[]>();

            // LIMPANDO A LISTA DE SOLUCOES
            solucoesX.Clear();

            if (rbMax.Checked)
                multMin = -1;

            int aux, i2 = 0, auxRes1 = 2, auxRes2 = 2; // coluna = 1 ou -1, uma edicao por linha, e acesso correto a matriz restricao

            // VENDO SE ALGUM SINAL DEVE SER TROCADO
            for (int i = 0; i < nRestricoes; i++)
                if (restricao[i, numVariaveis + numVarNeg + 1] < 0)
                    for (int j = 0; j < numVariaveis + numVarNeg + 2; j++)
                        restricao[i, j] *= -1;
            // CONTANDO QUANTAS VARIAVEIS AUXILIARES DEVERAO SER CRIADAS
            totVariaveis = numVariaveis + numVarNeg;
            for (int i = 0; i < nRestricoes; i++)
            {
                // SINAL DE MENOR OU IGUAL
                if (restricao[i, numVariaveis + numVarNeg] <= 0)
                    totVariaveis++;
                else // SINAL DE MAIOR
                    totVariaveis += 2;
            }
            // INSTANCIACAO DO QUADRO SIMPLEX
            quadSimp = new float[nRestricoes + 2, totVariaveis + 3];
            aux = numVariaveis + numVarNeg + 2; // Coluna no quadro que comecam as variaves de folga e artificial

            #region PreencheQuadroSimplex
            // PREENCHENDO O QUADRO SIMPLEX
            for (int i = 0; i <= nRestricoes; i++)
            {
                for (int j = 2; j < totVariaveis + 2; j++)
                {
                    // LADO ESQUERDO DA TABELA
                    if (j < numVariaveis + numVarNeg + 2)
                    {
                        // PRIMEIRA LINHA
                        if (i == 0)
                            quadSimp[i, j] = objetivo[j - 2] * multMin;
                        else
                            quadSimp[i, j] = restricao[i - 1, j - 2];
                    }
                    else // LADO DIREITO DA TABELA
                    {
                        // PRIMEIRA LINHA
                        if (i == 0)
                        {
                            // SINAL NEGATIVO
                            if (restricao[j - numVariaveis - numVarNeg - auxRes1, numVariaveis + numVarNeg] < 0)
                            {
                                quadSimp[i, j] = 0;
                            }
                            // SINAL DE IGUAL
                            else if (restricao[j - numVariaveis - numVarNeg - auxRes1, numVariaveis + numVarNeg] == 0)
                            {
                                quadSimp[i, j] = maiorPeso * 100;
                            }
                            // SINAL DE MAIOR
                            else
                            {
                                quadSimp[i, j++] = 0;
                                quadSimp[i, j] = maiorPeso * 100;
                                auxRes1++;
                            }
                        }
                        else // NAO E A PRIMEIRA LINHA DA TABELA
                        {
                            if (j == aux && i != i2) // COLUNA EM QUE ENTRA UM NUMERO DIFERENTE DE ZERO
                            {
                                // SINAL NEGATIVO
                                if (restricao[j - numVariaveis - numVarNeg - auxRes2, numVariaveis + numVarNeg] < 0)
                                {
                                    quadSimp[i, j] = 1;
                                    aux++;
                                }
                                // SINAL DE IGUAL
                                else if (restricao[j - numVariaveis - numVarNeg - auxRes2, numVariaveis + numVarNeg] == 0)
                                {
                                    quadSimp[i, j] = 1;
                                    aux++;
                                }
                                // SINAL DE MAIOR
                                else
                                {
                                    quadSimp[i, j++] = -1;
                                    quadSimp[i, j] = 1;
                                    aux += 2;
                                    auxRes2++;
                                }
                                i2 = i; // AVISANDO QUE ESTA LINHA JA FOI MARCADA COM 1 OU -1
                            }
                            else // COLUNA QUE ENTRA ZERO
                                quadSimp[i, j] = 0;
                        }
                    }
                }
            }
            // ATUALIZANDO BASE E Y
            aux = numVariaveis + numVarNeg + 1;
            for (int i = 1; i <= nRestricoes; i++)
            {
                if (restricao[i - 1, numVariaveis + numVarNeg] <= 0) // NEGATIVO OU IGUAL
                    quadSimp[i, 0] = ++aux;
                else // POSITIVO
                {
                    aux += 2;
                    quadSimp[i, 0] = aux;
                }
                quadSimp[i, 1] = quadSimp[0, (int)quadSimp[i, 0]];
                quadSimp[i, totVariaveis + 2] = restricao[i - 1, numVariaveis + numVarNeg + 1];
            }
            // ATUALIZANDO O CR
            for (int i = 2; i < totVariaveis + 2; i++)
            {
                quadSimp[nRestricoes + 1, i] = quadSimp[0, i];
                for (int j = 1; j <= nRestricoes; j++)
                    quadSimp[nRestricoes + 1, i] -= quadSimp[j, 1] * quadSimp[j, i];
            }
            // FIM DO PREENCHIMENTO DO QUADRO SIMPLEX
            #endregion

            retorno = ResolveSimplex(quadSimp, maiorPeso, totVariaveis, multMin, numVarNeg, varNeg, nRestricoes, solucoesX);

            // CASO ENCONTRE UMA OU MAIS SOLUCOES
            if (retorno == 1 || retorno == 2)
            {
                for (int j = 0; j < solucoesX.Count; j++)
                {
                    // ENCONTRANDO O VALOR DE Z DESTA SOLUCAO E VENDO SE AS VARIAVEIS INTEIRAS SAO INTEIRAS
                    atualZ = 0; 
                    for (int i = 0; i < numVariaveis; i++)
                        atualZ += solucoesX[j][i] * quadSimp[0, i + 2];
                    for (int i = 0; i < numVariaveis; i++)
                    {
                        // SE A VARIAVEL TIVER QUE SER INTEIRA E NAO FOR, FAZER MAIS ITERACOES - E TER Z O MENOR QUE O DA MELHOR SOLUCAO JA ENCONTRADA
                        if (dgObj[i, 2].Value.ToString() == "Inteira" && solucoesX[j][i] % 1 != 0)
                        {
                            solucaoInteira = false;
                            // SE VALER A PENA IR NAQUELE CAMINHO
                            if (!encontradoZ || menorZ >= atualZ)
                            {
                                // ADICIONA 2 NOVAS RESTRICOES EM 2 SIMPLEX DIFERENTES
                                float[,] restricao1, restricao2;
                                restricao1 = new float[nRestricoes + 1, numVariaveis + numVarNeg + 2];
                                for (int k = 0; k < nRestricoes; k++)
                                    for (int l = 0; l < numVariaveis + numVarNeg + 2; l++)
                                        restricao1[k, l] = restricao[k, l];
                                restricao1[nRestricoes, i] = 1;
                                // COLOCANDO RESTRICAO DO LIMITE SUPERIOR, COMO MAIOR OU IGUAL
                                restricao1[nRestricoes, numVariaveis + numVarNeg] = 1;
                                if (solucoesX[j][i] > 0)
                                    restricao1[nRestricoes, numVariaveis + numVarNeg + 1] = (float)Math.Truncate(solucoesX[j][i]) + 1;
                                else
                                    restricao1[nRestricoes, numVariaveis + numVarNeg + 1] = (float)Math.Truncate(solucoesX[j][i]);
                                retorno = CalculoSimplex(numVarNeg, varNeg, restricao1, objetivo, maiorPeso, nRestricoes + 1);
                                if (retorno == 3)
                                    return 3;
                                // AGORA O SEGUNDO SIMPLEX
                                restricao2 = new float[nRestricoes + 1, numVariaveis + numVarNeg + 2];
                                for (int k = 0; k < nRestricoes; k++)
                                    for (int l = 0; l < numVariaveis + numVarNeg + 2; l++)
                                        restricao2[k, l] = restricao[k, l];
                                restricao2[nRestricoes, i] = 1;
                                // COLOCANDO RESTRICAO DO LIMITE INFERIOR, COMO MAIOR OU IGUAL
                                restricao2[nRestricoes, numVariaveis + numVarNeg] = -1;
                                if (solucoesX[j][i] > 0)
                                    restricao2[nRestricoes, numVariaveis + numVarNeg + 1] = (float)Math.Truncate(solucoesX[j][i]);
                                else
                                    restricao2[nRestricoes, numVariaveis + numVarNeg + 1] = (float)Math.Truncate(solucoesX[j][i]) - 1;
                                // ADICIONAR NA ULTIMA LINHA A NOVA RESTRICAO
                                retorno = CalculoSimplex(numVarNeg, varNeg, restricao2, objetivo, maiorPeso, nRestricoes + 1);
                                if (retorno == 3)
                                    return 3;
                            }
                        }
                    }
                    // CASO DE TER ENCONTRADO SOLUCOES VALIDAS (INTEIRAS PARA AS VARIAVEIS PEDIDAS)
                    if ((!encontradoZ || menorZ >= atualZ) && solucaoInteira)
                    {
                        int igual = 0;
                        // SE A SOLUCAO ENCONTRADA FOR MELHOR QUE OUTRA JA ENCONTRADA, LIMPA O VETOR DE RESPOSTAS
                        if (menorZ > atualZ)
                            respostaX.Clear();
                        // ATUALIZA O MENOR Z ENCONTRADO
                        menorZ = atualZ;
                        encontradoZ = true;
                        solucaoJaExiste = false;
                        // GRAVA NA LISTA ESTA SOLUCAO INTEIRA OTIMA
                        for (int i = 0; i < respostaX.Count && !solucaoJaExiste; i++) 
                        {
                            for (int k = 0; k < numVariaveis; k++)
                                if (respostaX[i][k] == solucoesX[j][k])
                                    igual++;
                            if (igual == numVariaveis)
                                solucaoJaExiste = true;
                        }
                        if (!solucaoJaExiste)
                            respostaX.Add(solucoesX[j]);
                    }
                }
            }

            // CASO SEJA ENCONTRADO UMA SOLUCAO NO INFINITO
            if (retorno == 3)
                return 3;

            if (encontradoZ)
                return 1;
            else
                return 2;
        }

        int ResolveSimplex(float[,] quadSimp, float maiorPeso, int totVariaveis, int multMin, int numVarNeg, int[,] varNeg, int nRestricoes, List<float[]> solucoesX)
        {
            float menor, menorVertice, pivo;
            int indiceMenorVertice, menorJ, numeroSolucao;

            // CALCULOS DO SIMPLEX
            for (int iteracao = 0; iteracao < 50; iteracao ++)
            {
                // PROCURANDO O CRj MAIS NEGATIVO
                menor = 0;
                menorJ = -1;
                for (int j = 2; j < totVariaveis + 2; j++)
                    if (quadSimp[nRestricoes + 1, j] < menor)
                    {
                        menor = quadSimp[nRestricoes + 1, j];
                        menorJ = j;
                    }
                // NAO EXISTE CRj NEGATIVO - ULTIMA ITERACAO
                if (menorJ == -1)
                {
                    // VERIFICANDO SE EXISTE VARIAVEL ARTIFICIAL NA BASE COM VALOR NAO NULO
                    for (int i = 1; i <= nRestricoes; i++)
                    {
                        if (quadSimp[i, 1] == 100 * maiorPeso && quadSimp[i, totVariaveis + 2] != 0)
                            return 4;
                    }
                    // SOLUCAO ENCONTRADA - MOSTRAR A SOLUCAO
                    int aux2 = 0;
                    float[] solucao = new float[totVariaveis];
                    for (int i = 1; i <= nRestricoes; i++)
                    {
                        // SE FOR UMA DAS VARIAVEIS REAIS
                        solucao[(int)quadSimp[i, 0] - 2] = quadSimp[i, totVariaveis + 2];
                    }
                    numeroSolucao = 1;
                    for (int i = 0; i < numVariaveis; i++)
                    {
                        // SE FOR VARIAVEL QUE PODE SER NEGATIVA
                        if (aux2 < numVarNeg)
                            if (i == varNeg[aux2, 0])
                                solucao[i] -= solucao[varNeg[aux2++, 1]];
                    }
                    #region gravaSolucao
                    {
                        float[] copiaSolucao = new float[numVariaveis];
                        for (int i = 0; i < numVariaveis; i++)
                            copiaSolucao[i] = solucao[i];
                        solucoesX.Add(copiaSolucao);
                    }
                    #endregion
                    // MARCANDO AS VARIAVEIS QUE JA ESTAO NA BASE
                    bool[] naBase = new bool[totVariaveis];
                    for (int i = 0; i < totVariaveis; i++)
                        naBase[i] = false;
                    for (int i = 1; i <= nRestricoes; i++)
                        naBase[(int)quadSimp[i, 0] - 2] = true;
                    // VERIFICANDO SE É A ULTIMA SOLUCAO OU SE TEM MAIS QUE UMA
                    for (int j = 2; j < totVariaveis + 2; j++)
                    {
                        if (quadSimp[nRestricoes + 1, j] == 0 && quadSimp[0, j] != 100 * maiorPeso)
                        { // HÁ MAIS DE UMA SOLUCAO
                            if (naBase[j - 2]) // JA FOI MARCADO COMO NA BASE
                                continue;
                            else
                            {
                                menorJ = j;
                                naBase[j - 2] = true;
                            }
                            // CALCULANDO OS PONTOS QUE AS EQUACOES DE RESTRICAO SE CRUZAM
                            menorVertice = -1;
                            indiceMenorVertice = -1;
                            for (int i = 1; i <= nRestricoes; i++)
                            {
                                // DIVISAO RESULTA EM INFINITO, OU E NEGATIVO
                                if (quadSimp[i, menorJ] <= 0)
                                    continue;
                                if (quadSimp[i, totVariaveis + 2] / quadSimp[i, menorJ] < menorVertice || menorVertice == -1)
                                {
                                    menorVertice = quadSimp[i, totVariaveis + 2] / quadSimp[i, menorJ];
                                    indiceMenorVertice = i;
                                }
                            }
                            if (menorVertice == -1)
                                continue;
                            // ENTRA O MENOR J NO LUGAR DA LINHA COM MENOR DIVISAO (indiceMenorVertice)
                            quadSimp[indiceMenorVertice, 0] = menorJ;
                            quadSimp[indiceMenorVertice, 1] = quadSimp[0, (int)quadSimp[indiceMenorVertice, 0]];
                            // PIVOTANDO
                            pivo = quadSimp[indiceMenorVertice, menorJ];
                            for (int i = 2; i < totVariaveis + 3; i++)
                            {
                                quadSimp[indiceMenorVertice, i] /= pivo;
                            }
                            // OBTENDO A MATRIZ IDENTIDADE NA BASE
                            for (int i = 1; i <= nRestricoes; i++)
                            {
                                pivo = quadSimp[i, menorJ];
                                if (i != indiceMenorVertice)
                                    for (int j2 = 2; j2 < totVariaveis + 3; j2++)
                                        quadSimp[i, j2] -= pivo * quadSimp[indiceMenorVertice, j2];
                            }
                            // ATUALIZANDO O CR
                            for (int i = 2; i < totVariaveis + 2; i++)
                            {
                                quadSimp[nRestricoes + 1, i] = quadSimp[0, i];
                                for (int j2 = 1; j2 <= nRestricoes; j2++)
                                    quadSimp[nRestricoes + 1, i] -= quadSimp[j2, 1] * quadSimp[j2, i];
                            }
                            // ZERANDO O VETOR DE SOLUCAO
                            for (int i = 0; i < totVariaveis; i++)
                                solucao[i] = 0;
                            // SOLUCAO ENCONTRADA - MOSTRAR A SOLUCAO
                            for (int i = 1; i <= nRestricoes; i++)
                            {
                                // SE FOR UMA DAS VARIAVEIS REAIS
                                solucao[(int)quadSimp[i, 0] - 2] = quadSimp[i, totVariaveis + 2];
                            }
                            aux2 = 0;
                            for (int i = 0; i < numVariaveis; i++)
                            {
                                // SE FOR VARIAVEL QUE PODE SER NEGATIVA
                                if (aux2 < numVarNeg)
                                    if (i == varNeg[aux2, 0])
                                        solucao[i] -= solucao[varNeg[aux2++, 1]];
                            }
                            #region gravaSolucao
                            {
                                float[] copiaSolucao = new float[numVariaveis];
                                for (int i = 0; i < numVariaveis; i++)
                                    copiaSolucao[i] = solucao[i];
                                solucoesX.Add(copiaSolucao);
                            }
                            #endregion
                        }
                    }
                    if (numeroSolucao == 1)
                        return 1;
                    else
                        return 2;
                }
                // CALCULANDO OS PONTOS QUE AS EQUACOES DE RESTRICAO SE CRUZAM
                menorVertice = -1;
                indiceMenorVertice = -1;
                for (int i = 1; i <= nRestricoes; i++)
                {
                    // DIVISAO RESULTA EM INFINITO, OU E NEGATIVO
                    if (quadSimp[i, menorJ] <= 0)
                        continue;
                    if (quadSimp[i, totVariaveis + 2] / quadSimp[i, menorJ] < menorVertice || menorVertice == -1)
                    {
                        menorVertice = quadSimp[i, totVariaveis + 2] / quadSimp[i, menorJ];
                        indiceMenorVertice = i;
                    }
                }
                // SOLUCAO ILIMITADA OU NAO TEM SOLUCAO
                if (menorVertice == -1)
                {
                    // VERIFICANDO SE EXISTE VARIAVEL ARTIFICIAL NA BASE COM VALOR NAO NULO
                    for (int i = 1; i <= nRestricoes; i++)
                    {
                        if (quadSimp[i, 1] == 100 * (Math.Abs(maiorPeso)) && quadSimp[i, totVariaveis + 2] != 0)
                        {
                            return 4;
                        }
                    }
                    // SE NAO E O CASO DE QUE NAO TEM SOLUCAO, ENTAO:
                    return 3;
                }
                // ENTRA O MENOR J NO LUGAR DA LINHA COM MENOR DIVISAO (indiceMenorVertice)
                quadSimp[indiceMenorVertice, 0] = menorJ;
                quadSimp[indiceMenorVertice, 1] = quadSimp[0, (int)quadSimp[indiceMenorVertice, 0]];
                // PIVOTANDO
                pivo = quadSimp[indiceMenorVertice, menorJ];
                for (int i = 2; i < totVariaveis + 3; i++)
                {
                    quadSimp[indiceMenorVertice, i] /= pivo;
                }
                // OBTENDO A MATRIZ IDENTIDADE NA BASE
                for (int i = 1; i <= nRestricoes; i++)
                {
                    pivo = quadSimp[i, menorJ];
                    if (i != indiceMenorVertice)
                        for (int j = 2; j < totVariaveis + 3; j++)
                            quadSimp[i, j] -= pivo * quadSimp[indiceMenorVertice, j];
                }
                // ATUALIZANDO O CR
                for (int i = 2; i < totVariaveis + 2; i++)
                {
                    quadSimp[nRestricoes + 1, i] = quadSimp[0, i];
                    for (int j = 1; j <= nRestricoes; j++)
                        quadSimp[nRestricoes + 1, i] -= quadSimp[j, 1] * quadSimp[j, i];
                    // CONSIDERACAO DE ERROS EM APROXIMACAO - NO CASO DE TENDER A ZERO E DEVERIA SER ZERO, AFETA O PROGRAMA
                    if (quadSimp[nRestricoes + 1, i] > 0 && quadSimp[nRestricoes + 1, i] < 0.00001)
                        quadSimp[nRestricoes + 1, i] = 0;
                }
            }
            return 0;
        }
    }
}
