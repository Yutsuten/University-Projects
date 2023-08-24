#include <stdio.h>
#include <conio.h>
#include <windows.h>
int main ()
{
    char ch;
    while (!kbhit())
    {
        ch = kbhit();
        printf ("Teste %d\n",ch);
        Sleep (500);
    }
    ch = kbhit();
    printf ("Teste %d\n",ch);
    getch ();
    return 0;
}
