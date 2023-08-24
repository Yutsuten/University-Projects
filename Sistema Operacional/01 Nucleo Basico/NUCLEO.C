#include <system.h>
#include <stdio.h>

typedef struct registros
{
    unsigned bx1,es1;
} regis;

typedef union k
{
    regis x;
    char far *y;
} APONTA_REG_CRIT;

APONTA_REG_CRIT a;

typedef struct Desc_Prog *DESCRITOR_PROC;
struct Desc_Prog /* DEFINICAO DO TIPO BCP */
{
    int nome, estado;
    PTR_DESC contexto;
    DESCRITOR_PROC prox_desc;
};

/* VARIAVEIS GLOBAIS */
enum estado {ATIVO, TERMINADO};
PTR_DESC d_esc;
DESCRITOR_PROC PRIM, fim_lista = 0;

/* FUNCOES DO NUCLEO */
void far Criar_Processo(int numero, void far (*end_processo)())
{
    DESCRITOR_PROC p_aux;
    p_aux = (DESCRITOR_PROC) malloc (sizeof (struct Desc_Prog));
    p_aux->nome = numero;
    p_aux->estado = ATIVO;
    p_aux->contexto = cria_desc();
    newprocess(end_processo, p_aux->contexto);
    if (PRIM != 0) /* SE NAO FOR O PRIMEIRO PROCESSO */
    {
        p_aux->prox_desc = fim_lista->prox_desc;
        fim_lista->prox_desc = p_aux;
        fim_lista = p_aux;
    }
    else /* SE FOR O PRIMEIRO PROCESSO */
    {
        PRIM = p_aux;
        p_aux->prox_desc = p_aux;
        fim_lista = p_aux;
    }
}

void far Volta_Dos()
{
    disable();
    setvect(8, p_est->int_anterior);
    enable();
    exit(0);
}

DESCRITOR_PROC far Procura_Prox_Ativo()
{
    DESCRITOR_PROC aux;
    aux = PRIM->prox_desc;
    do
    {
        if (aux->estado == ATIVO)
            return aux;
        aux = aux->prox_desc;
    } while (aux != PRIM->prox_desc);
    return 0;
}

void far Escalador()
{
    p_est->p_origem = d_esc;
    p_est->p_destino = PRIM->contexto;
    p_est->num_vetor = 8;
    /* Iniciando ponteiro para a Pilha do DOS */
    _AH=0x34;
    _AL=0x00;
    geninterrupt(0x21);
    a.x.bx1=_BX;
    a.x.es1=_ES;
    while(1)
    {
        iotransfer();
        disable();
        /* Houve Interrupcao durante um trecho do DOS? */
        if (!*a.y)
        {
            /* Se entrou aqui, nao estava em um trecho do DOS! */
            if ((PRIM = Procura_Prox_Ativo()) == NULL)
                Volta_Dos();
            p_est->p_destino = PRIM->contexto;
        }
        enable();
    }
}

void far Dispara_Sistema()
{
    PTR_DESC d_inicio;
    d_esc = cria_desc();
    newprocess(Escalador, d_esc);
    d_inicio = cria_desc();
    transfer(d_inicio, d_esc);
}

void far Terminar_Processo()
{
    disable();
    PRIM->estado = TERMINADO;
    enable();
    while(1);
}
