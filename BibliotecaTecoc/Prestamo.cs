using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaTecoc
{
    public class Prestamo
    {
        public Libro Libro { get; }
        public Usuario Usuario { get; }
        public DateTime FechaPrestamo { get; }
        public DateTime FechaVencimiento { get; }
        private readonly INotificacionService _notificacionService;

        public Prestamo(Libro libro, Usuario usuario, DateTime fechaPrestamo, INotificacionService notificacionService)
        {
            Libro = libro;
            Usuario = usuario;
            FechaPrestamo = fechaPrestamo;
            FechaVencimiento = fechaPrestamo.AddDays(14); // 2 semanas de préstamo por defecto
            _notificacionService = notificacionService;
        }

        public void DevolverLibro()
        {
            if (DateTime.Now > FechaVencimiento)
            {
                _notificacionService.NotificarRetraso(Usuario, Libro);
            }
            Libro.ActualizarEstado("disponible");
            Usuario.AgregarPrestamo(this);
        }
    }
}
