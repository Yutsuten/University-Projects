/******************************************************************************
 * elevador.cpp                                                               *
 *                                                                            *
 * Este arquivo contém o código fonte das funções utilizadas para             *
 * controlar o Elevador.                                                      *
 *                                                                            *
 *                                   Silas Franco dos Reis Alves - Junho 2007 *
 *                                                      silas.alves@gmail.com *
 ******************************************************************************/

//==============================================================================
// INCLUDES
//==============================================================================

// Inclui o cabeçalho com a definição dos Tipos e Funções.
#include "elevador.h"


//==============================================================================
// FUNÇÕES DE CONTROLE DO GERENCIADOR
//==============================================================================

// Carrega a Biblioteca Dinâmica (DLL) para o acesso ao Porto Paralelo, reseta
// o Elevador e atualiza o GerenciadorElevador.
// Caso tenha sucesso, retorna 0.
// Caso ocorra um erro, retorna o valor referente ao erro.
int iniciaElevador (GerenciadorElevador *gerenciador)
{
    // Carrega a DLL.
    gerenciador->hLib = LoadLibrary("inpout32.dll");
    if (gerenciador->hLib == NULL) return ERRO_CARREGAR_DLL;
    
    // Extrai as funções da DLL.
    gerenciador->inp32 = (inpfuncPtr) GetProcAddress(gerenciador->hLib, "Inp32");
    if (gerenciador->inp32 == NULL)
    {
        gerenciador->status = ERRO_LER_FUNCOES_DLL;
        return ERRO_LER_FUNCOES_DLL;
    }   
    gerenciador->oup32 = (oupfuncPtr) GetProcAddress(gerenciador->hLib, "Out32");
    if (gerenciador->oup32 == NULL)
    {
        gerenciador->status = ERRO_LER_FUNCOES_DLL;
        return ERRO_LER_FUNCOES_DLL;
    }
    
    // Inicia o Porto Paralelo.
    outportb(gerenciador, CONTROL, 0x04);
    
    // Inicia o Elevador atribuindo 0 a todos as saídas.
    outportb(gerenciador, ADDR, 2);
    outportb(gerenciador, DATA, 0);
    outportb(gerenciador, ADDR, 1);
    outportb(gerenciador, DATA, 0);
    outportb(gerenciador, ADDR, 0);
    outportb(gerenciador, DATA, 0);
    
    // Reseta as variáveis do gerenciador;
    gerenciador->dispositivoSelecionado = 0;
    gerenciador->painelCabine = 0;
    gerenciador->painelChamada = 0;
    gerenciador->display = 0;
    gerenciador->motor = 0;
    gerenciador->ultimoPassoMotor = 0;

    // Retorna o estado da função
    gerenciador->status = ELEVADOR_INICIADO;
    return ELEVADOR_INICIADO;
}

//------------------------------------------------------------------------------

// Libera a Biblioteca Dinâmica e o acesso ao Porto Paralelo.
// Caso ocorra um erro, retorna o valor referente ao erro.
int encerraElevador (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Libera a DLL.
    FreeLibrary(gerenciador->hLib);
    
    // Atualiza o gerenciador.
    gerenciador->oup32 = NULL;
    gerenciador->inp32 = NULL;
    gerenciador->status = ELEVADOR_ENCERRADO;
}

//------------------------------------------------------------------------------

// Verifica se o GerenciadorElevador foi iniciado corretamente.
int validaGerenciador (GerenciadorElevador *gerenciador)
{
    // Verifica se existe algum erro no gerenciador.
    if (gerenciador->status!=ELEVADOR_INICIADO)
        return gerenciador->status;
    
    // Verifica se a DLL foi carregada corretamente.
    else if (gerenciador->hLib==NULL)
    {
        gerenciador->status = ERRO_DLL_NAO_CARREGADA;
        return ERRO_DLL_NAO_CARREGADA;
    }
    
    // Verifica se foi possível carregar as funções da DLL.
    else if (gerenciador->inp32==NULL || gerenciador->oup32==NULL)
    {
        gerenciador->status = ERRO_FUNCOES_NAO_CARREGADAS;
        return ERRO_FUNCOES_NAO_CARREGADAS;
    }
    
    // Caso não haja erro, retorna que o gerenciador esta iniciado.
    else
        return ELEVADOR_INICIADO;
}


//==============================================================================
// FUNÇÕES DE CONTROLE DO PAINEL DA CABINE
//==============================================================================

