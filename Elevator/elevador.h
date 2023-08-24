/******************************************************************************
 * elevador.h                                                                 *
 *                                                                            *
 * Este arquivo contém a declaração das definições, funções e estruturas      *
 * utilizadas para controlar o Elevador.                                      *
 *                                                                            *
 *                                   Silas Franco dos Reis Alves - Junho 2007 *
 *                                                      silas.alves@gmail.com *
 ******************************************************************************/

//==============================================================================
// INCLUDES
//==============================================================================

// Utilizada para acessar a Biblioteca Dinâmica (DLL).
#include <windows.h> 

//==============================================================================
// DEFINIÇÕES
//==============================================================================

// Define qual o modo de operação o Porto Paralelo funcionará. Se definido EPP,
// ele funcionará com os endereços do Porto Paralelo no modo EPP 1.7. Caso seja
// definido SPP_BIDI, funcionará com os endereços do Porto Paralelo no modo
// SPP Bidirecional emulando o modo EPP.
// PADRÃO: SPP_BIDI
#define SPP_BIDI

// Tempo mínimo necessário para realizar-se um passo.
// PADRÃO: 8
#define TEMPO_MINIMO_PASSO 8

// Valores dos botões quando pressionados.
#define NADA_PRESSIONADO    0x00
#define B1_PRESSIONADO      0x01
#define B2_PRESSIONADO      0x02
#define B3_PRESSIONADO      0x04
#define B4_PRESSIONADO      0x08
#define B5_PRESSIONADO      0x10
#define B6_PRESSIONADO      0x20
#define B7_PRESSIONADO      0x40

// Valor dos andares.
#define INDETERMINADO   0x00
#define ANDAR1          0x01
#define ANDAR2          0x02
#define ANDAR3          0x04
#define ANDAR4          0x08
#define ANDAR5          0x10
#define ANDAR6          0x20
#define ANDAR7          0x40

// Utilizadas pela variável do tipo GerenciadorElevador.
#define ELEVADOR_INICIADO           0
#define SUCESSO                     0
#define ERRO_CARREGAR_DLL           2
#define ERRO_LER_FUNCOES_DLL        3
#define ELEVADOR_ENCERRADO          4
#define ERRO_VALOR_EXCEDE_LIMITE    5
#define ERRO_DLL_NAO_CARREGADA      6
#define ERRO_FUNCOES_NAO_CARREGADAS 7
#define ERRO_ACIONAMENTO_INVALIDO   8
#define ERRO_GERAL                  -1

// Utilizadas pelas funções que acessam o Porto Paralelo.
#define BASE    0x378
#define STATUS  0x379
#define CONTROL 0x37a
#define ADDR    0x37b
#define DATA    0x37c


//==============================================================================
// TIPOS E ESTRUTURAS
//==============================================================================

// Ponteiros para as funções importadas da DLL.
typedef short _stdcall (*inpfuncPtr)(short portaddr);
typedef void _stdcall (*oupfuncPtr)(short portaddr, short datum);

// Gerenciador do Elevador, utilizado para armazenar o último estado dos
// dispositivos de IO do elevador e tornar possível a criação de funções
// que operem bits de saída.
typedef struct
{
    unsigned status:4;
    unsigned dispositivoSelecionado:2;
    unsigned painelCabine:8;
    unsigned painelChamada:8;
    unsigned display:4;
    unsigned motor:4;
    unsigned ultimoPassoMotor:2;
    HINSTANCE hLib;
    inpfuncPtr inp32;
    oupfuncPtr oup32;
} GerenciadorElevador;


//==============================================================================
// FUNÇÕES DE CONTROLE DO GERENCIADOR
//==============================================================================

// Carrega a Biblioteca Dinâmica (DLL) para o acesso ao Porto Paralelo, reseta
// o Elevador e atualiza o GerenciadorElevador.
int iniciaElevador (GerenciadorElevador *gerenciador);

// Libera a Biblioteca Dinâmica e o acesso ao Porto Paralelo.
int encerraElevador (GerenciadorElevador *gerenciador);

// Verifica se o GerenciadorElevador é válido.
int validaGerenciador (GerenciadorElevador *gerenciador);

//==============================================================================
// FUNÇÕES DE CONTROLE DO PAINEL DA CABINE
//==============================================================================

// Acende um LED do painel da cabine do elevador.
int acendeLEDCabine (GerenciadorElevador *gerenciador, int numeroLED);

// Apaga um LED do painel da cabine do elevador.
int apagaLEDCabine (GerenciadorElevador *gerenciador, int numeroLED);

// Envia uma mascara ao painel da cabine, onde cada bit corresponde a um LED.
int atualizaPainelCabine (GerenciadorElevador *gerenciador, int mascara);

// Verifica se determinado LED do painel da cabine está aceso ou não.
int leLEDPainelCabine (GerenciadorElevador *gerenciador, int numeroLED);

// Retorna o estado de todos os LEDs do painel da cabine.
int leTodosLEDPainelCabine (GerenciadorElevador *gerenciador);

// Lê o estado dos botões do painel de cabine.
int lePainelCabine (GerenciadorElevador *gerenciador);


//==============================================================================
// FUNÇÕES DE CONTROLE DO PAINEL DE CHAMADA
//==============================================================================

// Acende um LED do painel de chamada.
int acendeLEDChamada (GerenciadorElevador *gerenciador, int numeroLED);

// Apaga um LED do painel da chamada.
int apagaLEDChamada (GerenciadorElevador *gerenciador, int numeroLED);

// Envia uma mascara ao painel de chamada, onde cada bit corresponde a um LED.
int atualizaPainelChamada (GerenciadorElevador *gerenciador, int mascara);

// Verifica se determinado LED do painel de chamada está aceso ou não.
int leLEDPainelChamada (GerenciadorElevador *gerenciador, int numeroLED);

// Retorna o estado de todos os LEDs do painel de chamada.
int leTodosLEDPainelChamada (GerenciadorElevador *gerenciador);

// Lê o estado dos botões do painel de chamada.
int lePainelChamada (GerenciadorElevador *gerenciador);


//==============================================================================
// FUNÇÕES DE CONTROLE DO DISPLAY, MOTOR E SENSORES DE ANDAR
//==============================================================================

// Altera o número mostrado pelo display.
int alteraDisplay (GerenciadorElevador *gerenciador, int numero);

// Altera o acionamento das bobinas do motor.
int alteraBobinaMotor (GerenciadorElevador *gerenciador, int codigo);

// Lê o estado dos sensores de presença do elevador nos andares.
int leSensorAndar (GerenciadorElevador *gerenciador);

// Faz com que o motor ande para cima.
int PassoMotorCima (GerenciadorElevador *gerenciador);

// Faz com que o motor ande para baixo.
int PassoMotorBaixo (GerenciadorElevador *gerenciador);


//==============================================================================
// FUNÇÕES DO SISTEMA
//==============================================================================

// Funções que acessam o Porto Paralelo.
short inportb (GerenciadorElevador *gerenciador, short endPorto);
void outportb (GerenciadorElevador *gerenciador, short endPorto, short dado);
