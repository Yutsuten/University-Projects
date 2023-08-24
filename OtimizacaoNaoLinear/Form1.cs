using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mathos.Parser;

namespace Otimizacao_NaoLinear
{
    public partial class MainForm : Form
    {
        // CONSTANTES
        const decimal PRECISAO_DERIVADA = 0.00001m, PRECISAO_MONOVARIAVEL = 0.0001m; // PARA METODOS QUE USAM INDIRETAMENTE OUTROS (EX: MULTIVARIAVEL USANDO MONOVARIAVEL)
        const int MAX_ITERACOES_DERIVADA = 20;

        // VARIAVEIS GLOBAIS
        Mathos.Parser.MathParser recFunc;
        String testeEquacao;
        decimal ALFA, BETA;

        public MainForm()
        {
            InitializeComponent();
            lbIntervalo.Text = nudA.Value + " ≤ x ≤ " + nudB.Value;
            // INICIALIZANDO ALFA E BETA
            ALFA = 0.5m*(-1m + (decimal)Math.Sqrt(5));
            BETA = 1 - ALFA;
            // INICIALIZACAO DO VETOR DO PONTO INICIAL (ABA 2)
            for (int i = 0; i < 6; i++)
            {
                pontoOK[i] = true;
                ponto[i] = 0m;
            }
        }

        // CALCULOS DE FUNCAO NO PONTO, DERIVADAS PRIMEIRA E SEGUNDA
        decimal FuncaoNoPonto(String equacao, decimal x)
        {
            recFunc = new Mathos.Parser.MathParser();
            // FAZENDO DE X UMA NOVA VARIAVEL NO PARSER
            recFunc.ProgrammaticallyParse("x:=" + x);
            // CHAMANDO A FUNCAO QUE IRA RECONHECER A FUNCAO E RESOLVE-LA
            return recFunc.Parse(equacao);
        }

        // FUNCAO NO PONTO QUANDO HA MAIS DE UMA VARIAVEL
        decimal FuncaoNoPonto(String equacao, decimal[] x)
        {
            recFunc = new Mathos.Parser.MathParser();
            // FAZENDO AS NOVAS VARIAVEIS
            recFunc.ProgrammaticallyParse("a:=" + x[0]);
            recFunc.ProgrammaticallyParse("b:=" + x[1]);
            recFunc.ProgrammaticallyParse("c:=" + x[2]);
            recFunc.ProgrammaticallyParse("d:=" + x[3]);
            recFunc.ProgrammaticallyParse("e:=" + x[4]);
            recFunc.ProgrammaticallyParse("f:=" + x[5]);
            // CHAMANDO A FUNCAO QUE IRA RECONHECER A FUNCAO E RESOLVE-LA
            return recFunc.Parse(equacao);
        }

        decimal DerivadaPrimeira(String equacao, decimal x)
        {
            decimal resultadoAtual, resultadoAnterior, h = 1, pontoSomado, pontoSubtraido, erro = 10000, erroAnterior;
            int iteracao = 0;
            pontoSomado = FuncaoNoPonto(equacao, x + h);
            pontoSubtraido = FuncaoNoPonto(equacao, x - h);
            resultadoAtual = 0.5m*(pontoSomado - pontoSubtraido);
            do
            {
                resultadoAnterior = resultadoAtual;
                erroAnterior = erro;
                h *= 0.5m;
                pontoSomado = FuncaoNoPonto(equacao, x + h);
                pontoSubtraido = FuncaoNoPonto(equacao, x - h);
                resultadoAtual = (pontoSomado - pontoSubtraido) / (2*h);
                if (Math.Abs(resultadoAtual) > 1)
                    erro = Math.Abs((resultadoAtual - resultadoAnterior) / resultadoAtual);
                else
                    erro = Math.Abs(resultadoAtual - resultadoAnterior);
                iteracao += 1;
            } while (erro > PRECISAO_DERIVADA && iteracao < MAX_ITERACOES_DERIVADA && erro < erroAnterior);
            if (erro > erroAnterior)
                resultadoAtual = resultadoAnterior;
            return resultadoAtual;
        }

        decimal DerivadaSegunda(String equacao, decimal x)
        {
            decimal resultadoAtual, resultadoAnterior, h = 1, pontoSomado, pontoIntermediario, pontoSubtraido, erro = 10000, erroAnterior;
            int iteracao = 0;
            pontoSomado = FuncaoNoPonto(equacao, x + 2);
            pontoIntermediario = FuncaoNoPonto(equacao, x);
            pontoSubtraido = FuncaoNoPonto(equacao, x - 2);
            resultadoAtual = 0.25m*(pontoSomado - 2*pontoIntermediario + pontoSubtraido);
            do
            {
                resultadoAnterior = resultadoAtual;
                erroAnterior = erro;
                h *= 0.5m;
                pontoSomado = FuncaoNoPonto(equacao, x + 2*h);
                pontoSubtraido = FuncaoNoPonto(equacao, x - 2*h);
                resultadoAtual = (pontoSomado - 2*pontoIntermediario + pontoSubtraido) / (4*h*h);
                if (Math.Abs(resultadoAtual) > 1)
                    erro = Math.Abs((resultadoAtual - resultadoAnterior) / resultadoAtual);
                else
                    erro = Math.Abs(resultadoAtual - resultadoAnterior);
                iteracao += 1;
            } while (erro > PRECISAO_DERIVADA && iteracao < MAX_ITERACOES_DERIVADA && erro < erroAnterior);
            if (erro > erroAnterior)
                resultadoAtual = resultadoAnterior;
            return resultadoAtual;
        }

        // CALCULO DO VETOR GRADIENTE E DA MATRIZ HESSIANA
        decimal[] VetorGradiente(String equacao, decimal[] ponto, byte numVariaveis)
        {
            decimal[] gradiente = new decimal[numVariaveis],
                pontoSomado = new decimal[6], pontoSubtraido = new decimal[6];
            decimal h, erro, gradienteAnterior;
            int i, j, iteracao = 0;
            // CALCULANDO O A DERIVADA PARCIAL DE PRIMEIRA ORDEM DE TODAS AS VARIAVEIS
            for (i = 0; i < numVariaveis; i++) // CADA VEZ QUE AVANCAR i, SERA OUTRA VARIAVEL
            {
                // ## CALCULANDO PELA PRIMEIRA VEZ A DERIVADA PARCIAL ##
                // RESETA O h
                h = 1;
                // COPIANDO O PONTO ONDE VAI SER CALCULADO O GRADIENTE
                for (j = 0; j < numVariaveis; j++)
                    pontoSubtraido[j] = pontoSomado[j] = ponto[j];
                // COLOCANDO A SOMA/SUBTRACAO
                pontoSomado[i] += h;
                pontoSubtraido[i] -= h;
                // CALCULANDO A DERIVADA PARCIAL PELA PRIMEIRA VEZ, E COLOCANDO NO VETOR GRADIENTE
                gradiente[i] = (FuncaoNoPonto(equacao, pontoSomado) - FuncaoNoPonto(equacao, pontoSubtraido)) * 0.5m;
                do // ## ITERACOES QUE CALCULA O GRADIENTE ##
                {
                    // DIMINUI O VALOR DE h
                    h *= 0.5m;
                    // GRAVA O GRADIENTE ANTERIOR
                    gradienteAnterior = gradiente[i];
                    // RECALCULA OS NOVOS PONTOS
                    for (j = 0; j < numVariaveis; j++)
                        pontoSubtraido[j] = pontoSomado[j] = ponto[j];
                    pontoSomado[i] += h;
                    pontoSubtraido[i] -= h;
                    // RECALCULANDO O GRADIENTE
                    gradiente[i] = (FuncaoNoPonto(equacao, pontoSomado) - FuncaoNoPonto(equacao, pontoSubtraido)) / (2*h);
                    // CALCULO DO ERRO
                    erro = Math.Abs(gradienteAnterior - gradiente[i]);
                } while (erro > PRECISAO_DERIVADA && iteracao++ < MAX_ITERACOES_DERIVADA);
            } // LOOP DE TODAS AS VARIAVEIS TERMINA AQUI
            return gradiente;
        }