// Acende um LED do painel da cabine do elevador.
// Caso ocorra um erro, retorna o valor referente ao erro.
int acendeLEDCabine (GerenciadorElevador *gerenciador, int numeroLED)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 0 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 0)
    {
        outportb(gerenciador, ADDR, 0);
        gerenciador->dispositivoSelecionado = 0;
    }
    
    // Cria a máscara para o bit a ser alterado.
    char mascara;
    if (numeroLED == 0) mascara = 0x01;
    else if (numeroLED == 1) mascara = 0x02;
    else if (numeroLED == 2) mascara = 0x04;
    else if (numeroLED == 3) mascara = 0x08;
    else if (numeroLED == 4) mascara = 0x10;
    else if (numeroLED == 5) mascara = 0x20;
    else if (numeroLED == 6) mascara = 0x40;
    else return FALSE;
    
    // Aplica a máscara e atualiza o dispositivo.
    gerenciador->painelCabine |= mascara;
    outportb(gerenciador, DATA, gerenciador->painelCabine);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Apaga um LED do painel da cabine do elevador.
// Caso ocorra um erro, retorna o valor referente ao erro.
int apagaLEDCabine (GerenciadorElevador *gerenciador, int numeroLED)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 0 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 0)
    {
        outportb(gerenciador, ADDR, 0);
        gerenciador->dispositivoSelecionado = 0;
    }
        
    // Cria a máscara para o bit a ser alterado.
    char mascara;
    if (numeroLED == 0) mascara = 0xfe;
    else if (numeroLED == 1) mascara = 0xfd;
    else if (numeroLED == 2) mascara = 0xfb;
    else if (numeroLED == 3) mascara = 0xf7;
    else if (numeroLED == 4) mascara = 0xef;
    else if (numeroLED == 5) mascara = 0xdf;
    else if (numeroLED == 6) mascara = 0xbf;
    else return ERRO_VALOR_EXCEDE_LIMITE;
    
    // Aplica a máscara e atualiza o dispositivo.
    gerenciador->painelCabine &= mascara;
    outportb(gerenciador, DATA, gerenciador->painelCabine);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Envia uma mascara ao painel da cabine, onde cada bit corresponde a um LED.
// Caso ocorra um erro, retorna o valor referente ao erro.
int atualizaPainelCabine (GerenciadorElevador *gerenciador, int mascara)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 0 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 0)
    {
        outportb(gerenciador, ADDR, 0);
        gerenciador->dispositivoSelecionado = 0;
    }

    // Atualiza o dispositivo.
    gerenciador->painelChamada = mascara;
    outportb(gerenciador, DATA, gerenciador->painelChamada);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Verifica se determinado LED do painel da cabine está aceso ou não.
int leLEDPainelCabine (GerenciadorElevador *gerenciador, int numeroLED)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return ERRO_GERAL;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 0)
    {
        outportb(gerenciador, ADDR, 0);
        gerenciador->dispositivoSelecionado = 0;
    }
    
    // Cria a máscara para o bit a ser alterado.
    char mascara;
    if (numeroLED == 0) mascara = 0x01;
    else if (numeroLED == 1) mascara = 0x02;
    else if (numeroLED == 2) mascara = 0x04;
    else if (numeroLED == 3) mascara = 0x08;
    else if (numeroLED == 4) mascara = 0x10;
    else if (numeroLED == 5) mascara = 0x20;
    else if (numeroLED == 6) mascara = 0x40;
    else return ERRO_VALOR_EXCEDE_LIMITE;
    
    return (gerenciador->painelCabine&mascara);
}

//------------------------------------------------------------------------------

// Retorna o estado de todos os LEDs do painel da cabine.
int leTodosLEDPainelCabine (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return ERRO_GERAL;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 0)
    {
        outportb(gerenciador, ADDR, 0);
        gerenciador->dispositivoSelecionado = 0;
    }
    
    return (gerenciador->painelCabine);
}

//------------------------------------------------------------------------------

// Lê o estado dos botões do painel da cabine.
// Caso ocorra um erro, retorna o valor referente ao erro.
int lePainelCabine (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return ERRO_GERAL;
    
    // Verifica se o dispositivo 0 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 0)
    {
        outportb(gerenciador, ADDR, 0);
        gerenciador->dispositivoSelecionado = 0;
    }
    
    // Lê o painel, inverte e retorna a leitura.
    char painel;
    painel = inportb(gerenciador, DATA);
    painel ^= 0xff;
    return painel;
}


