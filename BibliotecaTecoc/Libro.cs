using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaTecoc
{
    public class Libro
    {
        public string Id { get; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string Estado { get; private set; }

        public Libro(string id, string titulo, string autor, string genero, string estado = "disponible")
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Genero = genero;
            Estado = estado;
        }

        public void ActualizarEstado(string nuevoEstado)
        {
            Estado = nuevoEstado;
        }
    }
}
