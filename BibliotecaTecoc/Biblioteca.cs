namespace BibliotecaTecoc
{
    public class Biblioteca
    {
        private readonly List<Libro> _libros;
        private readonly List<Usuario> _usuarios;

        public Biblioteca()
        {
            _libros = new List<Libro>();
            _usuarios = new List<Usuario>();
        }

        public void AgregarLibro(Libro libro)
        {
            _libros.Add(libro);
        }

        public void EliminarLibro(string idLibro)
        {
            _libros.RemoveAll(l => l.Id == idLibro);
        }

        public void ActualizarLibro(string idLibro, string titulo, string autor, string genero)
        {
            var libro = _libros.FirstOrDefault(l => l.Id == idLibro);
            if (libro != null)
            {
                libro.Titulo = titulo;
                libro.Autor = autor;
                libro.Genero = genero;
            }
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }

        public void RegistrarPrestamo(Libro libro, Usuario usuario)
        {
            if (libro.Estado == "disponible")
            {
                libro.ActualizarEstado("prestado");
                var prestamo = new Prestamo(libro, usuario, DateTime.Now, new NotificacionService());
                usuario.AgregarPrestamo(prestamo);
            }
        }

        public void RegistrarDevolucion(Prestamo prestamo)
        {
            prestamo.DevolverLibro();
        }

        public IEnumerable<Libro> BuscarLibros(string criterio, string valor)
        {
            return criterio.ToLower() switch
            {
                "titulo" => _libros.Where(l => l.Titulo.Contains(valor, StringComparison.OrdinalIgnoreCase)),
                "autor" => _libros.Where(l => l.Autor.Contains(valor, StringComparison.OrdinalIgnoreCase)),
                "genero" => _libros.Where(l => l.Genero.Contains(valor, StringComparison.OrdinalIgnoreCase)),
                _ => new List<Libro>()
            };
        }

        public IEnumerable<Libro> ObtenerLibrosMasPrestados()
        {
            return _libros; // Lógica para obtener los libros más prestados.
        }

        public IEnumerable<Prestamo> ObtenerLibrosVencidos()
        {
            return _usuarios.SelectMany(u => u.HistorialPrestamos)
                            .Where(p => p.FechaVencimiento < DateTime.Now && p.Libro.Estado == "prestado");
        }

        public IEnumerable<Libro> ObtenerTodosLosLibros()
        {
            return _libros;
        }

        public IEnumerable<Usuario> ObtenerTodosLosUsuarios()
        {
            return _usuarios;
        }

        public Libro BuscarLibroPorId(string idLibro)
        {
            return _libros.FirstOrDefault(l => l.Id == idLibro);
        }

        public Usuario BuscarUsuarioPorId(string idUsuario)
        {
            return _usuarios.FirstOrDefault(u => u.Id == idUsuario);
        }

        public Usuario BuscarUsuarioConLibroPrestado(Libro libro)
        {
            return _usuarios.FirstOrDefault(u => u.HistorialPrestamos.Any(p => p.Libro.Id == libro.Id && p.Libro.Estado == "prestado"));
        }
    }
}
