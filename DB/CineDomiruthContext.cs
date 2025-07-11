using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class CineDomiruthContext : DbContext
    {
        public CineDomiruthContext(DbContextOptions<CineDomiruthContext> options) : base(options)
        {
        }

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Funcion> Funciones { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones y comportamientos

            // Pelicula
            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasOne(p => p.Genero)
                    .WithMany(g => g.Peliculas)
                    .HasForeignKey(p => p.GeneroId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Funcion
            modelBuilder.Entity<Funcion>(entity =>
            {
                entity.HasOne(f => f.Pelicula)
                    .WithMany(p => p.Funciones)
                    .HasForeignKey(f => f.PeliculaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Sala)
                    .WithMany(s => s.Funciones)
                    .HasForeignKey(f => f.SalaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Reserva
            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasOne(r => r.Funcion)
                    .WithMany(f => f.Reservas)
                    .HasForeignKey(r => r.FuncionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Cliente)
                    .WithMany(c => c.Reservas)
                    .HasForeignKey(r => r.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de índices para mejor performance
            modelBuilder.Entity<Pelicula>()
                .HasIndex(p => p.Titulo);

            modelBuilder.Entity<Funcion>()
                .HasIndex(f => f.HoraInicio);

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.NumeroDocumento)
                .IsUnique();

            // Valor por defecto para FechaReserva
            modelBuilder.Entity<Reserva>()
                .Property(r => r.FechaReserva)
                .HasDefaultValueSql("NOW()");
        }
        public void SeedData()
        {
            try
            {
                if (HasExistingData())
                {
                    return;
                }

                using var transaction = Database.BeginTransaction();

                try
                {
                    SeedGeneros();
                    SeedSalas();
                    SeedPeliculas();
                    SeedFunciones();

                    transaction.Commit();
                    Console.WriteLine("✅ Seeding completado exitosamente.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"❌ Error durante el seeding: {ex.Message}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al verificar o ejecutar seeding: {ex.Message}");
                throw;
            }
        }

        private bool HasExistingData()
        {
            try
            {
                bool hasGeneros = Generos.Any();
                bool hasSalas = Salas.Any();
                bool hasPeliculas = Peliculas.Any();

                return hasGeneros || hasSalas || hasPeliculas;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void SeedGeneros()
        {
            if (Generos.Any())
            {
                return;
            }

            Generos.AddRange(
                new Genero { Id = 1, Nombre = "Acción" },
                new Genero { Id = 2, Nombre = "Comedia" },
                new Genero { Id = 3, Nombre = "Drama" },
                new Genero { Id = 4, Nombre = "Ciencia Ficción" },
                new Genero { Id = 5, Nombre = "Terror" },
                new Genero { Id = 6, Nombre = "Romance" },
                new Genero { Id = 7, Nombre = "Aventura" },
                new Genero { Id = 8, Nombre = "Animación" }
            );
            SaveChanges();
        }

        private void SeedSalas()
        {
            if (Salas.Any())
            {
                return;
            }

            Salas.AddRange(
                new Sala { Id = 1, Nombre = "Sala Premium 1", Capacidad = 120 },
                new Sala { Id = 2, Nombre = "Sala Standard 2", Capacidad = 80 },
                new Sala { Id = 3, Nombre = "Sala VIP 3", Capacidad = 50 },
                new Sala { Id = 4, Nombre = "Sala IMAX 4", Capacidad = 150 },
                new Sala { Id = 5, Nombre = "Sala 3D 5", Capacidad = 100 }
            );
            SaveChanges();
        }

        private void SeedPeliculas()
        {
            if (Peliculas.Any())
            {
                return;
            }

            Peliculas.AddRange(
                new Pelicula
                {
                    Id = 1,
                    Titulo = "Avengers: Endgame",
                    GeneroId = 1,
                    Sinopsis = "Los Vengadores se reúnen para revertir el snap de Thanos y salvar el universo en una batalla épica final.",
                    DuracionMinutos = 181,
                    ImagenUrl = "https://i.blogs.es/c7604e/d1pklzbuyaab0la/450_1000.jpg"
                },
                new Pelicula
                {
                    Id = 2,
                    Titulo = "The Hangover",
                    GeneroId = 2,
                    Sinopsis = "Un grupo de amigos despiertan después de una fiesta sin recordar nada de la noche anterior.",
                    DuracionMinutos = 100,
                    ImagenUrl = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p9573144_p_v8_ac.jpg"
                },
                new Pelicula
                {
                    Id = 3,
                    Titulo = "Inception",
                    GeneroId = 4,
                    Sinopsis = "Un ladrón especializado en infiltrarse en los sueños debe realizar una misión imposible.",
                    DuracionMinutos = 148,
                    ImagenUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg"
                },
                new Pelicula
                {
                    Id = 4,
                    Titulo = "The Dark Knight",
                    GeneroId = 1,
                    Sinopsis = "Batman enfrenta al Joker en una batalla psicológica por el alma de Gotham City.",
                    DuracionMinutos = 152,
                    ImagenUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg"
                },
                new Pelicula
                {
                    Id = 5,
                    Titulo = "Toy Story 4",
                    GeneroId = 8,
                    Sinopsis = "Woody y Buzz Lightyear viven nuevas aventuras con sus amigos juguetes.",
                    DuracionMinutos = 100,
                    ImagenUrl = "https://m.media-amazon.com/images/M/MV5BMTYzMDM4NzkxOV5BMl5BanBnXkFtZTgwNzM1Mzg2NzM@._V1_.jpg"
                },
                new Pelicula
                {
                    Id = 6,
                    Titulo = "Titanic",
                    GeneroId = 6,
                    Sinopsis = "Una historia de amor épica ambientada en el fatídico viaje del RMS Titanic.",
                    DuracionMinutos = 194,
                    ImagenUrl = "https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E5My00ZTliLWIzOTUtMTY4ZGI1YjdiNjk3XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_.jpg"
                }
            );
            SaveChanges();
        }

        private void SeedFunciones()
        {
            if (Funciones.Any())
            {
                return;
            }

            var hoy = DateTime.Today.ToUniversalTime();
            var mañana = hoy.AddDays(1);

            var funciones = new List<Funcion>
                {
                    // Funciones de hoy
                    new Funcion { PeliculaId = 1, SalaId = 1, HoraInicio = hoy.AddHours(10), EntradasDisponibles = 120 },
                    new Funcion { PeliculaId = 1, SalaId = 1, HoraInicio = hoy.AddHours(15), EntradasDisponibles = 120 },
                    new Funcion { PeliculaId = 1, SalaId = 1, HoraInicio = hoy.AddHours(20), EntradasDisponibles = 120 },

                    new Funcion { PeliculaId = 2, SalaId = 2, HoraInicio = hoy.AddHours(12), EntradasDisponibles = 80 },
                    new Funcion { PeliculaId = 2, SalaId = 2, HoraInicio = hoy.AddHours(17), EntradasDisponibles = 80 },

                    new Funcion { PeliculaId = 3, SalaId = 4, HoraInicio = hoy.AddHours(14), EntradasDisponibles = 150 },
                    new Funcion { PeliculaId = 3, SalaId = 4, HoraInicio = hoy.AddHours(19), EntradasDisponibles = 150 },

                    new Funcion { PeliculaId = 4, SalaId = 3, HoraInicio = hoy.AddHours(18), EntradasDisponibles = 50 },
                    new Funcion { PeliculaId = 5, SalaId = 5, HoraInicio = hoy.AddHours(11), EntradasDisponibles = 100 },
                    new Funcion { PeliculaId = 6, SalaId = 2, HoraInicio = hoy.AddHours(21), EntradasDisponibles = 80 },

                    // Funciones de mañana
                    new Funcion { PeliculaId = 1, SalaId = 1, HoraInicio = mañana.AddHours(10), EntradasDisponibles = 120 },
                    new Funcion { PeliculaId = 2, SalaId = 2, HoraInicio = mañana.AddHours(12), EntradasDisponibles = 80 },
                    new Funcion { PeliculaId = 3, SalaId = 4, HoraInicio = mañana.AddHours(14), EntradasDisponibles = 150 },
                    new Funcion { PeliculaId = 4, SalaId = 3, HoraInicio = mañana.AddHours(18), EntradasDisponibles = 50 },
                    new Funcion { PeliculaId = 5, SalaId = 5, HoraInicio = mañana.AddHours(16), EntradasDisponibles = 100 },
                    new Funcion { PeliculaId = 6, SalaId = 2, HoraInicio = mañana.AddHours(20), EntradasDisponibles = 80 }
                };

            Funciones.AddRange(funciones);
            SaveChanges();
        }

        public void ClearAllData()
        {

            using var transaction = Database.BeginTransaction();

            try
            {
                Reservas.RemoveRange(Reservas);
                Funciones.RemoveRange(Funciones);
                Peliculas.RemoveRange(Peliculas);
                Clientes.RemoveRange(Clientes);
                Salas.RemoveRange(Salas);
                Generos.RemoveRange(Generos);

                SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
