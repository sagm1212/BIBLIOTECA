namespace BibliotecaTecoc
{
    public class NotificacionService : INotificacionService
    {
        public void NotificarRetraso(Usuario usuario, Libro libro)
        {
            Console.WriteLine($"Notificación: El usuario {usuario.Nombre} tiene retraso en la devolución del libro {libro.Titulo}.");
        }
    }
}
