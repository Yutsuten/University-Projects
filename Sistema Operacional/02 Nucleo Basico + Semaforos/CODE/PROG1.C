#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <time.h>
#include <nucleo.h>

#define MAX 10

semaforo vazio;
semaforo mutex;
semaforo cheio;

int informacao;
int p,c;

void Retirar(char[] *msg)
{
	printf("Retirando... ");
	strcpy((*msg),informacao);
	strcpy(informacao,"_____________");
	printf("Retirou! ");
}

void Depositar(char[] msg)
{
	printf("Depositando... ");
	strcpy(informacao, msg);
	printf("Depositou! ");
}

void far Produtor()
{
	int i = 0;
    int numero_mensagem;
    int producao = 1;
    char[] mensagem[15];
    
	while(i < 20)
	{
		printf("Produzindo (%i) ... ",producao);
		srand(time(NULL));
		numero_mensagem = rand()%10;
		
		if (numero_mensagem < 4)
			strcpy(mensagem,"numero baixo");
		if (numero_mensagem > 6)
			strcpy(mensagem,"numero alto");
		else
			strcpy(mensagem,"numero medio");
			
		P(&vazio);
		P(&mutex);
		Depositar(mensagem);
	    V(&mutex);
	    V(&cheio);
	    printf("Produziu! ");
	    producao = producao + 1;
	    i = i + 1;
	}
	/*int i;
    i = 0;
    while(i++ < 2400)
    {
        fprintf(ARQ, "TIC ");
    }
    Terminar_Processo();*/
}

void far Consumidor()
{
	int j = 0;
	char[] mensagem[15];
	int consumo = 1;
	while (j < 20)
	{
		printf("Consumindo (%i) ... ",consumo);
	    P(&cheio);
	    P(&mutex);
	    Retirar(&mensagem);
	    V(&mutex);
	    V(&vazio);
	    printf("Consumiu! ");
	    printf("\nMensagem retirada: %s",mensagem);
	    consumo = consumo + 1;
	    j = j + 1;
	}
	    /*int i;
    i = 0;
    while(i++ < 1600)
    {
        fprintf(ARQ, "TAC ");
    }
    Terminar_Processo();*/
}


int main ()
{

	inicia_semaforo(&vazio, MAX);
	inicia_semaforo(&cheio, 0);
	inicia_semaforo(&mutex, 1);

	p = 0;
	c = 0;


    /*if ((ARQ = fopen("CODE\\SAIDA.TXT","r+t")) == NULL)
        printf("\nErro na abertura do arquivo.\n\n");
    else
        printf("\nArquivo aberto com sucesso. A saida estara em SAIDA.TXT.\n\n");
    */
    printf("Criando processo consumidor... ");
	Criar_Processo(0,Consumidor);
	printf("Feito! \n");
    printf("Criando processo produtor... ");
    Criar_Processo(1,Produtor);
    printf("Feito! \n");
    printf("Disparando sistema... \n");
    Dispara_Sistema();
    return 0;
}
