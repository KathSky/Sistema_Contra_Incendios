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
                string[] opciones = { "ver sensores iniciados", "realizar prueba de operatividad", "iniciar monitoreo en tiempo real", "salir" };
                eleccion = Menu.MostrarMenu("PANEL CENTRAL SCI - AVANCE PROYECTO", opciones);
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
                    default:
                        Textos.ImprimirMagentaAnimado("saliendo el sistema.....");
                        break;
                }
            }
            while (eleccion != 3);

            Textos.ImprimirMagentaAnimado("Gracias por utilizar el sistema de control de incendios. ¡Hasta luego!");
            Console.ReadKey();
        }
    }   
}