//==============================================================================
// FUNÇÕES DE CONTROLE DO PAINEL DE CHAMADA
//==============================================================================

// Acende um LED do painel de chamada.
// Caso ocorra um erro, retorna o valor referente ao erro.
int acendeLEDChamada (GerenciadorElevador *gerenciador, int numeroLED)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 1)
    {
        outportb(gerenciador, ADDR, 1);
        gerenciador->dispositivoSelecionado = 1;
    }
        
    // Cria a máscara para o bit a ser alterado.
    char mascara;
    if (numeroLED == 0) mascara = 0x01;
    else if (numeroLED == 1) mascara = 0x02;
    else if (numeroLED == 2) mascara = 0x04;
    else if (numeroLED == 3) mascara = 0x08;
    else if (numeroLED == 4) mascara = 0x10;
    else if (numeroLED == 5) mascara = 0x20;
    else if (numeroLED == 6) mascara = 0x40;
    else return ERRO_VALOR_EXCEDE_LIMITE;
    
    // Aplica a máscara e atualiza o dispositivo.
    gerenciador->painelChamada |= mascara;
    outportb(gerenciador, DATA, gerenciador->painelChamada);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Apaga um LED do painel da chamada.
// Caso ocorra um erro, retorna o valor referente ao erro.
int apagaLEDChamada (GerenciadorElevador *gerenciador, int numeroLED)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 1)
    {
        outportb(gerenciador, ADDR, 1);
        gerenciador->dispositivoSelecionado = 1;
    }
        
    // Cria a máscara para o bit a ser alterado.
    char mascara;
    if (numeroLED == 0) mascara = 0xfe;
    else if (numeroLED == 1) mascara = 0xfd;
    else if (numeroLED == 2) mascara = 0xfb;
    else if (numeroLED == 3) mascara = 0xf7;
    else if (numeroLED == 4) mascara = 0xef;
    else if (numeroLED == 5) mascara = 0xdf;
    else if (numeroLED == 6) mascara = 0xbf;
    else return ERRO_VALOR_EXCEDE_LIMITE;
    
    // Aplica a máscara e atualiza o dispositivo.
    gerenciador->painelChamada &= mascara;
    outportb(gerenciador, DATA, gerenciador->painelChamada);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Envia uma mascara ao painel de chamada, onde cada bit corresponde a um LED.
// Caso ocorra um erro, retorna o valor referente ao erro.
int atualizaPainelChamada (GerenciadorElevador *gerenciador, int mascara)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 1)
    {
        outportb(gerenciador, ADDR, 1);
        gerenciador->dispositivoSelecionado = 1;
    }

    // Atualiza o dispositivo.
    gerenciador->painelChamada = mascara;
    outportb(gerenciador, DATA, gerenciador->painelChamada);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Verifica se determinado LED do painel de chamada está aceso ou não.
int leLEDPainelChamada (GerenciadorElevador *gerenciador, int numeroLED)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return ERRO_GERAL;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 1)
    {
        outportb(gerenciador, ADDR, 1);
        gerenciador->dispositivoSelecionado = 1;
    }
    
    // Cria a máscara para o bit a ser alterado.
    char mascara;
    if (numeroLED == 0) mascara = 0x01;
    else if (numeroLED == 1) mascara = 0x02;
    else if (numeroLED == 2) mascara = 0x04;
    else if (numeroLED == 3) mascara = 0x08;
    else if (numeroLED == 4) mascara = 0x10;
    else if (numeroLED == 5) mascara = 0x20;
    else if (numeroLED == 6) mascara = 0x40;
    else return ERRO_VALOR_EXCEDE_LIMITE;
    
    return (gerenciador->painelChamada&mascara);
}

//------------------------------------------------------------------------------

// Retorna o estado de todos os LEDs do painel da cabine.
int leTodosLEDPainelChamada (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return ERRO_GERAL;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 1)
    {
        outportb(gerenciador, ADDR, 1);
        gerenciador->dispositivoSelecionado = 1;
    }
    
    return (gerenciador->painelChamada);
}

//------------------------------------------------------------------------------

// Lê o estado dos botões do painel de chamada.
// Caso ocorra um erro, retorna o valor referente ao erro.
int lePainelChamada (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return ERRO_GERAL;
    
    // Verifica se o dispositivo 1 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 1)
    {
        outportb(gerenciador, ADDR, 1);
        gerenciador->dispositivoSelecionado = 1;
    }
    
    // Lê o painel, inverte e retorna a leitura.
    char painel;
    painel = inportb(gerenciador, DATA);
    painel ^= 0xff;
    return painel;
}


