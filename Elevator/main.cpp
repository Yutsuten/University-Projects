#include <stdio.h>
#include <conio.h>
#include "elevador.h"
void imprimeErro (int erro)
{
    switch (erro)
    {
        case ERRO_CARREGAR_DLL: printf("ERRO: nao pode carregar a DLL.");  break;
        case  ERRO_LER_FUNCOES_DLL: printf("ERRO: nao pode importar funcoes da DLL."); break;
        case ELEVADOR_ENCERRADO: printf("ERRO: acesso ao porto ja encerrado."); break;
        case ERRO_VALOR_EXCEDE_LIMITE: printf("ERRO: valor atribuido e invalido."); break;
        case ERRO_DLL_NAO_CARREGADA: printf("ERRO: DLL nao carregada."); break;
        case ERRO_FUNCOES_NAO_CARREGADAS: printf("ERRO: funcoes nao importadas da DLL."); break;
        case ERRO_ACIONAMENTO_INVALIDO: printf("ERRO: codigo invalido para acionar o motor."); break;
        default: printf ("Erro: %d", erro); break;
    }
}
int main ()
{
    int status;
    GerenciadorElevador gerenciador;
    status = iniciaElevador (&gerenciador);
    if (status!=ELEVADOR_INICIADO)
    {
        imprimeErro (status);
        return status;
    }
    char leituraCabine = 0;
    char leituraAnteriorCabine = 0;
    char leituraChamada = 0;
    char leituraAnteriorChamada = 0;
    int movimento = 0;
    char leituraAndar;
    int andar;
    char caractere_digitado = '4',imprimir=0,anterior;
    do
    {
        leituraCabine = lePainelCabine(&gerenciador);
        if (leituraCabine!=leituraAnteriorCabine)
        {
            if (leituraCabine!=NADA_PRESSIONADO)
            {
                if (leituraCabine&B1_PRESSIONADO)
                {
                    if (!leLEDPainelCabine(&gerenciador, 0))
                        acendeLEDCabine(&gerenciador, 0);
                    else
                        apagaLEDCabine(&gerenciador, 0);
                }
                if (leituraCabine&B2_PRESSIONADO)
                {
                    if (!leLEDPainelCabine(&gerenciador, 1))
                        acendeLEDCabine(&gerenciador, 1);
                    else
                        apagaLEDCabine(&gerenciador, 1);
                }
                if (leituraCabine&B3_PRESSIONADO)
                {
                    if (!leLEDPainelCabine(&gerenciador, 2))
                        acendeLEDCabine(&gerenciador, 2);
                    else
                        apagaLEDCabine(&gerenciador, 2);
                }
                if (leituraCabine&B4_PRESSIONADO)
                {
                    if (!leLEDPainelCabine(&gerenciador, 3))
                        acendeLEDCabine(&gerenciador, 3);
                    else
                        apagaLEDCabine(&gerenciador, 3);
                }
                if (leituraCabine&B5_PRESSIONADO)
                {
                    if (!leLEDPainelCabine(&gerenciador, 4))
                        acendeLEDCabine(&gerenciador, 4);
                    else
                        apagaLEDCabine(&gerenciador, 4);
                }
                if (leituraCabine&B6_PRESSIONADO)
                {
                    if (!leLEDPainelCabine(&gerenciador, 5))
                        acendeLEDCabine(&gerenciador, 5);
                    else
                        apagaLEDCabine(&gerenciador, 5);
                }
                if (leituraCabine&B7_PRESSIONADO)
                {
                    if (!leLEDPainelCabine(&gerenciador, 6))
                        acendeLEDCabine(&gerenciador, 6);
                    else
                        apagaLEDCabine(&gerenciador, 6);
                }
                leituraAnteriorCabine = leituraCabine;
            }
        }
        leituraAnteriorCabine = leituraCabine;
        leituraChamada = lePainelChamada(&gerenciador);
        if (leituraChamada!=leituraAnteriorChamada)
        {
            char aux = leituraChamada^leTodosLEDPainelChamada(&gerenciador);
            atualizaPainelChamada (&gerenciador, aux);
        }
        leituraAnteriorChamada = leituraChamada;

        // Codigo de subida ou descida //

        if (movimento == 0)
            PassoMotorBaixo (&gerenciador);
        else if (movimento == 1)
            PassoMotorCima (&gerenciador);

        // Lendo o caractere que a pessoa digita //

        if (kbhit())
        {
            anterior = caractere_digitado;
            caractere_digitado = getch();
            imprimir = 1;
        }
        if ((caractere_digitado < '1' || caractere_digitado > '7') && caractere_digitado != 27)
        {
            printf ("Caractere invalido. Defalt andar 4 ativado.\n");
            imprimir = 0;
            caractere_digitado = '4';
            apagaLEDChamada (&gerenciador,anterior-48);
        }
        else if (imprimir)
        {
            imprimir = 0;
            if (caractere_digitado >= '1' && caractere_digitado <= '7') printf ("Pedido andar %c.\n",caractere_digitado);
            else printf ("Fechando programa...");
            apagaLEDChamada (&gerenciador,anterior-48);
        }

        // Lendo se o led de chamada esta aceso //



        if (leLEDPainelChamada (&gerenciador,0) == ANDAR1) caractere_digitado = '1';
        else if (leLEDPainelChamada (&gerenciador,1) == ANDAR2) caractere_digitado = '2';
        else if (leLEDPainelChamada (&gerenciador,2) == ANDAR3) caractere_digitado = '3';
        else if (leLEDPainelChamada (&gerenciador,3) == ANDAR4) caractere_digitado = '4';
        else if (leLEDPainelChamada (&gerenciador,4) == ANDAR5) caractere_digitado = '5';
        else if (leLEDPainelChamada (&gerenciador,5) == ANDAR6) caractere_digitado = '6';
        else if (leLEDPainelChamada (&gerenciador,6) == ANDAR7) caractere_digitado = '7';


        // Verificacao dos andares e movimento //

        leituraAndar = leSensorAndar(&gerenciador);
        if (leituraAndar==ANDAR1) andar = 1;
        else if (leituraAndar==ANDAR2) andar = 2;
        else if (leituraAndar==ANDAR3) andar = 3;
        else if (leituraAndar==ANDAR4) andar = 4;
        else if (leituraAndar==ANDAR5) andar = 5;
        else if (leituraAndar==ANDAR6) andar = 6;
        else if (leituraAndar==ANDAR7) andar = 7;



        // Movimentos do elevador //

        switch (andar)
        {
            case 1:
                if (caractere_digitado <= '7' && caractere_digitado >= '2')
                {
                    movimento = 1;
                }
                else if (caractere_digitado == '1') movimento = 2;
                alteraDisplay (&gerenciador, 1);
                acendeLEDChamada(&gerenciador,1);
            break;
            case 2:
                if (caractere_digitado == '1')
                {
                    movimento = 0;
                }
                else if (caractere_digitado >= '3' && caractere_digitado <= '7')
                {
                    movimento = 1;
                }
                else if (caractere_digitado == '2') movimento = 2;
                alteraDisplay (&gerenciador, 2);
                acendeLEDChamada(&gerenciador,2);
            break;
            case 3:
                if (caractere_digitado >= '1' && caractere_digitado <= '2')
                {
                    movimento = 0;
                }
                else if (caractere_digitado >= '4' && caractere_digitado <= '7')
                {
                    movimento = 1;
                }
                else if (caractere_digitado == '3') movimento = 2;
                alteraDisplay (&gerenciador, 3);
                acendeLEDChamada(&gerenciador,3);
            break;
            case 4:
                if (caractere_digitado >= '1' && caractere_digitado <= '3')
                {
                    movimento = 0;
                }
                else if (caractere_digitado >= '5' && caractere_digitado <= '7')
                {
                    movimento = 1;
                }
                else if (caractere_digitado == '4') movimento = 2;
                alteraDisplay (&gerenciador, 4);
                acendeLEDChamada(&gerenciador,4);
            break;
            case 5:
                if (caractere_digitado >= '1' && caractere_digitado <= '4')
                {
                    movimento = 0;
                }
                else if (caractere_digitado >= '6' && caractere_digitado <= '7')
                {
                    movimento = 1;
                }
                else if (caractere_digitado == '5') movimento = 2;
                alteraDisplay (&gerenciador, 5);
                acendeLEDChamada(&gerenciador,5);
            break;
            case 6:
                if (caractere_digitado >= '1' && caractere_digitado <= '5')
                {
                    movimento = 0;
                }
                else if (caractere_digitado == '7')
                {
                    movimento = 1;
                }
                else if (caractere_digitado == '6') movimento = 2;
                alteraDisplay (&gerenciador, 6);
                acendeLEDChamada(&gerenciador,6);
            break;
            case 7:
                if (caractere_digitado >= '1' && caractere_digitado <= '6')
                {
                    movimento = 0;
                }
                else if (caractere_digitado == '7') movimento = 2;
                alteraDisplay (&gerenciador, 7);
                acendeLEDChamada(&gerenciador,7);
            break;
        }
    } while (caractere_digitado != 27);
    encerraElevador (&gerenciador);
    return 0;
}
