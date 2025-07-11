using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Configuration
{
    /// <summary>
    /// Clase est�tica para manejar el seeding de datos iniciales en la base de datos
    /// </summary>
    public static class DatabaseSeeder
    {
        /// <summary>
        /// M�todo de extensi�n para inicializar datos en el contexto de la base de datos
        /// </summary>
        /// <param name="context">Contexto de la base de datos</param>
        /// <param name="diasFunciones">N�mero de d�as para generar funciones (por defecto 7)</param>
        public static void SeedData(this CineDomiruthContext context, int diasFunciones = 7)
        {
            // Evitar duplicaci�n de datos
            if (context.Peliculas.Any()) 
            {
                Console.WriteLine("Los datos ya existen en la base de datos. Saltando seeding.");
                return;
            }

            Console.WriteLine("Iniciando seeding de datos...");

            using var transaction = context.Database.BeginTransaction();

            try
            {
                SeedGeneros(context);
                SeedSalas(context);
                SeedPeliculas(context);
                SeedFunciones(context, diasFunciones);

                transaction.Commit();
                Console.WriteLine("Seeding completado exitosamente.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error durante el seeding: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sembrar g�neros de pel�culas
        /// </summary>
        private static void SeedGeneros(CineDomiruthContext context)
        {
            Console.WriteLine("Sembrando g�neros...");
            context.Generos.AddRange(SeedDataConstants.Generos);
            context.SaveChanges();
            Console.WriteLine($"Se agregaron {SeedDataConstants.Generos.Count} g�neros.");
        }

        /// <summary>
        /// Sembrar salas de cine
        /// </summary>
        private static void SeedSalas(CineDomiruthContext context)
        {
            Console.WriteLine("Sembrando salas...");
            context.Salas.AddRange(SeedDataConstants.Salas);
            context.SaveChanges();
            Console.WriteLine($"Se agregaron {SeedDataConstants.Salas.Count} salas.");
        }

        /// <summary>
        /// Sembrar pel�culas
        /// </summary>
        private static void SeedPeliculas(CineDomiruthContext context)
        {
            Console.WriteLine("Sembrando pel�culas...");
            context.Peliculas.AddRange(SeedDataConstants.Peliculas);
            context.SaveChanges();
            Console.WriteLine($"Se agregaron {SeedDataConstants.Peliculas.Count} pel�culas.");
        }

        /// <summary>
        /// Sembrar funciones de cine
        /// </summary>
        private static void SeedFunciones(CineDomiruthContext context, int diasFunciones)
        {
            Console.WriteLine($"Sembrando funciones para {diasFunciones} d�as...");
            var funciones = SeedDataConstants.GenerarFunciones(diasFunciones);
            context.Funciones.AddRange(funciones);
            context.SaveChanges();
            Console.WriteLine($"Se agregaron {funciones.Count} funciones.");
        }

        /// <summary>
        /// M�todo para limpiar todos los datos (�til para testing)
        /// </summary>
        public static void ClearAllData(this CineDomiruthContext context)
        {
            Console.WriteLine("Limpiando todos los datos...");
            
            using var transaction = context.Database.BeginTransaction();
            
            try
            {
                context.Reservas.RemoveRange(context.Reservas);
                context.Funciones.RemoveRange(context.Funciones);
                context.Peliculas.RemoveRange(context.Peliculas);
                context.Clientes.RemoveRange(context.Clientes);
                context.Salas.RemoveRange(context.Salas);
                context.Generos.RemoveRange(context.Generos);
                
                context.SaveChanges();
                transaction.Commit();
                
                Console.WriteLine("Todos los datos han sido eliminados.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error al limpiar datos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// M�todo para recrear todos los datos (limpiar y volver a sembrar)
        /// </summary>
        public static void RecreateData(this CineDomiruthContext context, int diasFunciones = 7)
        {
            Console.WriteLine("Recreando datos...");
            context.ClearAllData();
            context.SeedData(diasFunciones);
            Console.WriteLine("Datos recreados exitosamente.");
        }
    }
}