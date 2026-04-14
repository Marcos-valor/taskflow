using System.Collections.Generic;
using System;
using Taskflow.Models;

namespace TaskFlow.Services
{
    public class TaskService
    {
        private List<TaskItem> _tasks = new List<TaskItem>();

        public void CrearTarea(string titulo, string descripcion, string responsable)
        {
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
            Console.WriteLine($"\n✓ Tarea '{titulo}' creada correctamente con ID {tarea.Id}");
        }
    }
}
