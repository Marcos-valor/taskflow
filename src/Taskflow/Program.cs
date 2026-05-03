using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Services;

namespace TaskFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskService taskService = new TaskService();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== TaskFlow ===\n");
                Console.WriteLine("1. Crear tarea");
                Console.WriteLine("2. Listar tareas");
                Console.WriteLine("3. Cambiar estado de tarea");
                Console.WriteLine("4. Actualizar responsable");
                Console.WriteLine("5. Salir");
                Console.Write("\nElegí una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=== Crear nueva tarea ===\n");

                        Console.Write("Título (obligatorio): ");
                        string titulo = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(titulo))
                        {
                            Console.WriteLine("\n✗ El título es obligatorio.");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Descripción (opcional): ");
                        string descripcion = Console.ReadLine();

                        Console.Write("Responsable: ");
                        string responsable = Console.ReadLine();

                        taskService.CrearTarea(titulo, descripcion, responsable);
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== Listar tareas ===\n");
                        taskService.ListarTareas();
                        Console.ReadKey();
                        break;

                   
                    case "3":
                        Console.Clear();
                        Console.WriteLine("=== Cambiar estado de tarea ===\n");

                        Console.Write("ID de la tarea: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("ID inválido");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("\nNuevo estado:");
                        Console.WriteLine("1. Pendiente");
                        Console.WriteLine("2. En progreso");
                        Console.WriteLine("3. Completada");
                        Console.Write("Elegí una opción: ");

                        string opcionEstado = Console.ReadLine();

                        string nuevoEstado = opcionEstado switch
                        {
                            "1" => "Pendiente",
                            "2" => "En progreso",
                            "3" => "Completada",
                            _ => null
                        };

                        if (nuevoEstado == null)
                        {
                            Console.WriteLine("\n✗ Opción inválida.");
                        }
                        else
                        {
                            taskService.EstadoTarea(id, nuevoEstado);
                        }

                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("=== Actualizar responsable ===\n");

                        Console.Write("ID de la tarea: ");
                        if (!int.TryParse(Console.ReadLine(), out int idResponsable))
                        {
                            Console.WriteLine("ID inválido");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Nuevo responsable: ");
                        string nuevoResponsable = Console.ReadLine();

                        taskService.ActualizarResponsable(idResponsable, nuevoResponsable);

                        Console.ReadKey();
                        break;

                    case "5":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("\n✗ Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