        decimal[,] MatrizHessiana(String equacao, decimal[] ponto, byte numVariaveis)
        {
            decimal[,] hessiano = new decimal[numVariaveis, numVariaveis];
            decimal[] pontoSomaSoma = new decimal[6], pontoSomaSubtrai = new decimal[6],
                pontoSubtraiSoma = new decimal[6], pontoSubtraiSubtrai = new decimal[6];
            decimal h, erro, hessianoAnterior, resultadoFuncaoPontoLocal;
            int i, j, k, iteracao = 0;
            // CALCULANDO A FUNCAO NO PONTO ENVIADO (PARA EVITAR CALCULOS NO MESMO PONTO)
            resultadoFuncaoPontoLocal = FuncaoNoPonto(equacao, ponto);
            // CALCULANDO A DERIVADA PARCIAL DE SEGUNDA ORDEM DE TODAS AS VARIAVEIS
            for (i = 0; i < numVariaveis; i++)
                for (j = 0; j < numVariaveis; j++) // CADA VEZ QUE AVANCAR i OU j, SERA UMA NOVA POSICAO NA MATRIZ HESSIANA
                {
                    // RESETA O VALOR DE h
                    h = 1;                    
                    if (i == j) // ALGORITMO ESPECIFICO PARA CASO SEJA CALCULADO UM VALOR NA DIAGONAL PRINCIPAL
                    {
                        // COPIANDO O PONTO ONDE VAI SER CALCULADO O HESSIANO
                        for (k = 0; k < numVariaveis; k++)
                            pontoSomaSoma[k] = pontoSubtraiSubtrai[k] = ponto[k];
                        // COLOCANDO A SOMA/SUBTRACAO
                        pontoSomaSoma[i] += 2;
                        pontoSubtraiSubtrai[i] -= 2;
                        // CALCULANDO A DERIVADA PARCIAL DE SEGUNDA ORDEM PELA PRIMEIRA VEZ, E COLOCANDO NA MATRIZ HESSIANA
                        hessiano[i, j] = (FuncaoNoPonto(equacao, pontoSomaSoma) - 2 * resultadoFuncaoPontoLocal 
                            + FuncaoNoPonto(equacao, pontoSubtraiSubtrai)) * 0.25m;
                        do // ## ITERACOES QUE CALCULA O HESSIANO ##
                        {
                            // DIMINUI O VALOR DE h
                            h *= 0.5m;
                            // GRAVA O HESSIANO ANTERIOR
                            hessianoAnterior = hessiano[i, j];
                            // RECALCULA OS NOVOS PONTOS
                            for (k = 0; k < numVariaveis; k++)
                                pontoSomaSoma[k] = pontoSubtraiSubtrai[k] = ponto[k];
                            pontoSomaSoma[i] += 2 * h;
                            pontoSubtraiSubtrai[i] -= 2 * h;
                            // RECALCULANDO O HESSIANO
                            hessiano[i, j] = (FuncaoNoPonto(equacao, pontoSomaSoma) - 2 * resultadoFuncaoPontoLocal 
                                + FuncaoNoPonto(equacao, pontoSubtraiSubtrai)) / (4 * h * h);
                            // CALCULO DO ERRO
                            erro = Math.Abs(hessianoAnterior - hessiano[i, j]);
                        } while (erro > PRECISAO_DERIVADA && iteracao++ < MAX_ITERACOES_DERIVADA);
                    }
                    else // ALGORITMO ESPECIFICO PARA VALORES FORA DA DIAGONAL PRINCIPAL
                    {
                        // COPIANDO O PONTO ONDE VAI SER CALCULADO O HESSIANO
                        for (k = 0; k < numVariaveis; k++)
                            pontoSomaSoma[k] = pontoSomaSubtrai[k] = pontoSubtraiSoma[k] = pontoSubtraiSubtrai[k] = ponto[k];
                        // COLOCANDO A SOMA/SUBTRACAO
                        pontoSomaSubtrai[i] = pontoSomaSoma[i] += 1;
                        pontoSubtraiSoma[j] = pontoSomaSoma[j] += 1;
                        pontoSubtraiSoma[i] = pontoSubtraiSubtrai[i] -= 1;
                        pontoSomaSubtrai[j] = pontoSubtraiSubtrai[j] -= 1;
                        // CALCULANDO A DERIVADA PARCIAL DE SEGUNDA ORDEM PELA PRIMEIRA VEZ, E COLOCANDO NA MATRIZ HESSIANA
                        hessiano[i, j] = (FuncaoNoPonto(equacao, pontoSomaSoma) - FuncaoNoPonto(equacao, pontoSomaSubtrai)
                            - FuncaoNoPonto(equacao, pontoSubtraiSoma) + FuncaoNoPonto(equacao, pontoSubtraiSubtrai)) * 0.25m;
                        do // ## ITERACOES QUE CALCULA O HESSIANO ##
                        {
                            // DIMINUI O VALOR DE h
                            h *= 0.5m;
                            // GRAVA O HESSIANO ANTERIOR
                            hessianoAnterior = hessiano[i, j];
                            // RECALCULA OS NOVOS PONTOS
                            for (k = 0; k < numVariaveis; k++)
                                pontoSomaSoma[k] = pontoSomaSubtrai[k] = pontoSubtraiSoma[k] = pontoSubtraiSubtrai[k] = ponto[k];
                            pontoSomaSubtrai[i] = pontoSomaSoma[i] += h;
                            pontoSubtraiSoma[j] = pontoSomaSoma[j] += h;
                            pontoSubtraiSoma[i] = pontoSubtraiSubtrai[i] -= h;
                            pontoSomaSubtrai[j] = pontoSubtraiSubtrai[j] -= h;
                            // RECALCULANDO O HESSIANO
                            hessiano[i, j] = (FuncaoNoPonto(equacao, pontoSomaSoma) - FuncaoNoPonto(equacao, pontoSomaSubtrai) 
                                - FuncaoNoPonto(equacao, pontoSubtraiSoma) + FuncaoNoPonto(equacao, pontoSubtraiSubtrai)) * (4 * h * h);
                            // CALCULO DO ERRO
                            erro = Math.Abs(hessianoAnterior - hessiano[i, j]);
                        } while (erro > PRECISAO_DERIVADA && iteracao++ < MAX_ITERACOES_DERIVADA);
                    }
                } // LOOP DAS VARIAVEIS TERMINA AQUI
            return hessiano;
        }

        // CALCULOS DE DETERMINANTE
        decimal Determinante(byte n, decimal[,] matriz)
        {
            byte i, j, k, aux;
            decimal det = 0;
            decimal[,] matrizAux = new decimal[n-1, n-1];
            if (n == 1)
                return matriz[0, 0];
            for (i = 0; i < n; i++)
            {
                for (j = 1; j < n; j++)
                {
                    aux = 0;
                    for (k = 0; k < n; k++)
                        if (i != k)
                            matrizAux[j - 1, aux++] = matriz[j, k];
                }
                det += matriz[0, i] * Determinante((byte)(n - 1), matrizAux) * ((i % 2 == 1) ? (-1) : (1));
            }
            if (Math.Abs(det) < PRECISAO_DERIVADA)
                det = 0;
            return det;
        }

        bool DeterminanteDifZero(byte n, decimal[,] matriz)
        {
            byte i;
            decimal determinante;
            // TESTA TODOS OS 'TAMANHOS' DE MATRIZ
            for (i = 1; i <= n; i++)
            {
                determinante = Determinante(i, matriz);
                if (Math.Abs(determinante) < PRECISAO_DERIVADA)
                    return false;
            }
            return true;
        }

        bool DeterminantePositivo(byte n, decimal[,] matriz)
        {
            byte i;
            decimal determinante;
            // TESTA TODOS OS 'TAMANHOS' DE MATRIZ
            for (i = 1; i <= n; i++)
            {
                determinante = Determinante(i, matriz);
                if (determinante < PRECISAO_DERIVADA) // EH NEGATIVO
                    return false;
            }
            return true;
        }

        bool MatrizSimetrica(byte n, decimal[,] matriz)
        {
            int i, j;
            for (i = 0; i < n - 1; i++)
                for (j = i + 1; j < n; j++)
                    if (matriz[i, j] != matriz[j, i])
                        return false;
            return true;
        }

        // RESOLUCAO DE SISTEMA LINEAR
        decimal[] ResolveSistemaLinear(decimal[,] matriz, decimal[] vetor, byte n)
        {
            // ALGORITMO: GAUSS COM PIVOTAMENTO PARCIAL SEM TROCA DE LINHAS
            if (DeterminanteDifZero(n, matriz))
            {
                // VARIAVEIS QUE SERAO USADAS
                byte[] posicao = new byte[n];
                byte i, j, k, maior;
                decimal soma;
                decimal[] vetor2 = new decimal[6]; // VETOR DE SOLUCAO
                // PREENCHENDO O VETOR DE POSICOES
                for (i = 0; i < n; i++) 
                    posicao[i] = i;
                // PROCURANDO A LINHA COM O MAIOR NUMERO
                for (i = 0; i < n; i++)
                {
                    //CONSIDERANDO A PRIMEIRA LINHA COMO SENDO A MAIOR
                    maior = i;
                    for (j = i; j < n; j++)
                    {
                        // VARRENDO TODAS AS LINHAS DA COLUNA, E PROCURANDO O MAIOR
                        if (Math.Abs(matriz[posicao[j], i]) > Math.Abs(matriz[posicao[maior], i])) 
                            maior = j;
                    }
                    // BLOCO DE COMANDOS COM UMA VARIAVEL AUXILIAR AUX (QUE SERA APAGADA QUANDO TERMINAR O BLOCO)
                    {
                        byte aux = posicao[i];
                        posicao[i] = posicao[maior];
                        posicao[maior] = aux;
                    }
                    // VALORES J QUE APONTARAO QUAL LINHA SERA USADA (USANDO O VETOR DE POSICOES)
                    for (j = (byte)(i + 1); j < n; j++)
                    {
                        decimal multiplicador;
                        multiplicador = matriz[posicao[j], i] / matriz[posicao[i], i];
                        // PERCORRENDO TODAS AS COLUNAS MULTIPLICANDO PELO MULTIPLICADOR, INDUZINDO ASSIM A MATRIZ A FICAR COM TUDO ZERO
                        // NA COLUNA ATUAL
                        for (k = i; k < n; k++)
                        {
                            matriz[posicao[j], k] -= multiplicador * matriz[posicao[i], k];
                        }
                        vetor[posicao[j]] -= multiplicador * vetor[posicao[i]];
                    }
                }
                // AQUI JA TEMOS A MATRIZ COMO MATRIZ TRIANGULAR SUPERIOR
                for (i = (byte)(n - 1); i != 255; i--)
                {
                    soma = 0;
                    if (i == n - 1) 
                        vetor2[i] = vetor[posicao[i]] / matriz[posicao[i], i];
                    else
                    {
                        for (j = (byte)(i + 1); j < n; j++) 
                            soma += matriz[posicao[i], j] * vetor2[j];
                        vetor2[i] = ((soma * -1) + vetor[posicao[i]]) / matriz[posicao[i], i];
                    }
                }
                return vetor2;
            }
            else //  NAO PODE RESOLVER O SISTEMA LINEAR, ERRO
            {
                // TROW EXCEPTION
                throw new System.Exception("Sistema linear não pode ser resolvido");
            }        
        }

        // MULTIPLICACAO DE VETORES, MATRIZ COM VETOR E MATRIZ COM MATRIZ
        decimal Multiplica(decimal[] vetorA, decimal[] vetorB)
        {
            int n = vetorA.Length; // TAMANHO DOS VETORES
            decimal resultado = 0;
            for (byte i = 0; i < n; i++)
                resultado += vetorA[i] * vetorB[i];
            return resultado;
        }

        decimal[] Multiplica(decimal[,] matriz, decimal[] vetor)
        {
            int n = (int)Math.Sqrt((double)matriz.Length); // TAMANHO DO VETOR E MATRIZ
            decimal[] resultado = new decimal[n];
            byte i, j;
            for (i = 0; i < n; i++)
            {
                resultado[i] = 0; // RESETA VALOR DE RESULTADO NAQUELE PONTO
                for (j = 0; j < n; j++)
                    resultado[i] += matriz[i, j] * vetor[j]; // CONTAS
            }
            return resultado;
        }

        decimal[,] Multiplica(decimal[,] matrizA, decimal[,] matrizB)
        {
            int n = (int)Math.Sqrt((double)matrizA.Length); // TAMANHO DA MATRIZ
            decimal[,] resultado = new decimal[n, n];
            byte i, j, k;
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    for (k = 0; k < n; k++)
                        resultado[i, j] += matrizA[i, k] * matrizB[k, j];
            return resultado;
        }

