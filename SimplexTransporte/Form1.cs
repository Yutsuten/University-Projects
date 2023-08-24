using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimplexTransporte
{
    public partial class Form1 : Form
    {
        int numOrigens, numDestinos, numTarefas, numServidores;
        const int larguraColuna = 60;

        public Form1()
        {
            InitializeComponent();
            dgTransp.Columns.Add("First", "");
            dgTransp.Columns[0].Width = larguraColuna;
            dgAloc.Columns.Add("First", "");
            dgAloc.Columns[0].Width = larguraColuna;
            for (int i = 1; i < 4; i++)
            {
                dgTransp.Columns.Add("", "");
                dgTransp.Columns[i].Width = larguraColuna;
                if (i < 3)
                {
                    dgAloc.Columns.Add("", "");
                    dgAloc.Columns[i].Width = larguraColuna;
                }
            }
            dgTransp.Rows.Add(4);
            dgTransp[0, 0].ReadOnly = true;
            dgTransp[0, 0].Style.BackColor = Color.Gray;
            cbDestino.SelectedIndex = 0;
            cbOferta.SelectedIndex = 0;

            dgAloc.Rows.Add(3);
            dgAloc[0, 0].ReadOnly = true;
            dgAloc[0, 0].Style.BackColor = Color.Gray;
            cbServ.SelectedIndex = 0;
            cbTar.SelectedIndex = 0;
        }

        #region Transporte

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgTransp[0, dgTransp.Rows.Count - 1].ReadOnly = false;
            dgTransp[0, dgTransp.Rows.Count - 1].Style.BackColor = Color.White;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].ReadOnly = false;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].Style.BackColor = Color.White;
            // ESCONDE OU NAO DETERMINADAS LINHAS DO DATAGRID
            numOrigens = cbOferta.SelectedIndex + 4;
            if (numOrigens > dgTransp.Rows.Count)
                dgTransp.Rows.Add(numOrigens - dgTransp.Rows.Count);
            else
                for (int i = dgTransp.Rows.Count - 1; i >= numOrigens; i--)
                    dgTransp.Rows.RemoveAt(i);
            dgTransp[0, dgTransp.Rows.Count - 1].ReadOnly = true;
            dgTransp[0, dgTransp.Rows.Count - 1].Value = "";
            dgTransp[0, dgTransp.Rows.Count - 1].Style.BackColor = Color.Gray;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].ReadOnly = true;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].Value = "";
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].Style.BackColor = Color.Gray;
            // ESCREVENDO UM NOME PARA AS OFERTAS
            for (int i = 1; i < numOrigens - 1; i++)
                dgTransp[0, i].Value = "Oferta" + i;
        }

        private void cbDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgTransp[dgTransp.Columns.Count - 1, 0].ReadOnly = false;
            dgTransp[dgTransp.Columns.Count - 1, 0].Style.BackColor = Color.White;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].ReadOnly = false;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].Style.BackColor = Color.White;
            // ESCONDE OU NAO DETERMINADAS COLUNAS DO DATAGRID
            numDestinos = cbDestino.SelectedIndex + 4;
            if (numDestinos >= dgTransp.Columns.Count)
                for (int i = dgTransp.Columns.Count; i < numDestinos; i++)
                {
                    dgTransp.Columns.Add("", "");
                    dgTransp.Columns[dgTransp.Columns.Count - 1].Width = larguraColuna;
                }
            else
            {
                int numColunas = dgTransp.Columns.Count;
                for (int i = numDestinos; i < numColunas; i++)
                    dgTransp.Columns.Remove("");
            }
            dgTransp[dgTransp.Columns.Count - 1, 0].ReadOnly = true;
            dgTransp[dgTransp.Columns.Count - 1, 0].Value = "";
            dgTransp[dgTransp.Columns.Count - 1, 0].Style.BackColor = Color.Gray;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].ReadOnly = true;
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].Value = "";
            dgTransp[dgTransp.Columns.Count - 1, dgTransp.Rows.Count - 1].Style.BackColor = Color.Gray;
            for (int i = 1; i < numDestinos - 1; i++)
                dgTransp[i, 0].Value = "Demanda" + i;
        }

        private void btCalc_Click(object sender, EventArgs e)
        {
            int nDestinos = numDestinos - 2, nOrigens = numOrigens - 2, numSolucao;
            float[,] quadSimp;
            float[,] quadBase;
            float[,] quadCR;
            float[] ofertas, demandas;
            String resposta;

            ofertas = new float[nOrigens];
            demandas = new float[nDestinos];
            try
            {
                for (int i = 1; i < nDestinos + 1; i++)
                    demandas[i - 1] = float.Parse(dgTransp[i, numOrigens - 1].Value.ToString());
                for (int j = 1; j < nOrigens + 1; j++)
                    ofertas[j - 1] = float.Parse(dgTransp[numDestinos - 1, j].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Caractere inválido ou campo vazio foi encontrado.\nCertifique-se de digitar somente números e não deixar nenhum campo vazio.", "Erro!");
                return;
            }

            #region verificaNoFicticio
            {
                // VERIFICANDO SE A QUANTIDADE DE OFERTAS E IGUAL AO NUMERO DE DEMANDAS
                float somaOrigens = 0, somaDestinos = 0;
                for (int i = 0; i < nOrigens; i++)
                    somaOrigens += ofertas[i];
                for (int i = 0; i < nDestinos; i++)
                    somaDestinos += demandas[i];

                // SE FOR DIFERENTE, CRIAR O NO FICTICIO
                if (somaOrigens > somaDestinos)
                {
                    MessageBox.Show("A soma de ofertas é MAIOR que de demandas.\nO programa não coloca nós fictíticios para equilibrar seu problema.\n- Coloque-os manualmente -", "Erro!");
                    return;
                }
                else if (somaOrigens < somaDestinos)
                {
                    MessageBox.Show("A soma de ofertas é MENOR que de demandas.\nO programa não coloca nós fictíticios para equilibrar seu problema.\n- Coloque-os manualmente -", "Erro!");
                    return;
                }
            }
            #endregion

            // ARRUMADAS AS OFERTAS E DEMANDAS, INSTANCIAR AS MATRIZES PARA OS CALCULOS
            quadSimp = new float[nOrigens, nDestinos];
            quadBase = new float[nOrigens, nDestinos];
            quadCR = new float[nOrigens, nDestinos];

            #region IniciaQuadSimpEQuadBase
            try
            {
                for (int i = 1; i < nOrigens + 1; i++)
                    for (int j = 1; j < nDestinos + 1; j++)
                    {
                        // PREENCHENDO QUADRO SIMPLEX
                        quadSimp[i - 1, j - 1] = float.Parse(dgTransp[j, i].Value.ToString());
                        // PREENCHENDO A BASE DE VALORES -1 (TUDO FORA DA BASE)
                        quadBase[i - 1, j - 1] = -1;
                    }
            }
            catch
            {
                MessageBox.Show("Caractere inválido ou campo vazio foi encontrado.\nCertifique-se de digitar somente números e não deixar nenhum campo vazio.", "Erro!");
            }
            #endregion

            // INICIO DOS CALCULOS
            #region CriaBase
            // CRIANDO A BASE
            {
                int i, j = 0;
                float[] auxDemanda, auxOferta;

                auxDemanda = new float[nDestinos];
                auxOferta = new float[nOrigens];
                for (i = 0; i < nDestinos; i++)
                    auxDemanda[i] = demandas[i];
                for (i = 0; i < nOrigens; i++)
                    auxOferta[i] = ofertas[i];
                i = 0;

                while (j != nDestinos && i != nOrigens)
                {
                    // AVANCAR DE LINHA
                    if (auxDemanda[j] < auxOferta[i])
                    {
                        auxOferta[i] -= auxDemanda[j];
                        quadBase[i, j] = auxDemanda[j];
                        auxDemanda[j++] = 0;
                    }
                    // AVANCAR DE COLUNA
                    else
                    {
                        auxDemanda[j] -= auxOferta[i];
                        quadBase[i, j] = auxOferta[i];
                        auxOferta[i++] = 0;
                    }
                }
            }
            #endregion

            resposta = "Foram encontradas as seguintes soluções para este problema:\n\n";
            numSolucao = 1;
            for (int iter = 0; iter < 50; iter++)
            {

                #region calculaQuadCR
                // PRRENCHENDO QUADRO DE CR
                {
                    float[] u, v;
                    int i, j, iteracoes;
                    bool denovo;

                    u = new float[nDestinos];
                    v = new float[nOrigens];
                    for (i = 0; i < nDestinos; i++)
                        u[i] = 99;
                    for (i = 0; i < nOrigens; i++)
                        v[i] = 99;
                    u[0] = 0;
                    i = j = 0;
                    iteracoes = 0;
                    do
                    {
                        denovo = false;
                        for (i = 0; i < nOrigens; i++)
                            for (j = 0; j < nDestinos; j++)
                            {
                                if (quadBase[i, j] != -1)
                                {
                                    if (u[j] != 99)
                                        v[i] = quadSimp[i, j] - u[j];
                                    else if (v[i] != 99)
                                        u[j] = quadSimp[i, j] - v[i];
                                }

                            }
                        for (i = 0; i < nDestinos; i++)
                            if (u[i] == 99)
                                denovo = true;
                        for (i = 0; i < nOrigens; i++)
                            if (v[i] == 99)
                                denovo = true;
                        iteracoes++;
                    } while (denovo && iteracoes < 10);

                    if (iteracoes == 10 && denovo)
                    {
                        MessageBox.Show("Erro no calculo do CR: Número de iterações excedido.", "Erro!");
                        return;
                    }

                    // UTILIZACAO DO U E V PARA O CALCULO DO CR
                    for (i = 0; i < nOrigens; i++)
                        for (j = 0; j < nDestinos; j++)
                            quadCR[i, j] = quadSimp[i, j] - (u[j] + v[i]);
                }
                #endregion

                #region NovaBaseEResposta
                {
                    float menorCR, menorDoCiclo;
                    bool menorDoCicloValido;
                    int x = 0, y = 0;
                    List<int[]> ciclo = new List<int[]>();

                    menorCR = 0;
                    // PROCURA POR UM CAMINHO QUE MELHORA O TRANSPORTE, ENCONTRA O NOVO CICLO E A NOVA BASE
                    for (int i = 0; i < nOrigens; i++)
                        for (int j = 0; j < nDestinos; j++)
                            if (quadBase[i, j] == -1 && quadCR[i, j] < menorCR)
                            {
                                // EXISTE UM CAMINHO MELHOR
                                x = i;
                                y = j;
                                menorCR = quadCR[i, j];
                            }
                    // SE NAO ENCONTROU UM CAMINHO MELHOR, PROCURAR CAMINHOS IGUALMENTE BONS
                    if (menorCR == 0)
                    {
                        for (int i = 0; i < nOrigens; i++)
                            for (int j = 0; j < nDestinos; j++)
                                if (quadBase[i, j] == -1 && quadCR[i, j] == menorCR)
                                {
                                    // EXISTE UM CAMINHO IGUALMENTE BOM
                                    x = i;
                                    y = j;
                                    menorCR = -1;
                                }
                        if (numSolucao < 3 && menorCR == -1)
                        {
                            resposta += "Solução " + (numSolucao++) + ":\n";
                            for (int i = 0; i < nOrigens; i++)
                                for (int j = 0; j < nDestinos; j++)
                                    if (quadBase[i, j] != -1 && quadBase[i, j] != 0)
                                        resposta += "De \"" + dgTransp[0, i + 1].Value.ToString() + "\" para \"" + dgTransp[j + 1, 0].Value.ToString() + "\" transportar " + quadBase[i, j] + " unidades.\n";
                            resposta += "\n";
                        }
                    }
                    if (menorCR == 0 || numSolucao >= 3)
                    {
                        resposta += "Solução " + (numSolucao++) + ":\n";
                        for (int i = 0; i < nOrigens; i++)
                            for (int j = 0; j < nDestinos; j++)
                                if (quadBase[i, j] != -1 && quadBase[i, j] != 0)
                                    resposta += "De \"" + dgTransp[0, i + 1].Value.ToString() + "\" para \"" + dgTransp[j + 1, 0].Value.ToString() + "\" transportar " + quadBase[i, j] + " unidades.\n";
                        // TODAS AS SOLUCOES JA FORAM ENCONTRADAS
                        MessageBox.Show(resposta,"Soluções encontradas!");
                        return;
                    }

                    // TENDO O CAMINHO QUE E MELHOR/IGUAL, PROCURAR O CICLO E MARCA-LO
                    if (!procuraLinha(x, y, x, y, quadBase, ciclo)) //ALGORITMO RECURSIVO QUE RETORNA NA LISTA "CICLO" O CICLO EXISTENTE
                    {
                        MessageBox.Show("Ciclo nao encontrado! (Problemas no agoritmo?)", "Erro!");
                    }
                    // CALCULANDO QUAL E O MENOR VALOR NA BASE QUE ESTA NO CICLO
                    menorDoCicloValido = false;
                    menorDoCiclo = -1;
                    for (int i = 0; i < ciclo.Count * 0.5; i++)
                        if (quadBase[ciclo[i * 2 + 1][0], ciclo[i * 2 + 1][1]] < menorDoCiclo || !menorDoCicloValido)
                        {
                            menorDoCicloValido = true;
                            menorDoCiclo = quadBase[ciclo[i * 2 + 1][0], ciclo[i * 2 + 1][1]];
                        }
                    // DECREMENTANDO/INCREMENTANDO DE TODO O CICLO O MENOR VALOR DA BASE ENCONTRADO
                    quadBase[x, y] = menorDoCiclo;
                    for (int i = 1; i < ciclo.Count; i++)
                    {
                        if (i % 2 == 1)
                            quadBase[ciclo[i][0], ciclo[i][1]] -= menorDoCiclo;
                        else
                            quadBase[ciclo[i][0], ciclo[i][1]] += menorDoCiclo;
                    }
                    // PROCURANDO UM VALOR NA BASE COM VALOR ZERO E RETIRA-LO DA BASE
                    for (int i = 0; i < ciclo.Count * 0.5; i++)
                        if (quadBase[ciclo[i * 2 + 1][0], ciclo[i * 2 + 1][1]] == 0)
                        {
                            quadBase[ciclo[i * 2 + 1][0], ciclo[i * 2 + 1][1]] = -1;
                            break;
                        }
                }
                #endregion
            }
            MessageBox.Show("Numero maximo de iterações para encontrar a resposta atingido!","Erro!");
        }

        // FUNCOES PARA ENCONTRAR UM CICLO NA BASE

        bool procuraLinha(int x, int y, int destX, int destY, float[,] quadBase, List<int[]> ciclo)
        {
            bool saida = false;
            for (int i = 0; i < numDestinos - 2; i++)
                if (i != y)
                    if (quadBase[x, i] != -1 || (x == destX && i == destY))
                    {
                        if (!(x == destX && i == destY))
                            saida = procuraColuna(x, i, destX, destY, quadBase, ciclo);
                        if ((x == destX && i == destY) || saida)
                        {
                            int[] vet = new int[2];
                            vet[0] = x;
                            vet[1] = i;
                            ciclo.Add(vet);
                            return true;
                        }
                    }
            return false;
        }

        bool procuraColuna(int x, int y, int destX, int destY, float[,] quadBase, List<int[]> ciclo)
        {
            bool saida = false;
            for (int i = 0; i < numOrigens - 2; i++)
                if (i != x)
                    if (quadBase[i, y] != -1 || (i == destX && y == destY))
                    {
                        if (!(i == destX && y == destY))
                            saida = procuraLinha(i, y, destX, destY, quadBase, ciclo);
                        if ((i == destX && y == destY) || saida)
                        {
                            int[] vet = new int[2];
                            vet[0] = i;
                            vet[1] = y;
                            ciclo.Add(vet);
                            return true;
                        }
                    }
            return false;
        }

        #endregion

        private void tabAlocacao_Click(object sender, EventArgs e)
        {
        }

        private void cbServ_SelectedIndexChanged(object sender, EventArgs e)
        {
            numServidores = cbServ.SelectedIndex + 3;
            if (numServidores > dgAloc.Rows.Count)
                dgAloc.Rows.Add(numServidores - dgAloc.Rows.Count);
            else
                for (int i = dgAloc.Rows.Count - 1; i >= numServidores; i--)
                    dgAloc.Rows.RemoveAt(i);
            for (int i = 1; i < numServidores; i++)
                dgAloc[0, i].Value = "Serv" + i;
        }

        private void cbTar_SelectedIndexChanged(object sender, EventArgs e)
        {
            numTarefas = cbTar.SelectedIndex + 3;
            if (numTarefas > dgAloc.Columns.Count)
                for (int i = dgAloc.Columns.Count; i < numTarefas; i++)
                {
                    dgAloc.Columns.Add("", "");
                    dgAloc.Columns[dgAloc.Columns.Count - 1].Width = larguraColuna;
                }
            else
            {
                int numColunas = dgAloc.Columns.Count;
                for (int i = numTarefas; i < numColunas; i++)
                    dgAloc.Columns.Remove("");
            }
            for (int i = 1; i < numTarefas; i++)
                dgAloc[i, 0].Value = "Tarefa" + i;
        }

        private void btCalc3_Click(object sender, EventArgs e)
        {
            int nTarefas = numTarefas - 1, nServidores = numServidores - 1, n;
            float[,] quadSimp;

            #region IniciaQuadSimp
            if (nTarefas > nServidores)
            {
                n = nTarefas;
                quadSimp = new float[nTarefas, nTarefas];
            }
            else
            {
                n = nServidores;
                quadSimp = new float[nServidores, nServidores]; 
            }

            try
            {
                for (int i = 1; i < nServidores + 1; i++)
                    for (int j = 1; j < nTarefas + 1; j++)
                        // PREENCHENDO QUADRO SIMPLEX
                        quadSimp[i - 1, j - 1] = float.Parse(dgAloc[j, i].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Caractere inválido ou campo vazio foi encontrado.\nCertifique-se de digitar somente números e não deixar nenhum campo vazio.", "Erro!");
            }
            #endregion

            float menor;
            // PERCORRENDO AS LINHAS, PROCURANDO PELO MENOR E SUBTRAINDO-O DE TODOS
            for (int i = 0; i < n; i++)
            {
                menor = quadSimp[i, 0];
                for (int j = 1; j < n; j++)
                    if (quadSimp[i, j] < menor) menor = quadSimp[i, j];
                for (int j = 0; j < n; j++)
                    quadSimp[i, j] -= menor;
            }
            // PERCORRENDO AS COLUNAS, PROCURANDO PELO MENOR E SUBTRAINDO-O DE TODOS
            for (int j = 0; j < n; j++)
            {
                menor = quadSimp[0, j];
                for (int i = 1; i < n; i++)
                    if (quadSimp[i, j] < menor) menor = quadSimp[i, j];
                for (int i = 0; i < n; i++)
                    quadSimp[i, j] -= menor;
            }

            // VERIFICAR SE TEM COMO MARCAR MENOS DE 6 LINHAS/COLUNAS, E FAZENDO UMA COPIA DO QUADRO
            for (int iteracoes = 0; iteracoes < 50; iteracoes++)
            {
                // [0] Numero de zeros - [1] Indice - [2] Coluna (1 sim, 0 nao)
                List<int[]> marcador = new List<int[]>();
                int[] traco;
                float[,] quadCopia = new float[n, n];
                int linha = 0;

                // FAZENDO UMA COPIA
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        quadCopia[i, j] = quadSimp[i, j];

                // PROCURANDO NAS LINHAS
                for (int i = 0; i < n; i++)
                {
                    traco = new int[3];
                    traco[0] = 0;
                    traco[1] = i;
                    traco[2] = 0;
                    for (int j = 0; j < n; j++)
                        if (quadCopia[i, j] == 0)
                            traco[0]++;
                    marcador.Add(traco);
                }
                // PROCURANDO NAS COLUNAS
                for (int j = 0; j < n; j++)
                {
                    traco = new int[3];
                    traco[0] = 0;
                    traco[1] = j;
                    traco[2] = 1;
                    for (int i = 0; i < n; i++)
                        if (quadCopia[i, j] == 0)
                            traco[0]++;
                    marcador.Add(traco);
                }

                // CONTANDO QUANTOS "TRACOS" SERAO FEITOS
                for (int i = n; i > 0; i--)
                {
                    for (int j = 0; j < marcador.Count; j++)
                        if (marcador[j][0] == i) // ENCONTRADO UMA LINHA/COLUNA COM MAIS ZEROS
                        {
                            linha++;
                            if (marcador[j][2] == 0) // E UMA LINHA
                                for (int k = 0; k < n; k++)
                                {
                                    if (quadCopia[marcador[j][1], k] > 0)
                                        quadCopia[marcador[j][1], k] = -1;
                                    else
                                        quadCopia[marcador[j][1], k] -= 1;
                                }
                            else // E UMA COLUNA
                                for (int k = 0; k < n; k++)
                                {
                                    if (quadCopia[k, marcador[j][1]] > 0)
                                        quadCopia[k, marcador[j][1]] = -1;
                                    else
                                        quadCopia[k, marcador[j][1]] -= 1;
                                }
                            // RECALCULANDO O MARCADOR
                            marcador.Clear();
                            // PROCURANDO NAS LINHAS
                            for (int k = 0; k < n; k++)
                            {
                                traco = new int[3];
                                traco[0] = 0;
                                traco[1] = k;
                                traco[2] = 0;
                                for (int l = 0; l < n; l++)
                                    if (quadCopia[k, l] == 0)
                                        traco[0]++;
                                marcador.Add(traco);
                            }
                            // PROCURANDO NAS COLUNAS
                            for (int l = 0; l < n; l++)
                            {
                                traco = new int[3];
                                traco[0] = 0;
                                traco[1] = l;
                                traco[2] = 1;
                                for (int k = 0; k < n; k++)
                                    if (quadCopia[k, l] == 0)
                                        traco[0]++;
                                marcador.Add(traco);
                            }
                        }
                }

                if (linha == n)
                {
                    // FIM, MOSTRAR A RESPOSTA
                    String resposta;
                    resposta = "Foram encontradas as seguintes possíveis alocações:\n\n";
                    for (int i = 0; i < nServidores; i++)
                    {
                        for (int j = 0; j < nTarefas; j++)
                        {
                            if (quadSimp[i, j] == 0)
                                resposta += dgAloc[0, i + 1].Value.ToString() + " alocado em " + dgAloc[j + 1, 0].Value.ToString() + ".\n";
                        }
                        resposta += "\n";
                    }
                    MessageBox.Show(resposta, "Soluções encontradas!");
                    return;
                }

                menor = 0;
                { // PROCURANDO O MENOR NAO RISCADO
                    bool menorNaoCalculado = true;
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            if (quadCopia[i, j] > 0 && (quadCopia[i, j] < menor || menorNaoCalculado))
                            {
                                menorNaoCalculado = false;
                                menor = quadCopia[i, j];
                            }
                }
                // DECREMENTANDO NO QUADRO SIMPLEX AS LINHAS NAO TRACEJADAS
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (quadCopia[i, j] > 0)
                            quadSimp[i, j] -= menor;
                        else if (quadCopia[i, j] == -2)
                            quadSimp[i, j] += menor;
                    }
            }
            MessageBox.Show("Numero máximo de iterações excedido.", "Erro!");
        }
    }
}
