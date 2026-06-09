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
        static string[] pisos;
        static string[] sensores;
        static bool[] sensoresEstado;
        static string[] historial;
        static int historialContador;
        static Random simulador;

        static PanelCentral()
        {
            pisos = new string[] { "Piso 1", "Piso 2", "Piso 3" };
            sensores = InicializarSensores(pisos);

            sensoresEstado = new bool[sensores.Length];
            for (int i = 0; i < sensoresEstado.Length; i++)
            {
                sensoresEstado[i] = true;
            }

            historial = new string[100];
            historialContador = 0;
            simulador = new Random();

            RegistrarHistorial($"Sistema SCI Iniciado. Monitoreando {pisos.Length} pisos con {sensores.Length} dispositivos.");
        }

        private static string[] InicializarSensores(string[] listaPisos)
        {
            string[] tempSensores = new string[listaPisos.Length * 3];
            for (int i = 0; i < listaPisos.Length; i++)
            {
                tempSensores[i * 3] = $"Sensor Humo - {listaPisos[i]}";
                tempSensores[i * 3 + 1] = $"Sensor Temp - {listaPisos[i]}";
                tempSensores[i * 3 + 2] = $"Estación Manual - {listaPisos[i]}";
            }
            return tempSensores;
        }

        public static void RegistrarHistorial(string evento)
        {
            DateTime ahora = DateTime.Now;
            string registro = $"[{ahora:dd/MM/yyyy HH:mm:ss}] {evento}";

            if (historialContador < historial.Length)
            {
                historial[historialContador] = registro;
                historialContador++;
            }
            else
            {
                for (int i = 1; i < historial.Length; i++)
                {
                    historial[i - 1] = historial[i];
                }
                historial[historial.Length - 1] = registro;
            }
        }

        public static void MostrarSensores()
        {
            Console.Clear();
            Textos.ImprimirCyanAnimado("--- SENSORES DIRECCIONALES CONECTADOS ---");

            for (int i = 0; i < sensores.Length; i++)
            {
                if (sensoresEstado[i])
                {
                    Textos.ImprimirVerde($"Dirección: {i + 1:D2} - {sensores[i]} [OK - ACTIVO]");
                }
                else
                {
                    Textos.ImprimirRojo($"Dirección: {i + 1:D2} - {sensores[i]} [FALLO - FUERA DE LÍNEA]");
                }
            }
            Textos.ImprimirMagenta("\nPresiona cualquier tecla para volver al menú...");
            Console.ReadKey(true);
        }

        public static void PruebaOperatividad()
        {
            Console.Clear();
            Textos.ImprimirCyanAnimado("--- INICIANDO PRUEBAS DE OPERATIVIDAD ---");
            RegistrarHistorial("Prueba de operatividad de sensores iniciada.");

            bool algunFallo = false;
            for (int i = 0; i < sensores.Length; i++)
            {
                Textos.ImprimirMagenta($"Probando {sensores[i]}...");
                Thread.Sleep(200);

                int probabilidadFallo = simulador.Next(0, 100);

                if (probabilidadFallo > 90)
                {
                    Textos.ImprimirRojo($"¡FALLO DETECTADO en {sensores[i]}!");
                    RegistrarHistorial($"Fallo de comisionado en: {sensores[i]}");
                    sensoresEstado[i] = false;
                    algunFallo = true;
                }
                else
                {
                    Textos.ImprimirVerde("Respuesta correcta [OK]");
                    sensoresEstado[i] = true;
                }
            }

            if (algunFallo)
            {
                Textos.Warning("Se detectaron fallos en uno o más sensores.");
                RegistrarHistorial("Prueba de operatividad finalizada con advertencias.");
            }
            else
            {
                Textos.ImprimirVerde("\nTodas las pruebas finalizaron con éxito.");
                RegistrarHistorial("Prueba de operatividad finalizada con éxito.");
            }

            Textos.ImprimirAmarillo("\nPresiona cualquier tecla para volver al menú...");
            Console.ReadKey(true);
        }

        public static void IniciarMonitoreo()
        {
            Console.Clear();
            DateTime ahora = DateTime.Now;
            Textos.ImprimirAmarillo($"Fecha y hora de inicio: {ahora:dd/MM/yyyy HH:mm:ss}");
            Textos.ImprimirCyanAnimado("--- MONITOREO DE SISTEMA CONTRA INCENDIOS EN TIEMPO REAL ---");
            Textos.ImprimirAmarillo("Presione la tecla ESC para detener el monitoreo...\n");

            RegistrarHistorial("Monitoreo en tiempo real activado.");
            bool alarmaActivada = false;

            while (!alarmaActivada)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey(true);
                    if (tecla.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                int indicePiso = simulador.Next(0, pisos.Length);
                string piso = pisos[indicePiso];

                int estadoSensor = simulador.Next(0, 100);

                if (estadoSensor < 10)
                {
                    int dispositivoAleatorio = simulador.Next(0, 3);
                    int indiceSensorFallo = (indicePiso * 3) + dispositivoAleatorio;
                    sensoresEstado[indiceSensorFallo] = false;

                    string dispositivoFallo = sensores[indiceSensorFallo];
                    string mensajeFallo = $"[FALLO] {piso} - {dispositivoFallo} reporta error / fuera de línea";
                    Textos.ImprimirRojo(mensajeFallo);
                    RegistrarHistorial(mensajeFallo);
                }
                else
                {
                    sensoresEstado[indicePiso * 3] = true;
                    sensoresEstado[indicePiso * 3 + 1] = true;
                    sensoresEstado[indicePiso * 3 + 2] = true;

                    int humoSimulado = simulador.Next(0, 101);
                    int tempSimulada = simulador.Next(15, 101);

                    bool humoCritico = humoSimulado >= 70;
                    bool tempCritica = tempSimulada >= 70;

                    bool humoAdvertencia = humoSimulado >= 30 && humoSimulado < 70;
                    bool tempAdvertencia = tempSimulada >= 45 && tempSimulada < 70;

                    if (humoCritico && tempCritica)
                    {
                        alarmaActivada = true;
                        string alertaIncendio = $"¡INCENDIO DETECTADO! {piso} - Humo: {humoSimulado}%, Temp: {tempSimulada}°C (NIVEL CRÍTICO)";
                        Textos.ImprimirRojo(alertaIncendio);
                        RegistrarHistorial(alertaIncendio);
                        ActivarLucesEstroboscopicas(piso);
                    }
                    else if (humoCritico || tempCritica || humoAdvertencia || tempAdvertencia)
                    {
                        string detalle = "";
                        if (humoCritico) detalle = "Humo crítico, temperatura normal";
                        else if (tempCritica) detalle = "Temperatura crítica, humo normal";
                        else if (humoAdvertencia && tempAdvertencia) detalle = "Humo y temperatura moderados";
                        else if (humoAdvertencia) detalle = "Humo moderado";
                        else detalle = "Temperatura moderada";

                        string alertaAdvertencia = $"[ADVERTENCIA] {piso} - {detalle}. Humo: {humoSimulado}%, Temp: {tempSimulada}°C";
                        Textos.ImprimirAmarillo(alertaAdvertencia);
                        RegistrarHistorial(alertaAdvertencia);
                    }
                    else
                    {
                        Textos.ImprimirVerde($"[OK] {piso} - Sensores activos y funcionando bien. Humo: {humoSimulado}%, Temp: {tempSimulada}°C");
                    }
                }

                Thread.Sleep(1000);
            }

            if (!alarmaActivada)
            {
                RegistrarHistorial("Monitoreo en tiempo real finalizado por el usuario.");
            }

            Textos.ImprimirCyan("\nMonitoreo detenido. Presiona cualquier tecla para volver al menú...");
            Console.ReadKey(true);
        }

        private static void ActivarLucesEstroboscopicas(string piso)
        {
            Textos.ImprimirRojoAnimado($"\n===== 🔥 ACTIVANDO ALARMA DE INCENDIO EN {piso.ToUpper()} 🔥 =====");

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

            Textos.ImprimirMagentaAnimado($"[SISTEMA SCI] LUCES ESTROBOSCÓPICAS ENCIENDEN EN: {piso.ToUpper()}");
            Textos.ImprimirAmarilloAnimado("¡PROCEDA A LA EVACUACIÓN INMEDIATAMENTE!");
        }

        public static void ActivacionManual()
        {
            Console.Clear();

            string[] opcionesManual = new string[pisos.Length + 1];
            for (int i = 0; i < pisos.Length; i++)
            {
                opcionesManual[i] = $"Activar alarma en {pisos[i]}";
            }
            opcionesManual[pisos.Length] = "Volver al menú principal";

            int eleccion = Menu.MostrarMenu("ACTIVACIÓN MANUAL - SELECCIONE PISO", opcionesManual);

            if (eleccion >= 0 && eleccion < pisos.Length)
            {
                string pisoSeleccionado = pisos[eleccion];
                Console.Clear();

                string mensajeEmergencia = $"¡EMERGENCIA! Activación manual de estación manual contra incendios en: {pisoSeleccionado}.";
                Textos.ImprimirRojo(mensajeEmergencia);
                RegistrarHistorial(mensajeEmergencia);

                ActivarLucesEstroboscopicas(pisoSeleccionado);

                Textos.ImprimirMagenta("\nPresiona cualquier tecla para volver al menú principal...");
                Console.ReadKey(true);
            }
            else
            {
                Textos.ImprimirMagentaAnimado("Regresando al menú principal...");
                Thread.Sleep(500);
            }
        }

        public static void MostrarHistorial()
        {
            Console.Clear();
            Textos.ImprimirCyanAnimado("--- HISTORIAL DE ALERTAS Y EVENTOS DEL SISTEMA ---");

            if (historialContador == 0)
            {
                Textos.ImprimirAmarillo("El historial está vacío. No se han registrado eventos.");
            }
            else
            {
                for (int i = 0; i < historialContador; i++)
                {
                    string evento = historial[i];
                    if (evento.Contains("INCENDIO") || evento.Contains("EMERGENCIA"))
                    {
                        Textos.ImprimirRojo(evento);
                    }
                    else if (evento.Contains("ADVERTENCIA") || evento.Contains("FALLO"))
                    {
                        Textos.ImprimirAmarillo(evento);
                    }
                    else
                    {
                        Textos.ImprimirVerde(evento);
                    }
                }
            }

            Textos.ImprimirMagenta("\nPresiona cualquier tecla para volver al menú...");
            Console.ReadKey(true);
        }
    }
}
