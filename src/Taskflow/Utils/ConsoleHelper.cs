using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskflow.Utils
{
    public static class ConsoleHelper
    {
        public static void MostrarTitulo(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine($"║{titulo,-38}║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }

        public static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarInfo(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n➜ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarMenu(string[] opciones)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║         TASKFLOW - NovaTech          ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            foreach (var opcion in opciones)
            {
                Console.WriteLine($"║{opcion,-37}║");
            }
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("\nElegí una opción: ");
        }

        public static void Pausar()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPresioná cualquier tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
