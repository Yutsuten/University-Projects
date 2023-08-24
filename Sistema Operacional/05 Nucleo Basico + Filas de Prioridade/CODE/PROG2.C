#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <nucleo.h>

FILE *ARQ;

void far Tic()
{
    int i;
    i = 0;
    while(i++ < 5400)
    {
        fprintf(ARQ, "TIC ");
    }
    Terminar_Processo();
}

void far Tac()
{
    int i;
    i = 0;
    while(i++ < 5600)
    {
        fprintf(ARQ, "TAC ");
    }
    Terminar_Processo();
}

void far Toc()
{
    int i;
    i = 0;
    while(i++ < 5000)
    {
        fprintf(ARQ, "TOC ");
    }
    Terminar_Processo();
}

int main ()
{
    if ((ARQ = fopen("CODE\\SAIDA.TXT","r+t")) == NULL)
        printf("\nErro na abertura do arquivo.\n\n");
    else
        printf("\nArquivo aberto com sucesso. A saida estara em SAIDA.TXT.\n\n");
    Criar_Processo(0,Tic, 2);
    Criar_Processo(1,Toc, 0);
    Criar_Processo(2,Tac, 1);
    Dispara_Sistema();
    return 0;
}