        // MULTIPLICACAO DE VETORES QUE RETORNA MATRIZ
        decimal[,] Multiplica2(decimal[] vetorA, decimal[] vetorB)
        {
            int n = vetorA.Length; // TAMANHO DOS VETORES
            byte i, j;
            decimal[,] matriz = new decimal[n, n];
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    matriz[i, j] = vetorA[i] * vetorB[j];
            return matriz;
        }

        #region OtimizacaoMonovariável
        // FUNCOES DA PRIMEIRA ABA
        // METODOS DO PROGRAMA - BUSCA UNIFORME, BUSCA DICOTOMICA, SECAO AUREA, FIBONACCI, BISSECAO E NEWTON

        decimal BuscaUniforme(decimal a, decimal b, decimal passo, String equacao)
        {
            decimal x = a, resultado, resultadoAnterior, otimoAtual;
            byte refinando = 0; // QUANDO ENCONTRAR UM PONTO OTIMO, VOLTAR 2 PASSOS E INCREMENTAR ESTA VARIAVEL
            int iteracao = 0;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo.Text = String.Format("{0,-4}{1,10}{2,10}{3,10}{4,10}{5,12}\n", "K", "x", "f(x)", "xk", "f(xk)","f(x)>f(xk)");
            // PRIMEIRA AVALIACAO NO PONTO
            resultado = FuncaoNoPonto(equacao, x);
            do
            {
                // INCREMENTANDO O NUMERO DE ITERACOES
                iteracao++;
                // ARMAZENANDO O VALOR DO ULTIMO CALCULO NA FUNCAO
                resultadoAnterior = resultado;
                // CONSIDERANDO X ATUAL COMO O OTIMO
                otimoAtual = x;
                // EFETUANDO O PASSO
                x += passo;
                // AVALIACAO NO PONTO
                resultado = FuncaoNoPonto(equacao, x);
                // COLOCANDO OS RESULTADOS NA JANELA DO USUARIO
                rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,12}\n", iteracao, (x - passo), resultadoAnterior, x, resultado, resultado < resultadoAnterior);
                // VERIFICANDO SE O PASSO PIOROU
                if (resultado >= resultadoAnterior)
                { // VOLTANDO 2 PASSOS E DIMINUINDO O PASSO
                    refinando += 1;
                    x -= (2 * passo);
                    passo /= 10;
                    // RECALCULA NO PONTO QUE VOLTOU
                    resultado = FuncaoNoPonto(equacao, x);
                }
            } while (x < b && refinando <= 1 && iteracao < nudMaxIt.Value);
            return otimoAtual;
        }

