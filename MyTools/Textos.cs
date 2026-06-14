using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTools
{
    public static class Textos
    {
        static void Imprimir(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        static void ImprimirAnimado(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            mensaje += "\n";
            for (int i = 0; i < mensaje.Length; i++)
            {
                Console.Write(mensaje[i]);
                Thread.Sleep(30);
            }
            Console.ResetColor();
        }

        public static void ImprimirRojo(string text)
        {
            Imprimir(text, ConsoleColor.Red);
        }

        public static void ImprimirRojoAnimado(string text)
        {
            ImprimirAnimado(text, ConsoleColor.Red);
        }

        public static void ImprimirVerde(string text)
        {
            Imprimir(text, ConsoleColor.Green);
        }

        public static void ImprimirVerdeAnimado(string text)
        {
            ImprimirAnimado(text, ConsoleColor.Green);
        }

        public static void ImprimirAzul(string text)
        {
            Imprimir(text, ConsoleColor.Blue);
        }

        public static void ImprimirAzulAnimado(string text)
        {
            ImprimirAnimado(text, ConsoleColor.Blue);
        }

        public static void ImprimirAmarillo(string text)
        {
            Imprimir(text, ConsoleColor.Yellow);
        }

        public static void ImprimirAmarilloAnimado(string text)
        {
            ImprimirAnimado(text, ConsoleColor.Yellow);
        }

        public static void ImprimirCyan(string text)
        {
            Imprimir(text, ConsoleColor.Cyan);
        }

        public static void ImprimirCyanAnimado(string text)
        {
            ImprimirAnimado(text, ConsoleColor.Cyan);
        }

        public static void ImprimirMagenta(string text)
        {
            Imprimir(text, ConsoleColor.Magenta);
        }

        public static void ImprimirMagentaAnimado(string text)
        {
            ImprimirAnimado(text, ConsoleColor.Magenta);
        }

        public static void SelectTextAmarillo(string text)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            text = $"\t>>{text} <<";
            Imprimir(text, ConsoleColor.Black);
            Console.ResetColor();
        }

        public static void SelectTextCyan(string text)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            text = $"\t>>{text} <<";
            Imprimir(text, ConsoleColor.Black);
            Console.ResetColor();
        }

        public static void Warning(string text)
        {
            ImprimirAmarillo($"ADVERTENCIA: {text}");
        }

        public static void Error(string text)
        {
            ImprimirRojo($"ERROR: {text}");
        }

        public static void Success(string text)
        {
            ImprimirVerde($"{text}");
        }

        public static void SelectTextMagenta(string text)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            text = $"\t>>{text} <<";
            Imprimir(text, ConsoleColor.Black);
            Console.ResetColor();
        }

        public static void SelectTextVerde(string text)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            text = $"\t>>{text} <<";
            Imprimir(text, ConsoleColor.Black);
            Console.ResetColor();
        }
    }
}
