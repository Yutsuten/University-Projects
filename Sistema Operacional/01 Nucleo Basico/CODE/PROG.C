#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <nucleo.h>

FILE *ARQ;

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