        decimal BuscaDicotomica(decimal a, decimal b, decimal passo, decimal precisao, String equacao)
        {
            decimal x, z, resultadoX, resultadoZ, media;
            int iteracao = 0;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo.Text = String.Format("{0,-4}{1,10}{2,10}{3,10}{4,10}{5,10}{6,10}\n", "K", "a", "b", "x", "z", "f(x)", "f(z)");
            do
            {
                // INCREMENTANDO O NUMERO DE ITERACOES
                iteracao++;
                // CALCULANDO A MEDIA E FAZENDO OS PASSOS PARA AVALIACAO NA EQUACAO
                media = 0.5m*(a + b);
                x = media - passo;
                z = media + passo;
                // AVALIANDO OS PONTOS ENCONTRADOS
                resultadoX = FuncaoNoPonto(equacao, x);
                resultadoZ = FuncaoNoPonto(equacao, z);
                // COLOCANDO OS DADOS NA JANELA DO USUARIO
                rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,10:F4}{6,10:F4}\n", iteracao, a, b, x, z, resultadoX, resultadoZ);
                // DESCARTANDO O LADO QUE NAO CONTEM O MINIMO
                if (resultadoX > resultadoZ)
                    a = x;
                else // resultadoX <= resultadoZ
                    b = z;
            } while (Math.Abs(b - a) > precisao && iteracao < nudMaxIt.Value);
            return (0.5m*(a + b));
        }

        decimal SecaoAurea(decimal a, decimal b, decimal precisao, String equacao)
        {
            decimal mi = 0, lambda = 0, resultadoMi = 0, resultadoLambda = 0;
            // ALFA E BETA SAO VARIAVEIS GLOBAIS QUE FORAM INICIALIZADAS EM MAINFORM()
            int iteracao = 1;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo.Text = String.Format("{0,-4}{1,10}{2,10}{3,10}{4,10}{5,10}{6,10}\n", "K", "a", "b", "mi", "lambda", "f(mi)", "f(lambda)");
            
            // CALCULOS DA PRIMEIRA ITERACAO - SOMENTE NELA PRECISA CALCULAR TANTO MI QUANTO LAMBDA
            mi = a + BETA * (b - a);
            lambda = a + ALFA * (b - a);
            resultadoMi = FuncaoNoPonto(equacao, mi);
            resultadoLambda = FuncaoNoPonto(equacao, lambda);
            // COLOCANDO NA JANELA
            rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,10:F4}{6,10:F4}\n", iteracao, a, b, mi, lambda, resultadoMi, resultadoLambda);
            if (resultadoMi > resultadoLambda)
                a = mi;
            else
                b = lambda;
            // INICIANDO AS PROXIMAS ITERACOES
            while (Math.Abs(b - a) > precisao && iteracao < nudMaxIt.Value)
            {   
                iteracao++;
                if (resultadoMi > resultadoLambda)
                {
                    mi = lambda;
                    resultadoMi = resultadoLambda;
                    lambda = a + ALFA * (b - a);
                    resultadoLambda = FuncaoNoPonto(equacao, lambda);
                }
                else
                {
                    lambda = mi;
                    resultadoLambda = resultadoMi;
                    mi = a + BETA * (b - a);
                    resultadoMi = FuncaoNoPonto(equacao, mi);
                }
                //COLOCANDO NA JANELA
                rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,10:F4}{6,10:F4}\n", iteracao, a, b, mi, lambda, resultadoMi, resultadoLambda);
                if (resultadoMi > resultadoLambda)
                    a = mi;
                else
                    b = lambda;
            }
            return (0.5m * (a + b));
        }

        decimal Fibonacci(decimal a, decimal b, decimal precisao, String equacao)
        {
            decimal mi = 0, lambda = 0, resultadoMi = 0, resultadoLambda = 0, Fn;
            int[] fibonacci = new int[30];
            int iteracao = 1, i = 2, k;

            // PREENCHE OS DOIS PRIMEIROS TERMOS DA SEQUENCIA
            fibonacci[0] = 1;
            fibonacci[1] = 1;
            rtbCalculo.Text = String.Format("{0,-4}{1,10}{2,10}{3,10}{4,10}{5,10}{6,10}\n", "K", "a", "b", "mi", "lambda", "f(mi)", "f(lambda)");
            // CALCULA Fn
            Fn = (b - a) / precisao;
            do
            {
                fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
                i++;
            }
            while (fibonacci[i - 1] < (int)Fn);
            // PASSA O VALOR DE i PARA k QUE IRÁ SER USADO PARA PERCORRER O VETOR DE FIBONACCI DE TRÁS PARA FRENTE
            k = i - 1;

            // CALCULOS DA PRIMEIRA ITERACAO - SOMENTE NELA PRECISA CALCULAR TANTO MI QUANTO LAMBDA
            mi = a + ((decimal)fibonacci[k - 2 - (iteracao - 1)] / (decimal)fibonacci[k - (iteracao - 1)]) * (b - a);
            lambda = a + ((decimal)fibonacci[k - 1 - (iteracao - 1)] / (decimal)fibonacci[k - (iteracao - 1)]) * (b - a);
            resultadoMi = FuncaoNoPonto(equacao, mi);
            resultadoLambda = FuncaoNoPonto(equacao, lambda);
            // COLOCANDO NA JANELA
            rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,10:F4}{6,10:F4}\n", iteracao, a, b, mi, lambda, resultadoMi, resultadoLambda);
            if (resultadoMi > resultadoLambda)
                a = mi;
            else
                b = lambda;
            // INCIANDO AS PROXIMAS ITERACOES
            while (Math.Abs(b - a) > precisao && iteracao < i - 2)
            {
                iteracao++;
                if (resultadoMi > resultadoLambda)
                {
                    mi = lambda;
                    resultadoMi = resultadoLambda;
                    lambda = a + ((decimal)fibonacci[k - 1 - (iteracao - 1)] / (decimal)fibonacci[k - (iteracao - 1)]) * (b - a);
                    resultadoLambda = FuncaoNoPonto(equacao, lambda);
                }
                else
                {
                    lambda = mi;
                    resultadoLambda = resultadoMi;
                    mi = a + ((decimal)fibonacci[k - 2 - (iteracao - 1)] / (decimal)fibonacci[k - (iteracao - 1)]) * (b - a);
                    resultadoMi = FuncaoNoPonto(equacao, mi);
                }
                //COLOCANDO NA JANELA
                rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,10:F4}{6,10:F4}\n", iteracao, a, b, mi, lambda, resultadoMi, resultadoLambda);
                if (resultadoMi > resultadoLambda)
                    a = mi;
                else
                    b = lambda;
            }
            return (0.5m * (a + b));
        }

        decimal Bissecao(decimal a, decimal b, decimal precisao, String equacao)
        {
            decimal x = a, resultadoDerPrim;
            int iteracao = 0;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo.Text = String.Format("{0,-4}{1,10}{2,10}{3,10}{4,10}\n", "K", "a", "b", "x", "f'(x)");
            do
            {
                iteracao++;
                x = (a + b) / 2;
                resultadoDerPrim = DerivadaPrimeira(equacao, x);
                if (resultadoDerPrim > 0)
                    b = x;
                else
                    a = x;
                rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}\n", iteracao, a, b, x, resultadoDerPrim);
            } while (iteracao < nudMaxIt.Value && (b - a) > precisao && iteracao < nudMaxIt.Value);
            return ((a + b) / 2);
        }

        decimal Newton(decimal a, decimal b, decimal precisao, String equacao)
        {
            decimal x = a, xAnterior, resultadoDerPrim, resultadoDerSeg, erro;
            int iteracao = 0;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo.Text = String.Format("{0,-4}{1,10}{2,10}{3,10}{4,10}{5,10}\n", "K", "x", "f'(x)", "f''(x)", "xk", "erro");
            do
            {
                // INCREMENTANDO O NUMERO DE ITERACOES
                iteracao++;
                // GRAVANDO O X DA ULTIMA ITERACAO
                xAnterior = x;
                // CALCULANDO O NOVO X
                resultadoDerPrim = DerivadaPrimeira(equacao, x);
                resultadoDerSeg = DerivadaSegunda(equacao, x);
                x = x - resultadoDerPrim / resultadoDerSeg;
                // CALCULANDO O ERRO
                if (Math.Abs(x) > 1)
                    erro = Math.Abs((xAnterior - x) / x);
                else
                    erro = Math.Abs(xAnterior - x);
                if (Math.Abs(resultadoDerPrim) < erro)
                    erro = Math.Abs(resultadoDerPrim);
                // COLOCANDO OS RESULTADOS NA JANELA DO USUARIO
                rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,10:F4}\n", iteracao, xAnterior, resultadoDerPrim, resultadoDerSeg, x, erro);
            } while (iteracao < nudMaxIt.Value && erro > precisao && iteracao < nudMaxIt.Value);
            return x;
        }

        // FUNCOES EXECUTADAS DURANTE AS SELECOES DE INFORMACOES - FRESCURAS E TAL

        private void nudPasso_ValueChanged(object sender, EventArgs e)
        {
        }

        private void rbBuscaUniforme_CheckedChanged(object sender, EventArgs e)
        {
            lbPasso.Enabled = true;
            nudPasso.Enabled = true;
            lbPrecisao.Enabled = false;
            nudPrecisao.Enabled = false;
            lbMaxIteracoes.Enabled = true;
            nudMaxIt.Enabled = true;
        }

        private void rbDicotomica_CheckedChanged(object sender, EventArgs e)
        {
            lbPasso.Enabled = true;
            nudPasso.Enabled = true;
            lbPrecisao.Enabled = true;
            nudPrecisao.Enabled = true;
            lbMaxIteracoes.Enabled = true;
            nudMaxIt.Enabled = true;
        }

        private void rbSecaoAurea_CheckedChanged(object sender, EventArgs e)
        {
            lbPasso.Enabled = false;
            nudPasso.Enabled = false;
            lbPrecisao.Enabled = true;
            nudPrecisao.Enabled = true;
            lbMaxIteracoes.Enabled = true;
            nudMaxIt.Enabled = true;
        }

        private void rbFibonacci_CheckedChanged(object sender, EventArgs e)
        {
            lbPasso.Enabled = false;
            nudPasso.Enabled = false;
            lbPrecisao.Enabled = true;
            nudPrecisao.Enabled = true;
            lbMaxIteracoes.Enabled = false;
            nudMaxIt.Enabled = false;
        }

        private void rbBissecao_CheckedChanged(object sender, EventArgs e)
        {
            lbPasso.Enabled = false;
            nudPasso.Enabled = false;
            lbPrecisao.Enabled = true;
            nudPrecisao.Enabled = true;
            lbMaxIteracoes.Enabled = true;
            nudMaxIt.Enabled = true;
        }

        private void rbNewton_CheckedChanged(object sender, EventArgs e)
        {
            lbPasso.Enabled = false;
            nudPasso.Enabled = false;
            lbPrecisao.Enabled = true;
            nudPrecisao.Enabled = true;
            lbMaxIteracoes.Enabled = true;
            nudMaxIt.Enabled = true;
        }

        private void nudA_ValueChanged(object sender, EventArgs e)
        {
            if (nudA.Value >= nudB.Value)
                nudB.Value = nudA.Value + 0.1m;
            lbIntervalo.Text = nudA.Value + " ≤ x ≤ " + nudB.Value;
        }

        private void nudB_ValueChanged(object sender, EventArgs e)
        {
            if (nudB.Value <= nudA.Value)
                nudA.Value = nudB.Value - 0.1m;
            lbIntervalo.Text = nudA.Value + " ≤ x ≤ " + nudB.Value;
        }

        private void tbFuncao_TextChanged(object sender, EventArgs e)
        {
            // TESTANDO SE A FUNCAO DIGITADA ESTA NO FORMATO CORRETO
            testeEquacao = tbFuncao.Text.ToLower();
            try
            {
                FuncaoNoPonto(testeEquacao, 2);
                if (testeEquacao.Length != 0)
                {
                    lbSitFunc.ForeColor = Color.Green;
                    lbSitFunc.Text = "* Função válida!";
                    btCalcula.Enabled = true;
                }
                else
                {
                    lbSitFunc.Text = "";
                    btCalcula.Enabled = false;
                }
            }
            catch (Exception)
            {
                lbSitFunc.ForeColor = Color.Red;
                lbSitFunc.Text = "* Função inválida!";
                btCalcula.Enabled = false;
            }            
        }

        private void btCalcula_Click(object sender, EventArgs e)
        {
            decimal resultado, resultadoNaFuncao;
            String equacao, mensagem;
            Cursor.Current = Cursors.WaitCursor;
            // ARRUMANDO A EQUACAO PARA O FORMATO CORRETO
            equacao = tbFuncao.Text.ToLower();
            try
            {
                // CHAMANDO A FUNCAO QUE CALCULA A RESPOSTA
                if (rbBuscaUniforme.Checked)
                    resultado = BuscaUniforme(nudA.Value, nudB.Value, nudPasso.Value, equacao);
                else if (rbDicotomica.Checked)
                    resultado = BuscaDicotomica(nudA.Value, nudB.Value, nudPasso.Value, nudPrecisao.Value, equacao);
                else if (rbSecaoAurea.Checked)
                    resultado = SecaoAurea(nudA.Value, nudB.Value, nudPrecisao.Value, equacao);
                else if (rbFibonacci.Checked)
                    resultado = Fibonacci(nudA.Value, nudB.Value, nudPrecisao.Value, equacao);
                else if (rbBissecao.Checked)
                    resultado = Bissecao(nudA.Value, nudB.Value, nudPrecisao.Value, equacao);
                else // SE FOR NEWTON
                    resultado = Newton(nudA.Value, nudB.Value, nudPrecisao.Value, equacao);
                // CALCULANDO O VALOR DA FUNCAO NO PONTO OTIMO
                resultadoNaFuncao = FuncaoNoPonto(equacao, resultado);
                Cursor.Current = Cursors.Default;
                // MONTANDO UMA MENSAGEM PARA ESCREVER AO USUARIO
                mensagem = String.Format("O seguinte resultado foi encontrado:\n\nx* = {0:F5}\nf(x*) = {1:F5}", resultado, resultadoNaFuncao);
                // ABRINDO UMA JANELA COM A RESPOSTA            
                MessageBox.Show(mensagem, "Resultado encontrado!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante os cálculos.\n" + ex.Message, "Erro");
            }
        }

        #endregion

        #region OtimizacaoMultivariavel
        // FUNCOES DA SEGUNDA ABA
        // VARIAVEIS GLOBAIS DAQUI
        byte numVariaveis;
        decimal[] ponto = new decimal[6];
        bool funcOK = false;
        bool[] pontoOK = new bool[6];

        decimal NewtonSemConsole(decimal a, decimal b, decimal precisao, String equacao)
        {
            decimal x = a, xAnterior, resultadoDerPrim, resultadoDerSeg, erro;
            int iteracao = 0;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            //rtbCalculo.Text = String.Format("{0,-4}{1,10}{2,10}{3,10}{4,10}{5,10}\n", "K", "x", "f'(x)", "f''(x)", "xk", "erro");
            do
            {
                // INCREMENTANDO O NUMERO DE ITERACOES
                iteracao++;
                // GRAVANDO O X DA ULTIMA ITERACAO
                xAnterior = x;
                // CALCULANDO O NOVO X
                resultadoDerPrim = DerivadaPrimeira(equacao, x);
                resultadoDerSeg = DerivadaSegunda(equacao, x);
                x = x - resultadoDerPrim / resultadoDerSeg;
                // CALCULANDO O ERRO
                if (Math.Abs(x) > 1)
                    erro = Math.Abs((xAnterior - x) / x);
                else
                    erro = Math.Abs(xAnterior - x);
                if (Math.Abs(resultadoDerPrim) < erro)
                    erro = Math.Abs(resultadoDerPrim);
                // COLOCANDO OS RESULTADOS NA JANELA DO USUARIO
                //rtbCalculo.Text += String.Format("{0,-4:D3}{1,10:F4}{2,10:F4}{3,10:F4}{4,10:F4}{5,10:F4}\n", iteracao, xAnterior, resultadoDerPrim, resultadoDerSeg, x, erro);
            } while (iteracao < nudMaxIt.Value && erro > precisao && iteracao < nudMaxIt.Value);
            return x;
        }

        decimal[] CoordenadasCiclicas(decimal[] ponto, decimal precisao, String equacao)
        {
            decimal[] alfa = new decimal[6], pontoAnterior = new decimal[6];
            decimal erro, resultado, resultadoAnterior; // ERRO DA ITERACAO
            int numVar = (int)nudNumVar.Value;
            int iteracao = 1;
            String substituiVar, equacaoTemp, stringX, stringXK;
            // MONTANDO AS DIRECOES DOS PASSOS
            decimal[,] dir = new decimal[numVar, numVar];
            for (int i = 0; i < numVar; i++)
            {
                for (int j = 0; j < numVar; j++)
                {
                    dir[i, j] = (j == i ? 1 : 0);
                }
            }
            // CALCULO DA FUNCAO NAQUELE PONTO
            resultado = FuncaoNoPonto(equacao, ponto);
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            //rtbCalculo2.Text = String.Format("{0,-4}{1,20}{2,10}{3,20}{4,10}{5,10}\n", "K", "x", "f(x)", "xk", "f(xk)", "erro");
            rtbCalculo2.Text = String.Format("{0,-4}{1,30}{2,14}{3,10}\n", "K", "xk", "f(xk)", "erro");
            do // REPETE ESTE PROCESSO ATE QUE PASSE PELO CRITÉRIO DE PARADA
            {
                for (int i = 0; i < numVar; i++)
                    pontoAnterior[i] = ponto[i];
                for (int d = 0; d < numVar; d++) // MINIMIZA A EQUACAO EM TODAS AS DIRECOES
                {
                    equacaoTemp = equacao;
                    for (int i = 0; i < numVar; i++) // SUBSTITUINDO CADA VARIAVEL PELO SEU VALOR NO CALCULO LINEAR
                    {
                        substituiVar = "(" + ponto[i] + "+x*" + dir[d, i] + ")";
                        switch (i)
                        {
                            case 0: // VARIAVEL A
                                equacaoTemp = equacaoTemp.Replace("a", substituiVar);
                                break;
                            case 1: // VARIAVEL B
                                equacaoTemp = equacaoTemp.Replace("b", substituiVar);
                                break;
                            case 2: // VARIAVEL C
                                equacaoTemp = equacaoTemp.Replace("c", substituiVar);
                                break;
                            case 3: // VARIAVEL D
                                equacaoTemp = equacaoTemp.Replace("d", substituiVar);
                                break;
                            case 4: // VARIAVEL E
                                equacaoTemp = equacaoTemp.Replace("e", substituiVar);
                                break;
                            case 5: // VARIAVEL F
                                equacaoTemp = equacaoTemp.Replace("f", substituiVar);
                                break;
                        }
                    } // SUBSTITUIU CADA VARIAVEL PELOS VALORES DA ITERACAO
                    alfa[d] = NewtonSemConsole(0, 0, PRECISAO_MONOVARIAVEL, equacaoTemp);
                    // CALCULA O NOVO PONTO
                    for (int i = 0; i < numVar; i++)
                        ponto[i] += alfa[d] * dir[d, i];
                } // FIM DOS CALCULOS EM TODAS AS DIRECOES
                // CALCULO DO ERRO
                erro = 0;
                for (int i = 0; i < numVar; i++)
                    erro += (decimal)Math.Pow((double)alfa[i], 2); // PEGANDO OS VALORES DE ALFA, FAZENDO O QUADRADO E O SEU SOMATORIO
                erro = (decimal)Math.Sqrt((double)erro);
                // COLOCANDO OS RESULTADOS NA JANELA DO USUARIO
                resultadoAnterior = resultado;
                resultado = FuncaoNoPonto(equacao, ponto);
                // PEGANDO CADA PONTO E MONTANDO A STRING
                stringX = stringXK = "";
                for (int i = 0; i < numVar; i++)
                {
                    stringX += String.Format("{0:F2}", pontoAnterior[i]);
                    stringXK += String.Format("{0:F2}", ponto[i]);
                    if (i + 1 < numVar)
                    {
                        stringX += " ";
                        stringXK += " ";
                    }
                }
                //rtbCalculo2.Text += String.Format("{0,-4:D3}{1,20}{2,10:F4}{3,20}{4,10:F4}{5,10:F4}\n", iteracao, stringX, resultadoAnterior, stringXK, resultado, erro);
                rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringXK, resultado, erro);
            } while (erro >= precisao && iteracao++ < nudMaxIt2.Value);
            return ponto;
        }

        decimal[] HookeJeeves(decimal[] ponto, decimal precisao, String equacao)
        {
            int numVar = (int)nudNumVar.Value;
            decimal[] alfa = new decimal[6], pontoAnterior = new decimal[6], dir2 = new decimal[numVar];
            decimal erro, resultado, resultadoAnterior, lambda;
            int iteracao = 1;
            bool buscaConjulgada = false;
            String substituiVar, equacaoTemp, stringX, stringXK;
            // MONTANDO AS DIRECOES DOS PASSOS
            decimal[,] dir = new decimal[numVar, numVar];
            for (int i = 0; i < numVar; i++)
            {
                for (int j = 0; j < numVar; j++)
                {
                    dir[i, j] = (j == i ? 1 : 0);
                }
            }
            // SALVANDO PELA PRIMEIRA VEZ O PONTO
            for (int i = 0; i < numVar; i++)
            {
                pontoAnterior[i] = ponto[i];
            }
            // CALCULO DA FUNCAO NAQUELE PONTO
            resultado = FuncaoNoPonto(equacao, ponto);
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            //rtbCalculo2.Text = String.Format("{0,-4}{1,20}{2,10}{3,20}{4,10}{5,10}\n", "K", "x", "f(x)", "xk", "f(xk)", "erro");
            rtbCalculo2.Text = String.Format("{0,-4}{1,30}{2,14}{3,10}\n", "K", "xk", "f(xk)", "erro");
            do // REPETE ESTE PROCESSO ATE QUE PASSE PELO CRITÉRIO DE PARADA
            {
                if (buscaConjulgada) // BUSCA CONJULGADA A PARTIR DA SEGUNDA ITERACAO
                {
                    equacaoTemp = equacao;
                    for (int i = 0; i < numVar; i++) // SUBSTITUINDO CADA VARIAVEL PELO SEU VALOR NO CALCULO LINEAR
                    {
                        substituiVar = "(" + ponto[i] + "+x*" + dir2[i] + ")";
                        switch (i)
                        {
                            case 0: // VARIAVEL A
                                equacaoTemp = equacaoTemp.Replace("a", substituiVar);
                                break;
                            case 1: // VARIAVEL B
                                equacaoTemp = equacaoTemp.Replace("b", substituiVar);
                                break;
                            case 2: // VARIAVEL C
                                equacaoTemp = equacaoTemp.Replace("c", substituiVar);
                                break;
                            case 3: // VARIAVEL D
                                equacaoTemp = equacaoTemp.Replace("d", substituiVar);
                                break;
                            case 4: // VARIAVEL E
                                equacaoTemp = equacaoTemp.Replace("e", substituiVar);
                                break;
                            case 5: // VARIAVEL F
                                equacaoTemp = equacaoTemp.Replace("f", substituiVar);
                                break;
                        }
                    } // SUBSTITUIU CADA VARIAVEL PELOS VALORES DA ITERACAO
                    lambda = NewtonSemConsole(0, 0, PRECISAO_MONOVARIAVEL, equacaoTemp);
                    // CALCULA O NOVO PONTO
                    for (int i = 0; i < numVar; i++)
                        ponto[i] += lambda * dir2[i];
                }
                else
                    buscaConjulgada = true;
                for (int d = 0; d < numVar; d++) // MINIMIZA A EQUACAO EM TODAS AS DIRECOES
                {
                    equacaoTemp = equacao;
                    for (int i = 0; i < numVar; i++) // SUBSTITUINDO CADA VARIAVEL PELO SEU VALOR NO CALCULO LINEAR
                    {
                        substituiVar = "(" + ponto[i] + "+x*" + dir[d, i] + ")";
                        switch (i)
                        {
                            case 0: // VARIAVEL A
                                equacaoTemp = equacaoTemp.Replace("a", substituiVar);
                                break;
                            case 1: // VARIAVEL B
                                equacaoTemp = equacaoTemp.Replace("b", substituiVar);
                                break;
                            case 2: // VARIAVEL C
                                equacaoTemp = equacaoTemp.Replace("c", substituiVar);
                                break;
                            case 3: // VARIAVEL D
                                equacaoTemp = equacaoTemp.Replace("d", substituiVar);
                                break;
                            case 4: // VARIAVEL E
                                equacaoTemp = equacaoTemp.Replace("e", substituiVar);
                                break;
                            case 5: // VARIAVEL F
                                equacaoTemp = equacaoTemp.Replace("f", substituiVar);
                                break;
                        }
                    } // SUBSTITUIU CADA VARIAVEL PELOS VALORES DA ITERACAO
                    alfa[d] = NewtonSemConsole(0, 0, 0.001m, equacaoTemp);
                    // CALCULA O NOVO PONTO
                    for (int i = 0; i < numVar; i++)
                        ponto[i] += alfa[d] * dir[d, i];
                } // FIM DOS CALCULOS EM TODAS AS DIRECOES
                // GRAVA O PONTO ANTERIOR
                for (int i = 0; i < numVar; i++)
                {
                    dir2[i] = ponto[i] - pontoAnterior[i];
                    pontoAnterior[i] = ponto[i];
                }
                // CALCULO DO ERRO
                erro = 0;
                for (int i = 0; i < numVar; i++)
                    erro += (decimal)Math.Pow((double)dir2[i], 2); // PEGANDO OS VALORES DE ALFA, FAZENDO O QUADRADO E O SEU SOMATORIO
                erro = (decimal)Math.Sqrt((double)erro);
                // COLOCANDO OS RESULTADOS NA JANELA DO USUARIO
                resultadoAnterior = resultado;
                resultado = FuncaoNoPonto(equacao, ponto);
                // PEGANDO CADA PONTO E MONTANDO A STRING
                stringX = stringXK = "";
                for (int i = 0; i < numVar; i++)
                {
                    stringX += String.Format("{0:F2}", pontoAnterior[i]);
                    stringXK += String.Format("{0:F2}", ponto[i]);
                    if (i + 1 < numVar)
                    {
                        stringX += " ";
                        stringXK += " ";
                    }
                }
                //rtbCalculo2.Text += String.Format("{0,-4:D3}{1,20}{2,10:F4}{3,20}{4,10:F4}{5,10:F4}\n", iteracao, stringX, resultadoAnterior, stringXK, resultado, erro);
                rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringXK, resultado, erro);
            } while (erro >= precisao && iteracao++ < nudMaxIt2.Value);
            return ponto;
        }

        decimal[] Gradiente(decimal[] ponto, decimal precisao, String equacao)
        {            
            byte numVar = (byte)nudNumVar.Value;
            decimal[] dir;
            decimal erro, alfa;
            int iteracao = 0, i;
            String equacaoTemp, substituiVar, stringX;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo2.Text = String.Format("{0,-4}{1,30}{2,14}{3,10}\n", "K", "x", "f(x)", "erro");
            // CALCULO DA PRIMEIRA DIRECAO E CALCULO DO PRIMEIRO ERRO
            dir = VetorGradiente(equacao, ponto, numVar);
            erro = 0;
            for (i = 0; i < numVar; i++)
            {
                dir[i] *= -1; // INVERTENDO A DIRECAO (QUER-SE MINIMIZAR)
                erro += dir[i] * dir[i];
            }
            erro = (decimal)Math.Sqrt((double)erro);
            // IMPRIMINDO NO CONSOLE DO USUARIO
            stringX = "";
            for (i = 0; i < numVar; i++)
            {
                stringX += String.Format("{0:F2}", ponto[i]);
                if (i + 1 < numVar)
                    stringX += " ";
            }
            rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringX, 
                FuncaoNoPonto(equacao, ponto), erro);
            while (erro > precisao && iteracao++ < nudMaxIt2.Value)
            {
                equacaoTemp = equacao;
                for (i = 0; i < numVar; i++) // SUBSTITUINDO CADA VARIAVEL PELO SEU VALOR NO CALCULO LINEAR
                {
                    substituiVar = "(" + ponto[i] + "+x*" + dir[i] + ")";
                    switch (i)
                    {
                        case 0: // VARIAVEL A
                            equacaoTemp = equacaoTemp.Replace("a", substituiVar);
                            break;
                        case 1: // VARIAVEL B
                            equacaoTemp = equacaoTemp.Replace("b", substituiVar);
                            break;
                        case 2: // VARIAVEL C
                            equacaoTemp = equacaoTemp.Replace("c", substituiVar);
                            break;
                        case 3: // VARIAVEL D
                            equacaoTemp = equacaoTemp.Replace("d", substituiVar);
                            break;
                        case 4: // VARIAVEL E
                            equacaoTemp = equacaoTemp.Replace("e", substituiVar);
                            break;
                        case 5: // VARIAVEL F
                            equacaoTemp = equacaoTemp.Replace("f", substituiVar);
                            break;
                    }
                } // SUBSTITUIU CADA VARIAVEL PELOS VALORES DA ITERACAO
                alfa = NewtonSemConsole(0, 0, PRECISAO_MONOVARIAVEL, equacaoTemp);
                // CALCULA O NOVO PONTO
                for (i = 0; i < numVar; i++)
                    ponto[i] += alfa * dir[i];
                // CALCULANDO A NOVA DIRECAO E ERRO
                dir = VetorGradiente(equacao, ponto, numVar);
                erro = 0;
                for (i = 0; i < numVar; i++)
                {
                    dir[i] *= -1; // INVERTENDO A DIRECAO (QUER-SE MINIMIZAR)
                    erro += dir[i] * dir[i];
                }
                erro = (decimal)Math.Sqrt((double)erro);
                // IMPRIMINDO NO CONSOLE DO USUARIO
                stringX = "";
                for (i = 0; i < numVar; i++)
                {
                    stringX += String.Format("{0:F2}", ponto[i]);
                    if (i + 1 < numVar)
                        stringX += " ";
                }
                rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringX, 
                    FuncaoNoPonto(equacao, ponto), erro);
            }
            return ponto;
        }

        decimal[] GradienteConjugado(decimal[] ponto, decimal precisao, String equacao)
        {
            byte numVar = (byte)nudNumVar.Value;
            int iteracao = 1, i, j;
            decimal[,] hessiano;
            decimal[] dir = new decimal[numVar], grad, b = new decimal[numVar], vetorAux = new decimal[numVar];
            decimal lambda, beta, erro;
            String stringX;
            // CONSEGUINDO OS VALORES DE 'Q' (HESSIANO)
            hessiano = MatrizHessiana(equacao, ponto, numVar);
            if (DeterminantePositivo(numVar, hessiano) && MatrizSimetrica(numVar, hessiano))
            {
                // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
                rtbCalculo2.Text = String.Format("{0,-4}{1,30}{2,14}{3,10}\n", "K", "x", "f(x)", "erro");
                // CONSEGUINDO O VALOR DE 'G' (GRADIENTE)
                grad = VetorGradiente(equacao, ponto, numVar);
                // CALCULO DA DIRECAO E 'B' A PARTIR DO GRADIENTE
                for (i = 0; i < numVar; i++)
                    b[i] = dir[i] = grad[i] * -1;
                // CALCULOS DO PRIMEIRO MOVIMENTO
                lambda = -(Multiplica(grad, dir) / Multiplica(dir, Multiplica(hessiano, dir)));
                // CALCULO DO NOVO PONTO
                for (i = 0; i < numVar; i++)
                    ponto[i] += lambda * dir[i];
                // CALCULO DO NOVO GRADIENTE E ERRO
                erro = 0;
                vetorAux = Multiplica(hessiano, ponto);
                for (i = 0; i < numVar; i++)
                {
                    grad[i] = vetorAux[i] - b[i];
                    erro += grad[i] * grad[i];
                }
                erro = (decimal)Math.Sqrt((double)erro);
                // IMPRIMINDO NO CONSOLE DO USUARIO
                stringX = "";
                for (i = 0; i < numVar; i++)
                {
                    stringX += String.Format("{0:F2}", ponto[i]);
                    if (i + 1 < numVar)
                        stringX += " ";
                }
                rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringX,
                    FuncaoNoPonto(equacao, ponto), erro);
                // INICIO DAS ITERACOES
                while (erro > precisao && iteracao++ < nudMaxIt2.Value)
                {
                    // CALCULO DO BETA - EXISTE UMA CONTA QUE EH REPETIDO TANTO NO DIVISOR QUANTO NO DIVIDENDO
                    vetorAux = Multiplica(hessiano, dir);
                    beta = Multiplica(grad, vetorAux) / Multiplica(dir, vetorAux);
                    // RECALCULA A DIRECAO 'D'
                    for (i = 0; i < numVar; i++)
                        dir[i] = -1 * grad[i] + beta * dir[i];
                    // RECALCULA O PROXIMO MOVIMENTO (LAMBDA)
                    lambda = -(Multiplica(grad, dir) / Multiplica(dir, Multiplica(hessiano, dir)));
                    // CALCULO DO NOVO PONTO
                    for (i = 0; i < numVar; i++)
                        ponto[i] += lambda * dir[i];
                    // CALCULO DO NOVO GRADIENTE E ERRO
                    erro = 0;
                    vetorAux = Multiplica(hessiano, ponto);
                    for (i = 0; i < numVar; i++)
                    {
                        grad[i] = vetorAux[i] - b[i];
                        erro += grad[i] * grad[i];
                    }
                    erro = (decimal)Math.Sqrt((double)erro);
                    // IMPRIMINDO NO CONSOLE DO USUARIO
                    stringX = "";
                    for (i = 0; i < numVar; i++)
                    {
                        stringX += String.Format("{0:F2}", ponto[i]);
                        if (i + 1 < numVar)
                            stringX += " ";
                    }
                    rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringX,
                        FuncaoNoPonto(equacao, ponto), erro);
                }
            }
            else // HESSIANO NAO EH DEFINIDO POSITIVO OU NAO EH SIMETRICO - ERRO!
            {
                throw new System.Exception("Hessiano (Q) não é simétrico ou definido positivo");
            }
            return ponto;
        }

        decimal[] GradienteConjugadoGeneralizado(decimal[] ponto, decimal precisao, String equacao)
        {
            byte numVar = (byte)nudNumVar.Value;
            int iteracao = 1, passo = 0, i, j;
            decimal[,] hessiano;
            decimal[] dir = new decimal[numVar], grad, vetorAux = new decimal[numVar];
            decimal lambda, beta, erro;
            String stringX;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo2.Text = String.Format("{0,-4}{1,-3}{2,30}{3,14}{4,10}", "K", "P", "x", "f(x)", "erro");
            // CALCULA O GRADIENTE
            grad = VetorGradiente(equacao, ponto, numVar);
            do
            {
                // CALCULO DO HESSIANO
                hessiano = MatrizHessiana(equacao, ponto, numVar);                
                for (i = 0; i < numVar; i++)
                    dir[i] = grad[i] * -1;
                passo = 0;
                // PASSOS DO ALGORITMO QUE SIMULA APROXIMACAO COM GRADIENTE CONJUGADO PURO
                do
                {
                    // CALCULOS DO PRIMEIRO MOVIMENTO
                    lambda = -(Multiplica(grad, dir) / Multiplica(dir, Multiplica(hessiano, dir)));
                    // CALCULO DO NOVO PONTO
                    for (i = 0; i < numVar; i++)
                        ponto[i] += lambda * dir[i];
                    // IMPRIMINDO NO CONSOLE DO USUARIO O NOVO PONTO
                    stringX = "";
                    for (i = 0; i < numVar; i++)
                    {
                        stringX += String.Format("{0:F2}", ponto[i]);
                        if (i + 1 < numVar)
                            stringX += " ";
                    }
                    rtbCalculo2.Text += String.Format("\n{0,-4:D3}{1,-3:D2}{2,30}{3,14:F4}", iteracao, passo + 1, stringX,
                        FuncaoNoPonto(equacao, ponto));
                    // RECALCULA 'G' (GRADIENTE)
                    grad = VetorGradiente(equacao, ponto, numVar);
                    // AVANCA O PASSO E TESTA SE VAI CONTINUAR A APROXIMACAO
                    if (++passo < nudPassIter.Value)
                    {
                        // CALCULO DO BETA - EXISTE UMA CONTA QUE EH REPETIDO TANTO NO DIVISOR QUANTO NO DIVIDENDO
                        vetorAux = Multiplica(hessiano, dir);
                        beta = (Multiplica(grad, vetorAux) / Multiplica(dir, vetorAux));
                        // RECALCULA A DIRECAO 'D'
                        for (i = 0; i < numVar; i++)
                            dir[i] = -1 * grad[i] + beta * dir[i];
                        // RECALCULA O HESSIANO PARA O PROXIMO PASSO
                        hessiano = MatrizHessiana(equacao, ponto, numVar);
                        // PARA EVITAR TRAVAMENTOS, VERIFICAR SE ERRO EH ZERO
                        erro = 0;
                        for (i = 0; i < numVar; i++)
                            erro += grad[i] * grad[i];
                        erro = (decimal)Math.Sqrt((double)erro);
                        if (erro < PRECISAO_DERIVADA)
                            break;
                    }
                    else // FIM DA APROXIMACAO, TESTA-LA AGORA
                        break;
                } while (true);
                // CALCULO DO ERRO
                erro = 0;
                for (i = 0; i < numVar; i++)
                    erro += grad[i] * grad[i];
                erro = (decimal)Math.Sqrt((double)erro);
                // IMPRIME O ERRO NO CONSOLE DO USUARIO
                rtbCalculo2.Text += String.Format("{0,10:F4}", erro);
            } while (iteracao++ < nudMaxIt2.Value && erro > precisao);
            return ponto;
        }

        decimal[] Newton2(decimal[] ponto, decimal precisao, String equacao)
        {
            byte numVar = (byte)nudNumVar.Value;
            decimal[,] hessiano;
            decimal[] dir, mov = new decimal[6];
            decimal erroDerivada, erroMovimento = 10000;
            int iteracao = 1, i;
            String stringX;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo2.Text = String.Format("{0,-4}{1,30}{2,14}{3,10}\n", "K", "x", "f(x)", "erro");
            // CALCULO DA PRIMEIRA DIRECAO, HESSIANO E CALCULO DO PRIMEIRO ERRO
            dir = VetorGradiente(equacao, ponto, numVar);
            erroDerivada = 0;
            for (i = 0; i < numVar; i++)
            {
                dir[i] *= -1; // INVERTENDO A DIRECAO (QUER-SE MINIMIZAR)
                erroDerivada += dir[i] * dir[i];
            }
            erroDerivada = (decimal)Math.Sqrt((double)erroDerivada);
            // IMPRIMINDO NO CONSOLE DO USUARIO
            stringX = "";
            for (i = 0; i < numVar; i++)
            {
                stringX += String.Format("{0:F2}", ponto[i]);
                if (i + 1 < numVar)
                    stringX += " ";
            }
            rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringX,
                FuncaoNoPonto(equacao, ponto), erroDerivada);
            // INICIO DA ITERACOES DE NEWTON
            while (erroDerivada > precisao && erroMovimento > precisao && iteracao++ < nudMaxIt2.Value)
            {
                // CALCULA O HESSIANO
                hessiano = MatrizHessiana(equacao, ponto, numVar);
                // RESOLVE SISTEMA LINEAR E DESCOBRE O DESLOCAMENTO
                mov = ResolveSistemaLinear(hessiano, dir, numVar);
                // AVANCA O PONTO PARA MAIS PROXIMO DO MINIMO ENCONTRADO, RECALCULA ERRO
                erroMovimento = 0;
                for (i = 0; i < numVar; i++)
                {
                    ponto[i] += mov[i];
                    erroMovimento += mov[i] * mov[i];
                }
                erroMovimento = (decimal)Math.Sqrt((double)erroMovimento);
                // RECALCULA DIRECAO PARA A PROXIMA ITERACAO
                dir = VetorGradiente(equacao, ponto, numVar);
                erroDerivada = 0;
                for (i = 0; i < numVar; i++)
                {
                    dir[i] *= -1; // INVERTENDO A DIRECAO (QUER-SE MINIMIZAR)
                    erroDerivada += dir[i] * dir[i];
                }
                erroDerivada = (decimal)Math.Sqrt((double)erroDerivada);
                // IMPRIMINDO NO CONSOLE DO USUARIO
                stringX = "";
                for (i = 0; i < numVar; i++)
                {
                    stringX += String.Format("{0:F2}", ponto[i]);
                    if (i + 1 < numVar)
                        stringX += " ";
                }
                rtbCalculo2.Text += String.Format("{0,-4:D3}{1,30}{2,14:F4}{3,10:F4}\n", iteracao, stringX, 
                    FuncaoNoPonto(equacao, ponto), (erroDerivada < erroMovimento) ? erroDerivada : erroMovimento);
            }
            return ponto;
        }

        decimal[] FletcherAndReeves(decimal[] ponto, decimal precisao, String equacao)
        {
            byte numVar = (byte)nudNumVar.Value;
            int iteracao = 1, numPassos = (int)nudPassIter.Value, i, j;
            decimal[] dir = new decimal[6], grad, gradAnterior = new decimal[numVar];
            decimal erro, lambda, beta;
            String substituiVar, equacaoTemp, stringX;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo2.Text = String.Format("{0,-4}{1,-3}{2,30}{3,14}{4,10}", "K", "P", "x", "f(x)", "erro");
            // CALCULO DO GRADIENTE, DIRECAO E ERRO INICIAIS
            erro = 0;
            grad = VetorGradiente(equacao, ponto, numVar);
            for (i = 0; i < numVar; i++)
            {
                dir[i] = grad[i] * -1;
                erro += grad[i] * grad[i];
            }
            erro = (decimal)Math.Sqrt((double)erro);
            // ITERACOES DO ALGORITMO
            while (iteracao++ < nudMaxIt2.Value && erro > precisao)
            {
                for (i = 0; i < numPassos; i++)
                {
                    // CALCULANDO O NOVO PONTO
                    equacaoTemp = equacao;
                    for (j = 0; j < numVar; j++) // SUBSTITUINDO CADA VARIAVEL PELO SEU VALOR NO CALCULO LINEAR
                    {
                        substituiVar = "(" + ponto[j] + "+x*" + dir[j] + ")";
                        switch (j)
                        {
                            case 0: // VARIAVEL A
                                equacaoTemp = equacaoTemp.Replace("a", substituiVar);
                                break;
                            case 1: // VARIAVEL B
                                equacaoTemp = equacaoTemp.Replace("b", substituiVar);
                                break;
                            case 2: // VARIAVEL C
                                equacaoTemp = equacaoTemp.Replace("c", substituiVar);
                                break;
                            case 3: // VARIAVEL D
                                equacaoTemp = equacaoTemp.Replace("d", substituiVar);
                                break;
                            case 4: // VARIAVEL E
                                equacaoTemp = equacaoTemp.Replace("e", substituiVar);
                                break;
                            case 5: // VARIAVEL F
                                equacaoTemp = equacaoTemp.Replace("f", substituiVar);
                                break;
                        }
                    } // SUBSTITUIU CADA VARIAVEL PELOS VALORES DA ITERACAO
                    lambda = NewtonSemConsole(0, 0, PRECISAO_MONOVARIAVEL, equacaoTemp);
                    // CALCULA O NOVO PONTO E SALVA O GRADIENTE ANTERIOR
                    for (j = 0; j < numVar; j++)
                    {
                        ponto[j] += lambda * dir[j];
                        gradAnterior[j] = grad[j];
                    }
                    // RECALCULA O GRADIENTE
                    grad = VetorGradiente(equacao, ponto, numVar);
                    // IMPRIMINDO NO CONSOLE DO USUARIO O NOVO PONTO
                    stringX = "";
                    for (j = 0; j < numVar; j++)
                    {
                        stringX += String.Format("{0:F2}", ponto[j]);
                        if (j + 1 < numVar)
                            stringX += " ";
                    }
                    rtbCalculo2.Text += String.Format("\n{0,-4:D3}{1,-3:D2}{2,30}{3,14:F4}", iteracao, i + 1, stringX,
                        FuncaoNoPonto(equacao, ponto));
                    // SE NAO FOR A ULTIMA ITERACAO DOS PASSOS, RECALCULAR DIRECAO
                    if (i + 1 < numPassos)
                    { 
                        // CALCULO DO BETA
                        beta = Multiplica(grad, grad) / Multiplica(gradAnterior, gradAnterior);
                        // RECALCULA A DIRECAO
                        for (j = 0; j < numVar; j++)
                            dir[j] = -1 * grad[j] + beta * dir[j];
                        // PARA EVITAR TRAVAMENTOS, VERIFICAR SE ERRO EH ZERO
                        erro = 0;
                        for (j = 0; j < numVar; j++)
                            erro += grad[j] * grad[j];
                        erro = (decimal)Math.Sqrt((double)erro);
                        if (erro < PRECISAO_DERIVADA)
                            break;
                    }
                } // FIM LOOP QUE CONTA OS PASSOS
                // RECALCULA O ERRO
                erro = 0;
                for (i = 0; i < numVar; i++)
                    erro += grad[i] * grad[i];
                erro = (decimal)Math.Sqrt((double)erro);
                // IMPRIME O ERRO NO CONSOLE DO USUARIO
                rtbCalculo2.Text += String.Format("{0,10:F4}", erro);
            } // FIM LOOP QUE VERIFICA ERRO
            return ponto;
        }

        decimal[] DavidonFletcherPowell(decimal[] ponto, decimal precisao, String equacao)
        {
            byte numVar = (byte)nudNumVar.Value;
            int iteracao = 0, passo = 0, i, j, numPassos = (int)nudPassIter.Value;
            decimal[,] S = new decimal[numVar, numVar], matrizAux = new decimal[numVar, numVar], matrizAux2 = new decimal[numVar, numVar];
            decimal[] grad, dir, gradAnterior = new decimal[numVar], q = new decimal[numVar], p = new decimal[numVar];
            decimal erro, alfa, divisor, divisor2;
            String substituiVar, equacaoTemp, stringX;
            // IMPRIMINDO NA JANELA DO USUARIO O CABECALHO DOS CALCULOS
            rtbCalculo2.Text = String.Format("{0,-4}{1,-3}{2,30}{3,14}{4,10}", "K", "P", "x", "f(x)", "erro");
            // CALCULO INICIAL DO GRADIENTE
            grad = VetorGradiente(equacao, ponto, numVar);
            // CALCULO DO ERRO
            erro = 0;
            for (i = 0; i < numVar; i++)
                erro += grad[i] * grad[i];
            erro = (decimal)Math.Sqrt((double)erro);
            // INICIA MATRIZ SIMETRICA E DEFINIDA POSITIVA
            for (i = 0; i < numVar; i++)
                for (j = 0; j < numVar; j++)
                    S[i, j] = (j == i ? 1 : 0);
            // IMPRIMINDO NO CONSOLE DO USUARIO O NOVO PONTO
            stringX = "";
            for (j = 0; j < numVar; j++)
            {
                stringX += String.Format("{0:F2}", ponto[j]);
                if (j + 1 < numVar)
                    stringX += " ";
            }
            rtbCalculo2.Text += String.Format("\n{0,-4:D3}{1,-3:D2}{2,30}{3,14:F4}{4,10:F4}", iteracao, passo + 1, stringX,
                FuncaoNoPonto(equacao, ponto), erro);
            while (iteracao++ < nudMaxIt2.Value && erro > precisao)
            {
                // CALCULANDO A NOVA DIRECAO
                dir = Multiplica(S, grad);
                for (i = 0; i < numVar; i++)
                    dir[i] *= -1;
                // CALCULANDO O NOVO PONTO
                equacaoTemp = equacao;
                for (i = 0; i < numVar; i++) // SUBSTITUINDO CADA VARIAVEL PELO SEU VALOR NO CALCULO LINEAR
                {
                    substituiVar = "(" + ponto[i] + "+x*" + dir[i] + ")";
                    switch (i)
                    {
                        case 0: // VARIAVEL A
                            equacaoTemp = equacaoTemp.Replace("a", substituiVar);
                            break;
                        case 1: // VARIAVEL B
                            equacaoTemp = equacaoTemp.Replace("b", substituiVar);
                            break;
                        case 2: // VARIAVEL C
                            equacaoTemp = equacaoTemp.Replace("c", substituiVar);
                            break;
                        case 3: // VARIAVEL D
                            equacaoTemp = equacaoTemp.Replace("d", substituiVar);
                            break;
                        case 4: // VARIAVEL E
                            equacaoTemp = equacaoTemp.Replace("e", substituiVar);
                            break;
                        case 5: // VARIAVEL F
                            equacaoTemp = equacaoTemp.Replace("f", substituiVar);
                            break;
                    }
                } // SUBSTITUIU CADA VARIAVEL PELOS VALORES DA ITERACAO
                alfa = NewtonSemConsole(0, 0, PRECISAO_MONOVARIAVEL, equacaoTemp);
                // CALCULA O NOVO PONTO
                for (i = 0; i < numVar; i++)
                    ponto[i] += alfa * dir[i];
                // CASO AINDA ESTEJA DENTRO DE UMA ITERACAO
                if (++passo < numPassos)
                {
                    // GRAVA O GRADIENTE ANTERIOR
                    for (i = 0; i < numVar; i++)
                        gradAnterior[i] = grad[i];
                    // RECALCULA O GRADIENTE
                    grad = VetorGradiente(equacao, ponto, numVar);
                    // CALCULOS DE 'Q' E 'P'
                    for (i = 0; i < numVar; i++)
                    {
                        q[i] = grad[i] - gradAnterior[i];
                        p[i] = alfa * dir[i];
                    }
                    // RECALCULANDO 'S'
                    matrizAux = Multiplica2(p, p); // RETORNA UMA MATRIZ DA MULTIPLICACAO DE P COM P
                    matrizAux2 = Multiplica(Multiplica2(Multiplica(S, q), q), S); // MATRIZ x MATRIZ
                    divisor = Multiplica(p, q);
                    divisor2 = Multiplica(Multiplica(S, q), q);
                    // COLOCANDO A RESPOSTA EM 'S'
                    for (i = 0; i < numVar; i++)
                        for (j = 0; j < numVar; j++)
                            S[i, j] += matrizAux[i, j] / divisor - matrizAux2[i, j] / divisor2;
                }
                else // 'RESETA' OS PASSOS
                {
                    grad = VetorGradiente(equacao, ponto, numVar);
                    passo = 0;
                }
                // RECALCULA O ERRO
                erro = 0;
                for (i = 0; i < numVar; i++)
                    erro += grad[i] * grad[i];
                erro = (decimal)Math.Sqrt((double)erro);
                // IMPRIMINDO NO CONSOLE DO USUARIO O NOVO PONTO
                stringX = "";
                for (j = 0; j < numVar; j++)
                {
                    stringX += String.Format("{0:F2}", ponto[j]);
                    if (j + 1 < numVar)
                        stringX += " ";
                }
                rtbCalculo2.Text += String.Format("\n{0,-4:D3}{1,-3:D2}{2,30}{3,14:F4}{4,10:F4}", iteracao, passo + 1, stringX,
                    FuncaoNoPonto(equacao, ponto), erro);
            }
            return ponto;
        }

        private void checaValidade()
        {
            if (funcOK && pontoOK[0] && pontoOK[1] && pontoOK[2] && pontoOK[3] && pontoOK[4] && pontoOK[5])
                btCalcula2.Enabled = true;
            else
                btCalcula2.Enabled = false;
        }

        private void tbFuncao2_TextChanged(object sender, EventArgs e)
        {
            testeEquacao = tbFuncao2.Text.ToLower();
            testeEquacao = testeEquacao.Replace('x', 'z');
            testeEquacao = testeEquacao.Replace('a', 'x');
            testeEquacao = testeEquacao.Replace('b', 'x');
            switch ((int)nudNumVar.Value)
            {
                case 3:
                    testeEquacao = testeEquacao.Replace('c', 'x');
                    break;
                case 4:
                    testeEquacao = testeEquacao.Replace('c', 'x');
                    testeEquacao = testeEquacao.Replace('d', 'x');
                    break;
                case 5:
                    testeEquacao = testeEquacao.Replace('c', 'x');
                    testeEquacao = testeEquacao.Replace('d', 'x');
                    testeEquacao = testeEquacao.Replace('e', 'x');
                    break;
                case 6:
                    testeEquacao = testeEquacao.Replace('c', 'x');
                    testeEquacao = testeEquacao.Replace('d', 'x');
                    testeEquacao = testeEquacao.Replace('e', 'x');
                    testeEquacao = testeEquacao.Replace('f', 'x');
                    break;
            }      
            try
            {
                FuncaoNoPonto(testeEquacao, 2);
                if (testeEquacao.Length != 0)
                {
                    lbSitFunc2.ForeColor = Color.Green;
                    lbSitFunc2.Text = "* Função válida!";
                    funcOK = true;
                }
                else
                {
                    lbSitFunc2.Text = "";
                    funcOK = false;
                }
            }
            catch
            {
                lbSitFunc2.ForeColor = Color.Red;
                lbSitFunc2.Text = "* Função inválida!";
                funcOK = false;
            }
            checaValidade();
        }

        private void nudNumVar_ValueChanged(object sender, EventArgs e)
        {
            numVariaveis = (byte)nudNumVar.Value;

            tbPontoC.Enabled = false;
            tbPontoD.Enabled = false;
            tbPontoE.Enabled = false;
            tbPontoF.Enabled = false;            
            switch (numVariaveis)
            {
                case 2:
                    lbDigitaFunc.Text = "Digite aqui a função f(a,b):";
                    break;
                case 3:
                    lbDigitaFunc.Text = "Digite aqui a função f(a,b,c):";
                    tbPontoC.Enabled = true;
                    break;
                case 4:
                    lbDigitaFunc.Text = "Digite aqui a função f(a,b,c,d):";
                    tbPontoC.Enabled = true;
                    tbPontoD.Enabled = true;
                    break;
                case 5:
                    lbDigitaFunc.Text = "Digite aqui a função f(a,b,c,d,e):";
                    tbPontoC.Enabled = true;
                    tbPontoD.Enabled = true;
                    tbPontoE.Enabled = true;
                    break;
                case 6:
                    lbDigitaFunc.Text = "Digite aqui a função f(a,b,c,d,e,f):";
                    tbPontoC.Enabled = true;
                    tbPontoD.Enabled = true;
                    tbPontoE.Enabled = true;
                    tbPontoF.Enabled = true;
                    break;
            }
        }

        private void tbPontoA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ponto[0] = Decimal.Parse(tbPontoA.Text);
                pontoOK[0] = true;
            }
            catch
            {
                pontoOK[0] = false;
            }
            checaValidade();
        }

        private void tbPontob_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ponto[1] = Decimal.Parse(tbPontob.Text);
                pontoOK[1] = true;
            }
            catch
            {
                pontoOK[1] = false;
            }
            checaValidade();
        }

        private void tbPontoC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ponto[2] = Decimal.Parse(tbPontoC.Text);
                pontoOK[2] = true;
            }
            catch
            {
                pontoOK[2] = false;
            }
            checaValidade();
        }

        private void tbPontoD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ponto[3] = Decimal.Parse(tbPontoD.Text);
                pontoOK[3] = true;
            }
            catch
            {
                pontoOK[3] = false;
            }
            checaValidade();
        }

        private void tbPontoE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ponto[4] = Decimal.Parse(tbPontoE.Text);
                pontoOK[4] = true;
            }
            catch
            {
                pontoOK[4] = false;
            }
            checaValidade();
        }

        private void tbPontoF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ponto[5] = Decimal.Parse(tbPontoF.Text);
                pontoOK[5] = true;
            }
            catch
            {
                pontoOK[5] = false;
            }
            checaValidade();
        }

        private void btCalcula2_Click(object sender, EventArgs e)
        {
            decimal resultadoNaFuncao;
            decimal[] resultado = new decimal[6];
            int numVar = (int)nudNumVar.Value;
            String equacao, mensagem, stringResultado;
            Cursor.Current = Cursors.WaitCursor;
            // ARRUMANDO A EQUACAO PARA O FORMATO CORRETO
            equacao = tbFuncao2.Text.ToLower();
            try
            {
                ponto[0] = int.Parse(tbPontoA.Text);
                ponto[1] = int.Parse(tbPontob.Text);
                ponto[2] = int.Parse(tbPontoC.Text);
                ponto[3] = int.Parse(tbPontoD.Text);
                ponto[4] = int.Parse(tbPontoE.Text);
                ponto[5] = int.Parse(tbPontoF.Text);
                // CHAMANDO A FUNCAO QUE CALCULA A RESPOSTA
                if (rbCoordCicl.Checked)
                {
                    resultado = CoordenadasCiclicas(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                else if (rbHookeJeeves.Checked)
                {
                    resultado = HookeJeeves(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                else if (rbPassoDesc.Checked)
                {
                    resultado = Gradiente(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                else if (rbGradConj.Checked)
                {
                    resultado = GradienteConjugado(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                else if (rbGradConjGen.Checked)
                {
                    resultado = GradienteConjugadoGeneralizado(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                else if (rbNewton2.Checked)
                {
                    resultado = Newton2(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                else if (rbFletcher.Checked)
                {
                    resultado = FletcherAndReeves(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                else // DAVIDON-FLETCHER-POWELL
                {
                    resultado = DavidonFletcherPowell(ponto, (decimal)nudPrecisao2.Value, equacao);
                }
                // CALCULANDO O VALOR DA FUNCAO NO PONTO OTIMO
                resultadoNaFuncao = FuncaoNoPonto(equacao, resultado);
                Cursor.Current = Cursors.Default;
                // MONTANDO UMA MENSAGEM PARA ESCREVER AO USUARIO
                stringResultado = "";
                for (int i = 0; i < numVar; i++)
                {
                    stringResultado += String.Format("{0:F4}", resultado[i]);
                    if (i + 1 < numVar)
                    {
                        stringResultado += " ";
                    }
                }
                mensagem = String.Format("O seguinte resultado foi encontrado:\n\nx* = {0:F5}\nf(x*) = {1:F5}", stringResultado, resultadoNaFuncao);
                // ABRINDO UMA JANELA COM A RESPOSTA            
                MessageBox.Show(mensagem, "Resultado encontrado!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante os cálculos.\nERRO: " + ex.Message, "Erro");
            }
        }

        #endregion

        private void rbGradConjGen_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGradConjGen.Checked)
            {
                lbMaxIteracoes2.Text = "Max Iter:";
                lbPassIter.Visible = true;
                nudPassIter.Visible = true;
            }
            else
            {
                lbMaxIteracoes2.Text = "Máximo de Iterações:";
                lbPassIter.Visible = false;
                nudPassIter.Visible = false;
            }
        }

        private void rbFletcher_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFletcher.Checked)
            {
                lbMaxIteracoes2.Text = "Max Iter:";
                lbPassIter.Visible = true;
                nudPassIter.Visible = true;
            }
            else
            {
                lbMaxIteracoes2.Text = "Máximo de Iterações:";
                lbPassIter.Visible = false;
                nudPassIter.Visible = false;
            }
        }

        private void rbDavidon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDavidon.Checked)
            {
                lbMaxIteracoes2.Text = "Max Iter:";
                lbPassIter.Visible = true;
                nudPassIter.Visible = true;
            }
            else
            {
                lbMaxIteracoes2.Text = "Máximo de Iterações:";
                lbPassIter.Visible = false;
                nudPassIter.Visible = false;
            }
        }

    }
}
