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

typedef struct address mensagem;
typedef mensagem *PTR_MENSAGEM;
struct address
{
    int flag;
    int nome_emissor;
    char mensa[25];
    struct address *prox;
};

typedef struct Desc_Prog *DESCRITOR_PROC;
struct Desc_Prog /* DEFINICAO DO TIPO BCP */
{
    int nome, estado;
    PTR_DESC contexto;

    /* ------------------------ INICIO ALTERAÇÕES ------------------------ */
    PTR_MENSAGEM ptr_msg;   /*Ponteiro para a lista encadeada de mensagens*/
    int tam_fila;           /*Tamanho da lista de mensagens*/
    int qtde_msg_fila;      /*Quantidade de mensagens atualmente na lisa (deve
                            ser inicializado com 0)*/
    /* ------------------------- FIM ALTERAÇÕES -------------------------- */


    DESCRITOR_PROC prox_desc;
};

/* VARIAVEIS GLOBAIS */
enum estado {ATIVO, BLOQREC, BLOQENV, TERMINADO};
PTR_DESC d_esc;
DESCRITOR_PROC PRIM, fim_lista = 0;



/* ------------------------ INICIO ALTERAÇÕES ------------------------ */
PTR_MENSAGEM far Cria_Fila_Mensagens(int max_fila)
/*  Função para retornar um ponteiro de uma lista simplesmente encadeada
    de "max_fila" elementos do tipo "mensagem", tendo estes o campo flag
    inicializado com o valor 0
*/
{
    PTR_MENSAGEM aux1,aux2,fila;
    int i;

    fila = aux1 = (PTR_MENSAGEM) malloc(sizeof(mensagem));
    aux1->flag = 0;
    aux1->prox = NULL;
    aux2 = aux1;
    for(i = 1; i < max_fila; i++)
    {
        aux1 = (PTR_MENSAGEM) malloc(sizeof(mensagem));
        aux2->prox = aux1;
        aux1->flag = 0;
        aux1->prox = NULL;
        aux2 = aux1;
    }

    return fila;
}
/* ------------------------- FIM ALTERAÇÕES -------------------------- */




/* FUNCOES DO NUCLEO */
void far Criar_Processo(int numero, void far (*end_processo)(), int max_fila)
{
    DESCRITOR_PROC p_aux;
    p_aux = (DESCRITOR_PROC) malloc (sizeof (struct Desc_Prog));
    p_aux->nome = numero;
    p_aux->estado = ATIVO;
    p_aux->contexto = cria_desc();


    /* ------------------------ INICIO ALTERAÇÕES ------------------------ */
    /*inicialização dos campos novos da struct Desc_Prog*/

    /*inicializando variáveis*/
    p_aux->tam_fila = max_fila;
    p_aux->qtde_msg_fila = 0;

    /*função para criar uma lista de mensagens e retornar um ponteiro para ela*/
    p_aux->ptr_msg = Cria_Fila_Mensagens(max_fila);
    /* ------------------------- FIM ALTERAÇÕES -------------------------- */


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
/* ------------------------ INICIO ALTERAÇÕES ------------------------ */
/* TROCA DE MENSAGEMS */
int far Envia(int nome_destino,char *p_info)
{
    DESCRITOR_PROC p_aux, p1;
    PTR_MENSAGEM msg;

    p_aux = PRIM;

    /*procura descritor do destino da mensagem na fila dos prontos; */
    while(p_aux->nome != nome_destino)
    {
        p_aux = p_aux->prox_desc;
        /* fracasso: não achou destino */
        if(p_aux == PRIM)
            return 0;
    }

    if(p_aux->tam_fila < p_aux->qtde_msg_fila)
        return 1;   /* fracasso: fila cheia*/

    disable();

    /*localiza uma mensagem vazia (flag==0);*/
    msg = p_aux->ptr_msg;
    while(msg->flag != 0)
        msg = msg->prox;

    /*completa a mensagem: */
    msg->flag = 1;
    msg->nome_emissor = PRIM->nome;
    strcpy(msg->mensa,p_info);
    (p_aux->qtde_msg_fila)++;

    /*Se o destino estiver como BLOQREC ele é alterado para ATIVO*/
    if(p_aux->estado == BLOQREC)
        p_aux->estado = ATIVO;

    /*Bloqueia o processo atual como BLOQENV*/
    PRIM->estado = BLOQENV;

    /*Passa o controle para o próximo processo ativo*/
    p_aux = Procura_Prox_Ativo();
    p1 = PRIM;
    PRIM = p_aux;
    transfer(p1->contexto,PRIM->contexto);

    return 2;   /*Suceesso no envio*/
}

void far Recebe(int *nome_emissor,char *msg){
    DESCRITOR_PROC p_aux,p1;
    PTR_MENSAGEM p_msg;
    disable();

    /*Se não houver mensagens na fila do processo, ele se
    autobloqueia com BLOQREC até receber uma mensagem*/
    if(PRIM->qtde_msg_fila == 0){
        PRIM->estado = BLOQREC;
        p_aux = Procura_Prox_Ativo();
        p1 = PRIM;
        PRIM = p_aux;
        transfer(p1->contexto,PRIM->contexto);
    }

    /*localiza a primeira mensagem cheia (flag==1); */
    p_msg = PRIM->ptr_msg;
    while(p_msg->flag != 1)
        p_msg = p_msg->prox;

    /*salva o nome do emissor*/
    *nome_emissor = p_msg->nome_emissor;

    /*salva a mensagem*/
    strcpy(msg,p_msg->mensa);

    /*decrementa o contador de mensagens*/
    (PRIM->qtde_msg_fila)--;

    /*atualiza a flag para zero, sinalizando um "slot"
    vazio de mensagens*/
    p_msg->flag = 0;

    /*localiza descritor do emissor;*/
    p_aux = PRIM;

    /*Encontra o Emissor e caso ele esteja como BLOQENV
    ele é setado como ATIVO*/
    while(p_aux->nome != *nome_emissor)
        p_aux = p_aux->prox_desc;
    if(p_aux->estado == BLOQENV)
        p_aux->estado = ATIVO;
    enable();
    return;
}

/* ------------------------ FIM ALTERAÇÕES ------------------------ */

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
