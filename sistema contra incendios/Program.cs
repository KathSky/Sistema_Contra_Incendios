using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyTools;


namespace sistema_contra_incendios
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int eleccion = 0;
            do
            {
                string[] opciones = {
                    "Ver sensores iniciados",
                    "Realizar prueba de operatividad",
                    "Iniciar monitoreo en tiempo real",
                    "Activación manual de alarma por piso",
                    "Ver historial de alertas y eventos",
                    "Salir del sistema"
                };
                eleccion = Menu.MostrarMenu("PANEL CENTRAL SCI - SISTEMA CONTRA INCENDIOS", opciones);
                switch (eleccion)
                {
                    case 0:
                        PanelCentral.MostrarSensores();
                        break;
                    case 1:
                        PanelCentral.PruebaOperatividad();
                        break;
                    case 2:
                        PanelCentral.IniciarMonitoreo();
                        break;
                    case 3:
                        PanelCentral.ActivacionManual();
                        break;
                    case 4:
                        PanelCentral.MostrarHistorial();
                        break;
                    default:
                        Textos.ImprimirMagentaAnimado("Saliendo del sistema.....");
                        break;
                }
            }
            while (eleccion != 5);

            Textos.ImprimirMagentaAnimado("Gracias por utilizar el sistema de control de incendios. ¡Hasta luego!");
            Console.ReadKey();
        }
    }
}
