using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTools
{
    public static class PanelCentral
    {
        static string[] sensores = { "Sensor Humo - Zona 1", "Sensor Humo - Zona 2", "Sensor Humo - Zona 3" };

        static Random simulador = new Random();

        public static void MostrarSensores()
        {
            Console.Clear();
            Textos.ImprimirCyanAnimado("---SENSORES DIRECCIONALES CONECTADOS---");

            for (int i = 0; i < sensores.Length; i++)
            {
                Textos.ImprimirVerde($"Dirección: {i + 1} - {sensores[i]} [OK]");
            }
            Textos.ImprimirMagenta("\nPresiona cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        public static void PruebaOperatividad()
        {
            Console.Clear();
            Textos.ImprimirCyanAnimado("---INICIANDO COMISIONADO Y PRUEBAS---");

            for (int i = 0; i < sensores.Length; i++)
            {
                Textos.ImprimirMagenta($"Probando {sensores[i]}...");
                Thread.Sleep(500);

                int probabilidadFallo = simulador.Next(0, 100);

                if (probabilidadFallo > 85)
                {
                    Textos.ImprimirRojo("ERROR DE LECTURA!");

                }
                else
                {
                    Textos.ImprimirVerde("respuesta correcta...");
                }
            }
            Textos.ImprimirAmarillo("Pruebas finalizadas. Presiona cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        public static void IniciarMonitoreo()
        {
            Console.Clear();
            DateTime ahora = DateTime.Now;
            Textos.ImprimirAmarillo($"Fecha y hora: {ahora:dd/MM/yyyy HH:mm:ss}");
            Textos.ImprimirCyanAnimado("---MONITOREO DEL SISTEMA CONTRA INCENDIOS---");
            Textos.ImprimirAmarillo("presione la tecla ESC para detener el monitoreo...");

            bool alarmaActivada = false;

            while (!Console.KeyAvailable && !alarmaActivada)
            {
                int indiceSensor = simulador.Next(0, sensores.Length);
                int temperaturaSimulada = simulador.Next(20, 80);

                if (temperaturaSimulada >= 70)
                {
                    alarmaActivada = true;
                    Textos.ImprimirRojo($"¡ALERTA! {sensores[indiceSensor]} ha detectado una temperatura crítica: {temperaturaSimulada}°C");
                    Console.Beep(1000, 2000);
                }
                else
                {
                    Textos.ImprimirVerde($"Monitoreando {sensores[indiceSensor]} - Temperatura: {temperaturaSimulada}°C");
                }
                Thread.Sleep(800);
            }

            if (Console.KeyAvailable) Console.ReadKey(true);

            Textos.ImprimirCyan("Monitoreo detenido. Presiona cualquier tecla para volver al menú...");
            Console.ReadKey();
        }
        private static void ActivarLucesEstroboscopicas() //
        {
            for (int i = 0; i < 10; i++)
            {
                // Encendido de la luz
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                Console.WriteLine("\n\n");
                Console.WriteLine("⚠ ALARMA DE INCENDIO ⚠");
                Console.WriteLine("LUCES ESTROBOSCÓPICAS ACTIVADAS");
                Console.WriteLine("INICIE LA EVACUACIÓN");

                Console.Beep(1200, 150);
                Console.Beep(4000, 150);
                Thread.Sleep(300);

                // Apagado de la luz
                Console.ResetColor();
                Console.Clear();
                Thread.Sleep(300);
            }

            Console.ResetColor();
            Console.Clear();

            Textos.ImprimirRojo("Siga las instrucciones de evacuación.");
        }

    }
}
