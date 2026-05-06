using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Services;
using Taskflow.Utils;

namespace TaskFlow
{
    class Program
    {
        static void Main(string[] args)
        { 
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            TaskService taskService = new TaskService();
            bool salir = false;

            while (!salir)
            {
                ConsoleHelper.MostrarMenu(new string[]
                {
                    "1.➕ Crear nueva tarea",
                    "2.📋 Listar tareas",
                    "3.🔄 Cambiar estado de tarea",
                    "4.👤 Actualizar responsable",
                    "5.🗑️ Eliminar tarea",
                    "6.🚪 Salir"
                });

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ConsoleHelper.MostrarTitulo("Crear nueva tarea");

                        Console.Write("Título (obligatorio): ");
                        string titulo = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(titulo))
                        {
                            ConsoleHelper.MostrarError("El título es obligatorio.");
                            ConsoleHelper.Pausar();
                            break;
                        }

                        Console.Write("Descripción (opcional): ");
                        string descripcion = Console.ReadLine();

                        Console.Write("Responsable: ");
                        string responsable = Console.ReadLine();

                        taskService.CrearTarea(titulo, descripcion, responsable);
                        ConsoleHelper.Pausar();
                        break;

                    case "2":
                    ConsoleHelper.MostrarTitulo("Listar tareas");
    
                    // Submenú de filtros
                    ConsoleHelper.MostrarInfo("¿Qué tareas querés ver?");
                    Console.WriteLine("  1. 📋 Todas las tareas");
                    Console.WriteLine("  2. ⏳ Solo pendientes");
                    Console.WriteLine("  3. 🔄 Solo en progreso");
                    Console.WriteLine("  4. ✅ Solo completadas");
                    Console.Write("\nElegí una opción: ");
    
                    string opcionFiltro = Console.ReadLine();
                    string filtro = opcionFiltro switch
                    {
                        "2" => "pendiente",
                        "3" => "en progreso",
                        "4" => "completada",
                        _ => "todas"
                    };
    
                    taskService.ListarTareas(filtro);
                    ConsoleHelper.Pausar();
                    break;

                   
                    case "3":
                    {
                        ConsoleHelper.MostrarTitulo("Cambiar estado de tarea");

                        Console.Write("ID de la tarea: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            ConsoleHelper.MostrarError("ID inválido.");
                            ConsoleHelper.Pausar();
                            break;
                        }
                    

                        ConsoleHelper.MostrarInfo("Selecciona el nuevo estado:");
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
                            ConsoleHelper.MostrarError("Opción de estado inválida.");
                        }
                        else
                        {
                            taskService.EstadoTarea(id, nuevoEstado);
                        }

                        ConsoleHelper.Pausar();
                        break;
                    }

                    case "4":
                        ConsoleHelper.MostrarTitulo("Actualizar responsable de tarea");

                        Console.Write("ID de la tarea: ");
                        if (!int.TryParse(Console.ReadLine(), out int idResponsable))
                        {
                            ConsoleHelper.MostrarError("ID inválido.");
                            ConsoleHelper.Pausar();
                            break;
                        }

                        if (!taskService.ExisteTarea(idResponsable))
                        {
                            ConsoleHelper.MostrarError("No existe una tarea con ese ID.");
                            ConsoleHelper.Pausar();
                            break;
                        }

                        Console.Write("Nuevo responsable: ");
                        string nuevoResponsable = Console.ReadLine();

                        taskService.ActualizarResponsable(idResponsable, nuevoResponsable);
                        ConsoleHelper.Pausar();
                        break;
                    case "5":
                    {
                        Console.Clear();
                        Console.WriteLine("=== Eliminar tarea ===\n");
                        Console.Write("Ingresá el ID de la tarea a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            taskService.EliminarTarea(id);
                        }
                        else
                        {
                        Console.WriteLine("\n✗ ID inválido.");
                        }
                        Console.ReadKey();
                        break;
                    }   

                    case "6":
                        ConsoleHelper.MostrarInfo("¡Gracias por usar TaskFlow! Hasta luego.");
                        salir = true;
                        break;

                    default:
                        ConsoleHelper.MostrarError("Opción inválida. Por favor, selecciona una opción del menú.");
                        ConsoleHelper.Pausar();
                        break;
                }
            }
        }
    }
}
