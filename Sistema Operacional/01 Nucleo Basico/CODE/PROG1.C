#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <nucleo.h>

FILE *ARQ;

void far Tic()
{
    int i, j;
    i = 0;
    while(i++ < 240)
    {
        fprintf(ARQ, "TIC ");
        for (j = 0; j < 1000; j++)
        {
            j -= 3;
            j += 1;
            j += 1;
            j += 1;
        }
    }
    Terminar_Processo();
}

void far Tac()
{
    int i, j;
    i = 0;
    while(i++ < 160)
    {
        fprintf(ARQ, "TAC ");
        for (j = 0; j < 1000; j++)
        {
            j -= 3;
            j += 1;
            j += 1;
            j += 1;
        }
    }
    Terminar_Processo();
}

void far Toc()
{
    int i, j;
    i = 0;
    while(i++ < 200)
    {
        fprintf(ARQ, "TOC ");
        for (j = 0; j < 1000; j++)
        {
            j -= 3;
            j += 1;
            j += 1;
            j += 1;
        }
    }
    Terminar_Processo();
}

int main ()
{
    if ((ARQ = fopen("CODE\\SAIDA.TXT","r+t")) == NULL)
        printf("\nErro na abertura do arquivo.\n\n");
    else
        printf("\nArquivo aberto com sucesso. A saida estara em SAIDA.TXT.\n\n");
    Criar_Processo(0,Tic);
    Criar_Processo(1,Toc);
    Criar_Processo(2,Tac);
    Dispara_Sistema();
    return 0;
}
