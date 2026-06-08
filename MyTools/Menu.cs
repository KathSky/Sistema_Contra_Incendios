using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTools
{
    public static class Menu
    {
        public static int MostrarMenu(string titulo, string[] opciones)
        {
            Console.CursorVisible = false;
            int seleccionActual = 0;

            while (true)
            {
                Console.Clear();
                string lineaHorizontal = new string('=', titulo.Length + 2);
                Textos.ImprimirMagenta($"\t╔{lineaHorizontal}╗");
                Textos.ImprimirMagenta($"\t║ {titulo} ║");
                Textos.ImprimirMagenta($"\t╚{lineaHorizontal}╝");
                Textos.ImprimirCyan($"\tFecha y hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");

                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == seleccionActual)
                    {
                        Textos.SelectTextVerde(opciones[i]);
                    }
                    else
                    {
                        Textos.ImprimirAmarillo($"\t  {opciones[i]}   ");
                    }
                }
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.UpArrow)
                {
                    seleccionActual--;
                    if (seleccionActual < 0)
                        seleccionActual = opciones.Length - 1;
                }
                else if (tecla.Key == ConsoleKey.DownArrow)
                {
                    seleccionActual++;
                    if (seleccionActual >= opciones.Length)
                        seleccionActual = 0;
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = true;
                    return seleccionActual;
                }
            }
        }
    }
}
