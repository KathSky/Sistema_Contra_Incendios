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
                    ActivarLucesEstroboscopicas();
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
        private static void ActivarLucesEstroboscopicas()
        {
            Textos.ImprimirRojoAnimado("===== 🔥 ALARMA DE INCENDIO 🔥 =====");

            for (int ciclo = 0; ciclo < 5; ciclo++)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.Beep(900, 180);

                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                Console.Beep(1200, 180);

                Console.ResetColor();
                Console.Clear();
                Thread.Sleep(100);
            }
            Console.ResetColor();
            Console.Clear();

            Textos.ImprimirMagentaAnimado("LUCES ESTROBOSCÓPICAS ACTIVADAS");
            Textos.ImprimirAmarilloAnimado("INICIE LA EVACUACIÓN");
        }
    }
}
