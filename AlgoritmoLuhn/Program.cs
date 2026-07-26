using System;
using System.IO;

class Program
{
    // Variables globales para llevar el conteo de estadísticas
    static int tarjetasValidas = 0, tarjetasInvalidas = 0;
    static int visa = 0, mastercard = 0, amex = 0, discover = 0;

    static void Main()
    {
        int opcion;
        do
        {
            // Limpia la consola en cada iteración del menú
            Console.Clear();
            Console.WriteLine("===== VALIDADOR DE TARJETAS =====");
            Console.WriteLine("1. Validar una tarjeta");
            Console.WriteLine("2. Validar desde archivo");
            Console.WriteLine("3. Generar número válido");
            Console.WriteLine("4. Estadísticas");
            Console.WriteLine("5. Salir");
            Console.Write("Opción: ");

            // Valida que el usuario ingrese un entero válido, si no, asigna 0
            if (!int.TryParse(Console.ReadLine(), out opcion))
                opcion = 0;

            switch (opcion)
            {
                case 1:
                    Console.Write("Número: ");
                    // Lee el texto ingresado, remueve espacios y procesa la tarjeta
                    ProcesarTarjeta(Console.ReadLine()?.Trim() ?? "");
                    break;

                case 2:
                    Console.Write("Ruta del archivo: ");
                    // Lee la ruta y procesa las tarjetas contenidas en el archivo
                    ValidarDesdeArchivo(Console.ReadLine() ?? "");
                    break;

                case 3:
                    // Genera dinámicamente un número válido de prueba
                    string t = GenerarNumeroValido();
                    Console.WriteLine($"\nNúmero: {t}");
                    Console.WriteLine($"Marca: {IdentificarMarca(t)}");
                    Console.WriteLine("Estado: ✅ VÁLIDA");
                    break;

                case 4:
                    // Despliega el resumen de estadísticas acumuladas
                    MostrarEstadisticas();
                    break;
            }

            // Pausa la ejecución para que el usuario pueda leer el resultado
            if (opcion != 5)
            {
                Console.WriteLine("\nPresione una tecla...");
                Console.ReadKey();
            }

        } while (opcion != 5); // Repite hasta que la opción sea 5
    }

    // Procesa una tarjeta individual, evalúa su validez e incrementa contadores
    static void ProcesarTarjeta(string numero)
    {
        bool ok = ValidarTarjeta(numero);
        string marca = IdentificarMarca(numero);

        Console.WriteLine($"Marca: {marca}");
        Console.WriteLine(ok ? "Estado: ✅ VÁLIDA" : "Estado: ❌ INVÁLIDA");

        // Actualiza los contadores globales según el resultado
        if (ok) tarjetasValidas++; else tarjetasInvalidas++;
        ContarMarca(marca);
    }

    // Lee un archivo texto de forma secuencial y evalúa cada línea
    static void ValidarDesdeArchivo(string ruta)
    {
        try
        {
            foreach (var linea in File.ReadAllLines(ruta))
            {
                Console.WriteLine("----------------");
                Console.WriteLine($"Número: {linea.Trim()}");
                ProcesarTarjeta(linea.Trim());
            }
        }
        catch(Exception ex)
        {
            // Muestra mensaje de error si el archivo no existe o no se puede leer
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Valida la tarjeta comprobando longitud y aplicando el Algoritmo de Luhn
    static bool ValidarTarjeta(string numero)
    {
        // Verifica que la cadena no esté vacía y tenga una longitud entre 13 y 19 dígitos
        if (string.IsNullOrWhiteSpace(numero) || numero.Length < 13 || numero.Length > 19)
            return false;

        int suma = 0;
        bool dup = false; // Controla qué dígitos se deben duplicar

        // Algoritmo de Luhn: Recorre la tarjeta desde el último dígito hacia el primero
        for (int i = numero.Length - 1; i >= 0; i--)
        {
            // Retorna falso si detecta un carácter que no es un número
            if (!char.IsDigit(numero[i])) return false;
            
            int d = numero[i] - '0'; // Convierte char a entero
            if (dup)
            {
                d *= 2; // Duplica cada segundo dígito
                if (d > 9) d -= 9; // Si supera 9, resta 9 (suma de sus dos dígitos)
            }
            
            suma += d;
            dup = !dup; // Alterna la bandera para el próximo dígito
        }

        // Si la suma total es divisible por 10, la tarjeta es válida
        return suma % 10 == 0;
    }

    // Identifica la franquicia analizando la longitud y los dígitos iniciales (BIN)
    static string IdentificarMarca(string n)
    {
        if (string.IsNullOrWhiteSpace(n)) return "Desconocida";

        // Visa: Empieza con 4 y tiene 13 o 16 dígitos
        if (n.StartsWith("4") && (n.Length == 13 || n.Length == 16))
            return "Visa";

        // Mastercard: Tiene 16 dígitos y empieza entre 51 y 55
        if (n.Length == 16 && int.TryParse(n.Substring(0, 2), out int p2) && p2 >= 51 && p2 <= 55)
            return "Mastercard";

        // American Express: Tiene 15 dígitos y empieza con 34 o 37
        if (n.Length == 15 && (n.StartsWith("34") || n.StartsWith("37")))
            return "American Express";

        // Discover: Entre 16 y 19 dígitos con prefijos específicos (6011, 65, 644-649 o 622126-622925)
        if (n.Length >= 16 && n.Length <= 19)
        {
            if (n.StartsWith("6011") || n.StartsWith("65"))
                return "Discover";
            if (int.TryParse(n.Substring(0, 3), out int p3) && p3 >= 644 && p3 <= 649)
                return "Discover";
            if (int.TryParse(n.Substring(0, 6), out int p6) && p6 >= 622126 && p6 <= 622925)
                return "Discover";
        }

        return "Desconocida";
    }

    // Genera un número aleatorio de 16 dígitos que pase la prueba de Luhn (Marca Visa)
    static string GenerarNumeroValido()
    {
        Random r = new Random();
        string baseNum = "4"; // Empieza en 4 (Visa)
        
        // Agrega 14 dígitos aleatorios
        for (int i = 0; i < 14; i++) baseNum += r.Next(10);
        
        // Prueba cuál último dígito (del 0 al 9) hace que la tarjeta sea válida
        for (int i = 0; i <= 9; i++)
        {
            string n = baseNum + i;
            if (ValidarTarjeta(n)) return n;
        }
        
        return "";
    }

    // Muestra en pantalla el resumen de todas las tarjetas analizadas
    static void MostrarEstadisticas()
    {
        Console.WriteLine($"Válidas: {tarjetasValidas}");
        Console.WriteLine($"Inválidas: {tarjetasInvalidas}");
        Console.WriteLine($"Visa: {visa}");
        Console.WriteLine($"Mastercard: {mastercard}");
        Console.WriteLine($"American Express: {amex}");
        Console.WriteLine($"Discover: {discover}");
    }

    // Suma 1 al contador correspondiente según la marca de la tarjeta
    static void ContarMarca(string m)
    {
        switch (m)
        {
            case "Visa": visa++; break;
            case "Mastercard": mastercard++; break;
            case "American Express": amex++; break;
            case "Discover": discover++; break;
        }
    }
}
