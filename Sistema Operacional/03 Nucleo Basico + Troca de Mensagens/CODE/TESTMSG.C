#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include "C:\PROGS\NUCLEO\NUCLEO.H"

FILE *ARQ;

void far P1()
{
    int i,test;
    i = 0;
    while(i < 5000)
    {
        test = Envia(1,"teste");
        if(test == 2)
        {
            i++;
            printf("mensagem enviada %d\n",i);
        }
        else
        {
            printf("mensagem nao enviada: ");
            if(test == 0)
            {
                printf("destino nao encontrado\n");
                Terminar_Processo();
            }
            else
            {
                printf("fila cheia\n");
            }
        }

    }
    Terminar_Processo();
}

void far P2()
{
    int emissor;
    char str[25];
    int i;
    i = 0;
    while(i < 5000)
    {
        Recebe(&emissor,str);
        i++;
        printf("mensagem n§:%d\nmensagem recebida de: %d\nmensagem: %s\n",i,emissor,str);
    }
    Terminar_Processo();
}


void far Tic()
{
    int i;
    i = 0;
    while(i++ < 2400)
    {
        fprintf(ARQ, "TIC ");
    }
    Terminar_Processo();
}

void far Tac()
{
    int i;
    i = 0;
    while(i++ < 1600)
    {
        fprintf(ARQ, "TAC ");
    }
    Terminar_Processo();
}

void far Toc()
{
    int i;
    i = 0;
    while(i++ < 2000)
    {
        fprintf(ARQ, "TOC ");
    }
    Terminar_Processo();
}

int main ()
{
    if ((ARQ = fopen("C:\\PROGS\\NUCLEO\\CODE\\SAIDA.TXT","r+t")) == NULL)
        printf("\nErro na abertura do arquivo.\n\n");
    else
        printf("\nArquivo aberto com sucesso. A saida estara em SAIDA.TXT.\n\n");
    Criar_Processo(0,P1,10);
    Criar_Processo(1,P2,10);
    Criar_Processo(2,Tic,0);
    Criar_Processo(3,Tac,0);
    Criar_Processo(4,Toc,0);
    Dispara_Sistema();
    return 0;
}
