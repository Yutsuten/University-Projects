#include <system.h>
#include <stdio.h>

extern void far Criar_Processo(int numero, void far (*end_processo)());
extern void far Terminar_Processo();
extern void far Dispara_Sistema();