//==============================================================================
// FUNÇÕES DE CONTROLE DO DISPLAY, MOTOR E SENSORES DE ANDAR
//==============================================================================

// Altera o número mostrado pelo display.
// Caso ocorra um erro, retorna o valor referente ao erro.
int alteraDisplay (GerenciadorElevador *gerenciador, int numero)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 2 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 2)
    {
        outportb(gerenciador, ADDR, 2);
        gerenciador->dispositivoSelecionado = 2;
    }
    
    // Verifica se o número excede o valor que o display pode mostrar (0-15)    
    if (numero<0x0 || numero>0xf) return ERRO_VALOR_EXCEDE_LIMITE;
    
    gerenciador->display = numero;
    outportb(gerenciador, DATA, gerenciador->display);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Altera o acionamento das bobinas do motor.
// Caso ocorra um erro, retorna o valor referente ao erro.
int alteraBobinaMotor (GerenciadorElevador *gerenciador, int codigo)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 2 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 2)
    {
        outportb(gerenciador, ADDR, 2);
        gerenciador->dispositivoSelecionado = 2;
    }
    
    // Verifica se o comando causa curto-circuito na Ponte H que ativa o motor.
    if (codigo&0x3 || codigo&0xc) return ERRO_ACIONAMENTO_INVALIDO;
    
    // Aqui é elaborado o byte a ser enviado ao dispositivo. Isto é necessário
    // pois as estruturas motor e display fazem uso do mesmo dispositivo. Desta
    // forma, o dado do display não é perdido.
    // OBS: o campo motor é o mais significativo.
    gerenciador->motor = codigo;
    codigo <<=4;
    codigo |= gerenciador->display;
    
    // Atualiza o dispositivo.
    outportb(gerenciador, DATA, codigo);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Lê o estado dos sensores de presença do elevador nos andares.
// Caso ocorra um erro, retorna o valor referente ao erro.
int leSensorAndar (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return ERRO_GERAL;
    
    // Verifica se o dispositivo 2 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 2)
    {
        outportb(gerenciador, ADDR, 2);
        gerenciador->dispositivoSelecionado = 2;
    }
    
    // Lê os sensores, inverte e retorna a leitura.
    char sensores;
    sensores = inportb(gerenciador, DATA);
    sensores ^= 0xff;
    return sensores;
}

//------------------------------------------------------------------------------

// Faz com que o motor ande para cima.
// Caso ocorra um erro, retorna o valor referente ao erro.
int PassoMotorCima (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 2 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 2)
    {
        outportb(gerenciador, ADDR, 2);
        gerenciador->dispositivoSelecionado = 2;
    }
    
    // Escolhe qual o código para ativar as bobinas.
    char passo;
    if (gerenciador->ultimoPassoMotor==0)       passo = 0x10;
    else if (gerenciador->ultimoPassoMotor==1)  passo = 0x40;
    else if (gerenciador->ultimoPassoMotor==2)  passo = 0x20;
    else if (gerenciador->ultimoPassoMotor==3)  passo = 0x80;
    
    // Avança o passo.
    gerenciador->ultimoPassoMotor++;
    
    // Aqui é elaborado o byte a ser enviado ao dispositivo. Isto é necessário
    // pois as estruturas motor e display fazem uso do mesmo dispositivo. Desta
    // forma, o dado do display não é perdido.
    // OBS: o campo motor é o mais significativo.
    passo |= gerenciador->display;
    
    // Atualiza o dispositivo.
    outportb(gerenciador, DATA, passo);
    Sleep (TEMPO_MINIMO_PASSO);
    outportb(gerenciador, DATA, gerenciador->display);
    return SUCESSO;
}

//------------------------------------------------------------------------------

