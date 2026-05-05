using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TaskFlow.Models;
using Taskflow.Utils;

namespace TaskFlow.Services
{
    public class TaskService
    {
        private List<TaskItem> _tasks = new List<TaskItem>();
        private readonly string _filePath = "data/tasks.json";

        public TaskService()
        {
            CargarTareas();
        }

        public void CrearTarea(string titulo, string descripcion, string responsable)
        {
            if (!EsResponsableValido(responsable))
            {
                ConsoleHelper.MostrarError("\n✗ Responsable inválido. Debe tener al menos 3 letras y no contener números, espacios al inicio o al final ni caracteres especiales.");
                return;
            }
            var tarea = new TaskItem
            {
                Id = _tasks.Count + 1,
                Titulo = titulo,
                Descripcion = descripcion,
                Responsable = responsable,
                Estado = "Pendiente",
                FechaCreacion = DateTime.Now
            };
            _tasks.Add(tarea);
            GuardarTareas();
            ConsoleHelper.MostrarExito($"\n Tarea '{titulo}' creada correctamente con ID {tarea.Id}");
        }

        public void ListarTareas(string filtro = "todas")
        {
            var tareasFiltradas = filtro.ToLower() switch
            {
                "pendiente"   => _tasks.Where(t => t.Estado == "Pendiente").ToList(),
                "en progreso" => _tasks.Where(t => t.Estado == "En progreso").ToList(),
                "completada"  => _tasks.Where(t => t.Estado == "Completada").ToList(),
                _             => _tasks
            };

            if (!tareasFiltradas.Any())
            {
                ConsoleHelper.MostrarInfo("\nNo hay tareas para mostrar.");
                return;
            }

            ConsoleHelper.MostrarTitulo("LISTADO DE TAREAS");
            foreach (var t in tareasFiltradas)
            {
                ConsoleHelper.MostrarInfo($"ID: {t.Id} | Titulo: {t.Titulo} | Responsable: {t.Responsable} | Estado: {t.Estado}");
                ConsoleHelper.MostrarInfo($"   Creada: {t.FechaCreacion:dd/MM/yyyy HH:mm} | Modificada: {(t.FechaActualizacion.HasValue ? t.FechaActualizacion.Value.ToString("dd/MM/yyyy HH:mm") : "Sin modificaciones")}");
                ConsoleHelper.MostrarInfo(new string('-', 60));
            }
        }
        public void EstadoTarea(int id, string nuevoEstado)
        {
            var tarea = _tasks.FirstOrDefault(t => t.Id == id);

            if (tarea == null)
            {
                ConsoleHelper.MostrarError($"\n✗ No existe una tarea con ID {id}");
                return;
            }

            var estadosValidos = new List<string> { "Pendiente", "En progreso", "Completada" };

            if (!estadosValidos.Contains(nuevoEstado))
            {
                ConsoleHelper.MostrarError("\n✗ Estado inválido.");
                return;
            }

            tarea.Estado = nuevoEstado;
            tarea.FechaActualizacion = DateTime.Now;
            GuardarTareas();

            ConsoleHelper.MostrarExito($"\n✓ Estado de la tarea {id} actualizado a '{nuevoEstado}'");
        }

        private void CargarTareas()
        {
            try
            {

                if (File.Exists(_filePath))
              {
                string json = File.ReadAllText(_filePath);
                _tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) 
                        ?? new List<TaskItem>();
              }
            }
            catch (Exception ex)
            {
                ConsoleHelper.MostrarError($"Error al cargar tareas: {ex.Message}");
                _tasks = new List<TaskItem>();
            }
        }

        private bool EsResponsableValido(string responsable)
        {
            if (string.IsNullOrWhiteSpace(responsable))
                return false;

            
            if (responsable != responsable.Trim())
                return false;

            if (responsable.Length < 3)
                return false;

            return responsable.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        private void GuardarTareas()
        {
            try
            {
                Directory.CreateDirectory("data");
                string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                ConsoleHelper.MostrarError($"Error al guardar tareas: {ex.Message}");
            }
        }


        public bool ExisteTarea(int id)
        {
            return _tasks.Any(t => t.Id == id);
        }

        public void ActualizarResponsable(int id, string nuevoResponsable)
        {
            var tarea = _tasks.FirstOrDefault(t => t.Id == id);

            if (tarea == null)
            {
                ConsoleHelper.MostrarError($"\n✗ No existe una tarea con ID {id}");
                return;
            }

            if (!EsResponsableValido(nuevoResponsable))
            {
                ConsoleHelper.MostrarError("\n✗ Responsable inválido. Debe tener al menos 3 letras y no contener números, espacios al inicio o al final ni caracteres especiales.");
                return;
            }

            if (tarea.Responsable == nuevoResponsable)
            {
                ConsoleHelper.MostrarError("\n⚠ El responsable ya es el mismo.");
                return;
            }

            tarea.Responsable = nuevoResponsable;
            tarea.FechaActualizacion = DateTime.Now;

            GuardarTareas();

            ConsoleHelper.MostrarExito($"\n✓ Responsable de la tarea {id} actualizado a '{nuevoResponsable}'");
        }
        
    }
}
