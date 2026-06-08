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
        static string[] sensores =
        {
                "Sensor Humo - Zona 1", "Sensor Temperatura - Zona 1",

                "Sensor Humo - Zona 2", "Sensor Temperatura - Zona 2",

                "Sensor Humo - Zona 3", "Sensor Temperatura - Zona 3",
        };
        static bool[] estadoDispositivos =
        {
            true,true,true,
            true,true,true,
            true,true,true
        };

        static Random simulador = new Random();
        static string modoAlerta = "AUTOMÁTICO";
        static int umbralTemperatura = 70;
        static int umbralHumo = 60;
        static List<string> historial = new List<string>();

        public static void MostrarSensores()
        {
            Console.Clear();
            Textos.ImprimirAmarillo($"Fecha y hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");
            Textos.ImprimirCyanAnimado("---SENSORES DIRECCIONALES CONECTADOS---");

            for (int i = 0; i < sensores.Length; i++)
            {
                string estado;

                if (estadoDispositivos[i])
                    estado = "ACTIVO";
                else
                    estado = "INACTIVO";

                Textos.ImprimirVerde(
                    $"Dirección: {i + 1} - {sensores[i]} [{estado}]"
                );
            }
            Textos.ImprimirMagenta("\nPresiona cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        public static void PruebaOperatividad()
        {
            Console.Clear();
            historial.Add($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Comisionado completado. Todos los dispositivos operan correctamente."
);
            Textos.ImprimirAmarillo($"Fecha y hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");
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
                    historial.Add($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Prueba realizada a {sensores[i]}");              }
                }
            Textos.ImprimirAmarillo("Pruebas finalizadas. Presiona cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        public static void IniciarMonitoreo()
        {
            Console.Clear();

            DateTime ahora = DateTime.Now;
            DateTime inicioMonitoreo = DateTime.Now;

            Textos.ImprimirAmarillo($"Fecha y hora: {ahora:dd/MM/yyyy HH:mm:ss}");
            Textos.ImprimirCyanAnimado("---MONITOREO DEL SISTEMA CONTRA INCENDIOS---");
            Textos.ImprimirAmarillo("Presione cualquier tecla para detener el monitoreo...\n");

            while (!Console.KeyAvailable)
            {
                int indiceSensor = simulador.Next(0, sensores.Length);

                int temperaturaSimulada = simulador.Next(20, 80);

                int porcentajeHumo = simulador.Next(0, 100);

                TimeSpan tiempoTranscurrido =
                    DateTime.Now - inicioMonitoreo;

                if (temperaturaSimulada >= umbralTemperatura ||
                    porcentajeHumo >= umbralHumo)
                {
                    if (modoAlerta == "AUTOMÁTICO")
                    {
                        Textos.ImprimirRojo(
                            $"¡ALERTA! {sensores[indiceSensor]} | Temp: {temperaturaSimulada}°C | Humo: {porcentajeHumo}%"
                        );

                        historial.Add(
                            $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - ALARMA EN {sensores[indiceSensor]} | Temp: {temperaturaSimulada}°C | Humo: {porcentajeHumo}%"
                        );

                        Console.Beep(1000, 2000);

                        ActivarLucesEstroboscopicas();

                        Textos.ImprimirAmarillo(
                            "Monitoreo reanudado...\n"
                        );
                    }
                    else
                    {
                        Textos.ImprimirAmarillo(
                            $"ADVERTENCIA: Umbral superado en {sensores[indiceSensor]} | Temp: {temperaturaSimulada}°C | Humo: {porcentajeHumo}%"
                        );

                        Textos.ImprimirAmarillo(
                            "Se requiere activación manual de una estación."
                        );
                    }
                }
                else
                {
                    Textos.ImprimirVerde(
                        $"Tiempo: {(int)tiempoTranscurrido.TotalSeconds}s | " +
                        $"{sensores[indiceSensor]} | " +
                        $"Temperatura: {temperaturaSimulada}°C | " +
                        $"Humo: {porcentajeHumo}%"
                    );
                }

                Thread.Sleep(800);
            }

            if (Console.KeyAvailable)
                Console.ReadKey(true);

            Textos.ImprimirCyan(
                "\nMonitoreo detenido. Presiona cualquier tecla para volver al menú..."
            );

            Console.ReadKey();
        }

        public static void EstacionesManuales()
        {
            Console.Clear();

            Textos.ImprimirCyanAnimado("--- ESTACIONES MANUALES ---");

            Console.Write("1. Estación Manual - Zona 1 ");

            if (estadoDispositivos[2])
                Textos.ImprimirVerde("[ACTIVO]");
            else
                Textos.ImprimirRojo("[INACTIVO]");

            Console.Write("2. Estación Manual - Zona 2 ");

            if (estadoDispositivos[5])
                Textos.ImprimirVerde("[ACTIVO]");
            else
                Textos.ImprimirRojo("[INACTIVO]");
            Console.Write("3. Estación Manual - Zona 3 ");
            if (estadoDispositivos[8])
                Textos.ImprimirVerde("[ACTIVO]");
            else
                Textos.ImprimirRojo("[INACTIVO]");
            Console.Write("\nSeleccione una zona (1-3): ");
            int zona = int.Parse(Console.ReadLine());

            Console.WriteLine("\n1. Activar");
            Console.WriteLine("2. Desactivar");

            Console.Write("Seleccione una opción: ");
            int accion = int.Parse(Console.ReadLine());

            int indice = 0;

            if (zona == 1)
                indice = 2;
            else if (zona == 2)
                indice = 5;
            else if (zona == 3)
                indice = 8;

            if (accion == 1)
            {
                estadoDispositivos[indice] = true;

                Textos.ImprimirVerde("Estación manual activada.");
            }
            else if (accion == 2)
            {
                estadoDispositivos[indice] = false;

                Textos.ImprimirRojo("Estación manual desactivada.");
            }

            Textos.ImprimirAmarillo("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
            return;
        }
        public static void ConfiguracionSistema()
        {
            int opcion;
            do
            {
                Console.Clear();
                Textos.ImprimirCyanAnimado("--- CONFIGURACIÓN DEL SISTEMA ---");

                Console.WriteLine("\n[1] Ver modo de alertas");
                Console.WriteLine("[2] Cambiar modo de alertas");
                Console.WriteLine("[3] Cambiar umbral de temperatura °C");
                Console.WriteLine("[4] Cambiar umbral de humo %");
                Console.WriteLine("[0] Volver al menú principal");

                Console.Write("\nSeleccione una opción --> ");
                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:

                        Console.Clear();
                        Textos.ImprimirVerde($"Modo actual: {modoAlerta}");
                        Console.ReadKey();

                        break;

                    case 2:

                        Console.Clear();

                        Console.WriteLine("1. AUTOMÁTICO");
                        Console.WriteLine("2. MANUAL");

                        Console.Write("\nSeleccione una opción: ");

                        int modo = int.Parse(Console.ReadLine());

                        if (modo == 1)
                            modoAlerta = "AUTOMÁTICO";
                        else if (modo == 2)
                            modoAlerta = "MANUAL";

                        Textos.ImprimirVerde($"Modo cambiado a {modoAlerta}");
                        Console.ReadKey();

                        break;

                    case 3:

                        Console.Clear();

                        Console.WriteLine($"Umbral actual: {umbralTemperatura}°C");

                        Console.Write("\nNuevo umbral de temperatura: ");

                        umbralTemperatura = int.Parse(Console.ReadLine());

                        Textos.ImprimirVerde("Temperatura actualizada.");
                        Console.ReadKey();

                        break;

                    case 4:

                        Console.Clear();

                        Console.WriteLine($"Umbral actual: {umbralHumo}%");

                        Console.Write("\nNuevo umbral de humo: ");

                        umbralHumo = int.Parse(Console.ReadLine());

                        Textos.ImprimirVerde("Porcentaje de humo actualizado.");
                        Console.ReadKey();

                        break;
                }

            } while (opcion != 0);
        }
        public static void RestablecerSistema()
        {
            Console.Clear();

            Textos.ImprimirAmarillo($"Fecha y hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");

            Textos.ImprimirCyanAnimado("--- RESTABLECIENDO SISTEMA ---");

            Thread.Sleep(1000);

            Textos.ImprimirVerde("Alarmas reiniciadas.");
            Textos.ImprimirVerde("Sistema normalizado.");

            historial.Add($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - SISTEMA RESTABLECIDO");

            Console.ReadKey();
        }
        public static void MostrarHistorial()
        {
            Console.Clear();

            Textos.ImprimirAmarillo($"Fecha y hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");

            Textos.ImprimirCyanAnimado("--- HISTORIAL DEL SISTEMA ---");

            if (historial.Count == 0)
            {
                Textos.ImprimirRojo("No existen eventos registrados.");
            }
            else
            {
                for (int i = 0; i < historial.Count; i++)
                {
                    Textos.ImprimirVerde(historial[i]);
                }
            }

            Textos.ImprimirMagenta("\nPresiona cualquier tecla para volver...");
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
