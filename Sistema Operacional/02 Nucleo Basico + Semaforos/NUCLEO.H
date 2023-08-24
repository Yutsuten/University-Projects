#include <system.h>
#include <stdio.h>

typedef struct Desc_Prog *DESCRITOR_PROC;
struct Desc_Prog /* DEFINICAO DO TIPO BCP */
{
    int nome, estado;
    PTR_DESC contexto;
    DESCRITOR_PROC prox_desc;

    struct Desc_Prog *fila_semaforo; /*fila de processos bloqueados*/

    int tam_fila;
    int qtde_msg_fila;
};

typedef struct sinaleira semaforo;
struct sinaleira
{
	int s;
	DESCRITOR_PROC Q;
};

extern void far Criar_Processo(int numero, void far (*end_processo)());
extern void far Terminar_Processo();
extern void far Dispara_Sistema();
extern void far P(semaforo (*sem));
extern void far V(semaforo (*sem));
extern void far inicia_semaforo(semaforo (*sem), int n);