#include <system.h>
#include <stdio.h>

#define tamPrior 10

typedef struct registros
{
    unsigned bx1,es1;
} regis;

typedef union k
{
    regis x;
    char far *y;
} APONTA_REG_CRIT;

typedef struct Desc_Prog *DESCRITOR_PROC;
struct Desc_Prog /* DEFINICAO DO TIPO BCP */
{
    int nome, estado, prioridade;
    PTR_DESC contexto;
    DESCRITOR_PROC prox_desc;
};

/* VARIAVEIS GLOBAIS */
enum estado {ATIVO, TERMINADO};
PTR_DESC d_esc;
DESCRITOR_PROC lista_prioridade[tamPrior] = {0,0,0,0,0,0,0,0,0,0}, fim_lista[tamPrior] = {0,0,0,0,0,0,0,0,0,0}, processo_atual = 0;
APONTA_REG_CRIT a;
int priorAtual = tamPrior - 1, prior_participantes = tamPrior - 1;

/* FUNCOES DO NUCLEO */
void far Criar_Processo(int numero, void far (*end_processo)(), int prior)
{
    DESCRITOR_PROC p_aux;
    p_aux = (DESCRITOR_PROC) malloc (sizeof (struct Desc_Prog));
    p_aux->nome = numero;
    p_aux->estado = ATIVO;
    /* Tratando a prioridade enviada pelo usuario */
    if (prior >= tamPrior)
        prior = tamPrior - 1;
    else if (prior < 0)
        prior = 0;
    /* Continuando as inicializacoes do BCP */
    p_aux->prioridade = prior;
    p_aux->contexto = cria_desc();
    newprocess(end_processo, p_aux->contexto);
    if (lista_prioridade[prior] != 0) /* SE NAO FOR O PRIMEIRO PROCESSO COM AQUELA PRIORIDADE */
    {
        fim_lista[prior]->prox_desc = p_aux;
        fim_lista[prior] = p_aux;
    }
    else /* SE FOR O PRIMEIRO PROCESSO COM AQUELA PRIORIDADE */
    {
        lista_prioridade[prior] = p_aux;
        fim_lista[prior] = p_aux;
    }
    p_aux->prox_desc = 0;
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
    DESCRITOR_PROC auxiliar = processo_atual;
    auxiliar = lista_prioridade[priorAtual]->prox_desc;
    /* Caso nao esteja ativo - procura na mesma fila de prioridade */
    while (auxiliar != 0)
    {
        if (auxiliar->estado == ATIVO)
            return auxiliar;
        auxiliar = auxiliar->prox_desc;
    }
    /* Caso tenha que mudar de fila de prioridade */
    while (1)
    {
        /* Passando para outra prioridade - Varios tratamentos devem ser feitos */
        priorAtual--; /* Percorre a lista de processos participantes */
        if (priorAtual < prior_participantes) /* Se vai aumentar o numero de participantes */
        {
            priorAtual = tamPrior - 1; /* Comeca a ver desde o que tem maior prioridade */
            prior_participantes--; /* Aumenta o numero de participantes a serem escalados */
            if (prior_participantes < 0) /* Se ja tinha todos como participantes */
                prior_participantes = tamPrior - 1; /* Reseta o numero de participantes para somente os que tem maior prioridade */
        }
        auxiliar = lista_prioridade[priorAtual]; /* Auxiliar recebe o primeiro da lista */
        /* Percorrendo a fila inteira da prioridade atual */
        while (auxiliar != 0)
        {
            if (auxiliar->estado == ATIVO)
                return auxiliar;
            if (auxiliar == processo_atual)
                return 0;
            auxiliar = auxiliar->prox_desc;
        }
    }
}

DESCRITOR_PROC Procura_Primeiro_Ativo()
{
    DESCRITOR_PROC auxiliar;
    int i;
    /* Se houver algum elemento com prioridade maxima, ele eh o primeiro */
    for (i = tamPrior - 1; i >= 0; i--)
    {
        if ((auxiliar = lista_prioridade[i]) != 0)
        {
            priorAtual = prior_participantes = i;
            return auxiliar;
        }
    }
    return 0;
}

void far Escalador()
{
    p_est->p_origem = d_esc;
    p_est->p_destino = processo_atual->contexto;
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
        /* Acabou a fatia de tempo do processo. */
        if (!*a.y) /* Houve Interrupcao durante um trecho do DOS? */
        {
            if ((processo_atual = Procura_Prox_Ativo()) == NULL)
                Volta_Dos();
            p_est->p_destino = processo_atual->contexto;
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
    processo_atual = Procura_Primeiro_Ativo();
    transfer(d_inicio, d_esc);
}

void far Terminar_Processo()
{
    disable();
    processo_atual->estado = TERMINADO;
    enable();
    while(1);
}
