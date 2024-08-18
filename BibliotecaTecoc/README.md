1. Single Responsibility Principle (SRP) - Principio de Responsabilidad Única
Este principio establece que una clase debe tener una única responsabilidad o motivo para cambiar. En otras palabras, cada clase debe tener una sola razón de ser.

Aplicación en mi código:

Clase Libro: Su única responsabilidad es manejar los datos y el estado de un libro.
Clase Usuario: Su única responsabilidad es manejar los datos y el historial de préstamos de un usuario.
Clase Biblioteca: Su responsabilidad es coordinar las interacciones entre los libros, los usuarios y los préstamos, pero no debería manejar las notificaciones o cualquier otra funcionalidad no relacionada directamente con la gestión de la biblioteca.
2. Open/Closed Principle (OCP) - Principio de Abierto/Cerrado
Este principio sugiere que las clases deben estar abiertas para la extensión pero cerradas para la modificación. Es decir, debes poder añadir nuevas funcionalidades sin cambiar el código existente.

Aplicación en tu código:

Métodos como BuscarLibros: El método utiliza un switch para permitir la búsqueda por diferentes criterios. Si necesitas agregar más criterios de búsqueda, podrías extender este método sin modificarlo directamente. Esto se puede mejorar con un patrón como el patrón de estrategia para adherirse mejor al OCP.
Posible Mejora: Puedes usar interfaces y clases derivadas para extender las funcionalidades como notificaciones y tipos de préstamos, sin modificar la clase Biblioteca en sí.
3. Liskov Substitution Principle (LSP) - Principio de Sustitución de Liskov
Este principio establece que los objetos de una clase derivada deben ser reemplazables por objetos de su clase base sin alterar el correcto funcionamiento del programa.

Aplicación en tu código:

Intercambiabilidad de Clases: Aunque en el código actual no hay una herencia directa aplicada, al usar interfaces para ciertos servicios (como las notificaciones), podrías asegurar que cualquier clase que implemente esa interfaz podría ser intercambiada sin problemas.

Si decidieras, por ejemplo, tener una clase LibroEspecial que extienda Libro, esta debería ser capaz de ser utilizada dondequiera que Libro es utilizado, sin romper el comportamiento esperado del sistema.

4. Interface Segregation Principle (ISP) - Principio de Segregación de Interfaces
Este principio establece que los clientes no deben verse forzados a depender de interfaces que no utilizan. Es decir, las interfaces deben ser específicas y pequeñas para cumplir con su propósito.

Aplicación en tu código:

5. Dependency Inversion Principle (DIP) - Principio de Inversión de Dependencias
Este principio sugiere que las clases deben depender de abstracciones (interfaces) en lugar de clases concretas.

Aplicación en tu código:

Notificación en Préstamo: La clase Prestamo depende de NotificacionService, lo cual es una implementación concreta. 

public Prestamo(Libro libro, Usuario usuario, DateTime fechaPrestamo, INotificacionService notificacionService)
{
    // ...
}

Aquí, INotificacionService sería una interfaz, y voy a poder pasar diferentes implementaciones concretas de notificación (como notificaciones por email, SMS, etc.) sin modificar la clase Prestamo.