// Faz com que o motor ande para baixo.
// Caso ocorra um erro, retorna o valor referente ao erro.
int PassoMotorBaixo (GerenciadorElevador *gerenciador)
{
    // Verifica validade do gerenciador.
    int status = validaGerenciador (gerenciador);
    if (status!=ELEVADOR_INICIADO) return status;
    
    // Verifica se o dispositivo 2 já foi selecionado.
    if (gerenciador->dispositivoSelecionado != 2)
    {
        outportb(gerenciador, ADDR, 2);
        gerenciador->dispositivoSelecionado = 2;
    }
    
    // Escolhe qual o código para ativar as bobinas.
    char passo;
    if (gerenciador->ultimoPassoMotor==0)       passo = 0x10;
    else if (gerenciador->ultimoPassoMotor==1)  passo = 0x40;
    else if (gerenciador->ultimoPassoMotor==2)  passo = 0x20;
    else if (gerenciador->ultimoPassoMotor==3)  passo = 0x80;
    
    // Retrocede o passo.
    gerenciador->ultimoPassoMotor--;
    
    // Aqui é elaborado o byte a ser enviado ao dispositivo. Isto é necessário
    // pois as estruturas motor e display fazem uso do mesmo dispositivo. Desta
    // forma, o dado do display não é perdido.
    // OBS: o campo motor é o mais significativo.
    passo |= gerenciador->display;
    
    // Atualiza o dispositivo.
    outportb(gerenciador, DATA, passo);
    Sleep (TEMPO_MINIMO_PASSO);
    outportb(gerenciador, DATA, gerenciador->display);
    return SUCESSO;
}


//==============================================================================
// FUNÇÕES PARA ACESSO AO PORTO PARALELO SPP BIDIRECIONAL
//==============================================================================

// Estas funções são utilizadas quando o Porto Paralelo do computador não
// suportar o modo EPP. Elas utilizam o Porto Paralelo no modo SPP Bidirecional
// para emular o protocolo utilizado pelo modo EPP.
// OBS.: Por ser emulado por software, os acessos ao Porto Paralelo tornam-se
// mais lentos, reduzindo a taxa de transferência.

#ifdef SPP_BIDI

// Função que importa os dados do Porto Paralelo modificada para emular o
// protocolo EPP.
short inportb (GerenciadorElevador *gerenciador, short endPorto)
{
    // Variável que conterá o dado do porto.
    short dado;
    
    // Caso seja uma leitura no Registrador de Endereço, emula o modo EPP.
    // OBS.: Em nenhuma parte do programa será feita uma leitura do registrador
    //       de endereço, uma vez que é indisponível no hardware.
    if (endPorto==ADDR)
    {
        (gerenciador->oup32)(CONTROL, 0x2c);
        dado = (gerenciador->inp32)(BASE);
        (gerenciador->oup32)(CONTROL, 0x04);
    }
    
    // Caso seja uma leitura no Registrador de Dados, emula o modo EPP.
    else if (endPorto==DATA)
    {
        (gerenciador->oup32)(CONTROL, 0x26);
        dado = (gerenciador->inp32)(BASE);
        (gerenciador->oup32)(CONTROL, 0x04);
    }

    // Caso contrário, é um acesso aos registradores padrões SPP.
    else dado = (gerenciador->inp32)(endPorto);
    
    // Retorna o dado lido.
    return dado;
}

//------------------------------------------------------------------------------

// Função que exporta os dados ao Porto Paralelo modificada para emular o
// protocolo EPP.
void outportb (GerenciadorElevador *gerenciador, short endPorto, short dado)
{
    // Caso seja uma escrita no Registrador de Endereço, emula o modo EPP.
    if (endPorto==ADDR)
    {
        (gerenciador->oup32)(CONTROL, 0x0d);
        (gerenciador->oup32)(BASE, dado);
        (gerenciador->oup32)(CONTROL, 0x04);
    }
    
    // Caso seja uma escrita no Registrador de Dados, emula o modo EPP.
    else if (endPorto==DATA)
    {
        (gerenciador->oup32)(CONTROL, 0x07);
        (gerenciador->oup32)(BASE, dado);
        (gerenciador->oup32)(CONTROL, 0x04);
    }
    
    // Caso contrário, é um acesso aos registradores padrões SPP.
    else (gerenciador->oup32)(endPorto, dado);
}

#endif

//==============================================================================
// FUNÇÕES PARA ACESSO AO PORTO PARALELO EPP
//==============================================================================

// Estas funções são utilizadas quando o Porto Paralelo do computador quando
// o modo EPP esta disponível. Nesse caso, o protocolo é gerenciado pelo
// hardware, reduzindo a carga computacional sobre o mesmo, tornando-o mais
// veloz.

#ifdef EPP

// Função que importa os dados do Porto Paralelo no modo EPP.
short inportb (GerenciadorElevador *gerenciador, short endPorto)
{
    return (gerenciador->inp32)(endPorto);
}

//------------------------------------------------------------------------------

// Função que exporta os dados ao Porto Paralelo no modo EPP.
void outportb (GerenciadorElevador *gerenciador, short endPorto, short dado)
{
    (gerenciador->oup32)(endPorto, dado);
}

#endif
