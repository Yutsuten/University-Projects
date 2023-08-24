#include <system.h>
#include <stdio.h>

extern void far Criar_Processo(int numero, void far (*end_processo)(),int max_fila);
extern void far Terminar_Processo();
extern void far Dispara_Sistema();
extern void far Recebe(int *nome_emissor, char *msg);
extern int far Envia(int nome_destino, char *p_info);
