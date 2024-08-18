using BibliotecaTecoc;

public class Program
{
    private static void Main(string[] args)
    {
        // Crear una instancia de la biblioteca
        var biblioteca = new Biblioteca();

        // Agregar algunos libros a la biblioteca
        biblioteca.AgregarLibro(new Libro("1", "Cien Años de Soledad", "Gabriel García Márquez", "Novela"));
        biblioteca.AgregarLibro(new Libro("2", "1984", "George Orwell", "Distopía"));
        biblioteca.AgregarLibro(new Libro("3", "Don Quijote de la Mancha", "Miguel de Cervantes", "Novela"));

        // Registrar algunos usuarios
        var usuario1 = new Usuario("1", "Juan Pérez", "Calle Falsa 123");
        var usuario2 = new Usuario("2", "María García", "Avenida Siempreviva 456");
        biblioteca.RegistrarUsuario(usuario1);
        biblioteca.RegistrarUsuario(usuario2);

        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\nSeleccione una opción:");
            Console.WriteLine("1. Ver lista de libros");
            Console.WriteLine("2. Ver lista de usuarios");
            Console.WriteLine("3. Registrar un préstamo");
            Console.WriteLine("4. Registrar una devolución");
            Console.WriteLine("5. Buscar libros");
            Console.WriteLine("6. Consultar libros vencidos");
            Console.WriteLine("7. Ver libros más prestados");
            Console.WriteLine("8. Salir");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MostrarListaDeLibros(biblioteca);
                    break;
                case "2":
                    MostrarListaDeUsuarios(biblioteca);
                    break;
                case "3":
                    RegistrarPrestamo(biblioteca);
                    break;
                case "4":
                    RegistrarDevolucion(biblioteca);
                    break;
                case "5":
                    BuscarLibros(biblioteca);
                    break;
                case "6":
                    ConsultarLibrosVencidos(biblioteca);
                    break;
                case "7":
                    VerLibrosMasPrestados(biblioteca);
                    break;
                case "8":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                    break;
            }
        }
    }

    static void MostrarListaDeLibros(Biblioteca biblioteca)
    {
        Console.WriteLine("\nLista de libros disponibles en la biblioteca:");
        foreach (var libro in biblioteca.ObtenerTodosLosLibros())
        {
            Console.WriteLine($"ID: {libro.Id}, Título: {libro.Titulo}, Autor: {libro.Autor}, Género: {libro.Genero}, Estado: {libro.Estado}");
        }
    }

    static void MostrarListaDeUsuarios(Biblioteca biblioteca)
    {
        Console.WriteLine("\nLista de usuarios registrados en la biblioteca:");
        foreach (var usuario in biblioteca.ObtenerTodosLosUsuarios())
        {
            Console.WriteLine($"ID: {usuario.Id}, Nombre: {usuario.Nombre}, Dirección: {usuario.Direccion}");
        }
    }

    static void RegistrarPrestamo(Biblioteca biblioteca)
    {
        Console.WriteLine("\nIngrese el ID del libro que desea prestar:");
        var idLibro = Console.ReadLine();
        var libro = biblioteca.BuscarLibroPorId(idLibro);

        if (libro == null || libro.Estado != "disponible")
        {
            Console.WriteLine("El libro no está disponible o no existe.");
            return;
        }

        Console.WriteLine("Ingrese el ID del usuario que realiza el préstamo:");
        var idUsuario = Console.ReadLine();
        var usuario = biblioteca.BuscarUsuarioPorId(idUsuario);

        if (usuario == null)
        {
            Console.WriteLine("El usuario no existe.");
            return;
        }

        biblioteca.RegistrarPrestamo(libro, usuario);
        Console.WriteLine($"El libro '{libro.Titulo}' ha sido prestado a {usuario.Nombre}.");
    }

    static void RegistrarDevolucion(Biblioteca biblioteca)
    {
        Console.WriteLine("\nIngrese el ID del libro que desea devolver:");
        var idLibro = Console.ReadLine();
        var libro = biblioteca.BuscarLibroPorId(idLibro);

        if (libro == null || libro.Estado != "prestado")
        {
            Console.WriteLine("El libro no está prestado o no existe.");
            return;
        }

        var usuario = biblioteca.BuscarUsuarioConLibroPrestado(libro);

        if (usuario == null)
        {
            Console.WriteLine("No se encontró un usuario con este libro prestado.");
            return;
        }

        var prestamo = usuario.HistorialPrestamos.FirstOrDefault(p => p.Libro.Id == libro.Id);

        if (prestamo != null)
        {
            biblioteca.RegistrarDevolucion(prestamo);
            Console.WriteLine($"El libro '{libro.Titulo}' ha sido devuelto por {usuario.Nombre}.");
        }
    }

    static void BuscarLibros(Biblioteca biblioteca)
    {
        Console.WriteLine("\nIngrese el criterio de búsqueda (titulo, autor, genero):");
        var criterio = Console.ReadLine();
        Console.WriteLine("Ingrese el valor a buscar:");
        var valor = Console.ReadLine();

        var libros = biblioteca.BuscarLibros(criterio, valor);

        if (!libros.Any())
        {
            Console.WriteLine("No se encontraron libros que coincidan con la búsqueda.");
        }
        else
        {
            Console.WriteLine("Libros encontrados:");
            foreach (var libro in libros)
            {
                Console.WriteLine($"ID: {libro.Id}, Título: {libro.Titulo}, Autor: {libro.Autor}, Género: {libro.Genero}, Estado: {libro.Estado}");
            }
        }
    }

    static void ConsultarLibrosVencidos(Biblioteca biblioteca)
    {
        var librosVencidos = biblioteca.ObtenerLibrosVencidos();

        if (!librosVencidos.Any())
        {
            Console.WriteLine("\nNo hay libros vencidos.");
        }
        else
        {
            Console.WriteLine("\nLibros vencidos:");
            foreach (var prestamo in librosVencidos)
            {
                Console.WriteLine($"Título: {prestamo.Libro.Titulo}, Usuario: {prestamo.Usuario.Nombre}, Fecha de vencimiento: {prestamo.FechaVencimiento.ToShortDateString()}");
            }
        }
    }

    static void VerLibrosMasPrestados(Biblioteca biblioteca)
    {
        var librosMasPrestados = biblioteca.ObtenerLibrosMasPrestados();

        Console.WriteLine("\nLibros más prestados:");
        foreach (var libro in librosMasPrestados)
        {
            Console.WriteLine($"Título: {libro.Titulo}, Autor: {libro.Autor}, Género: {libro.Genero}, Estado: {libro.Estado}");
        }
    }
}

