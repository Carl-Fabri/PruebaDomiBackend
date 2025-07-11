using DB.Models;

namespace DB.Configuration
{
    /// <summary>
    /// Contiene los datos iniciales para el seeding de la base de datos
    /// </summary>
    public static class SeedDataConstants
    {
        public static readonly List<Genero> Generos = new()
        {
            new Genero { Id = 1, Nombre = "Acción" },
            new Genero { Id = 2, Nombre = "Comedia" },
            new Genero { Id = 3, Nombre = "Drama" },
            new Genero { Id = 4, Nombre = "Ciencia Ficción" },
            new Genero { Id = 5, Nombre = "Terror" },
            new Genero { Id = 6, Nombre = "Romance" },
            new Genero { Id = 7, Nombre = "Aventura" },
            new Genero { Id = 8, Nombre = "Animación" },
            new Genero { Id = 9, Nombre = "Suspenso" },
            new Genero { Id = 10, Nombre = "Musical" }
        };

        public static readonly List<Sala> Salas = new()
        {
            new Sala { Id = 1, Nombre = "Sala Premium 1", Capacidad = 120 },
            new Sala { Id = 2, Nombre = "Sala Standard 2", Capacidad = 80 },
            new Sala { Id = 3, Nombre = "Sala VIP 3", Capacidad = 50 },
            new Sala { Id = 4, Nombre = "Sala IMAX 4", Capacidad = 150 },
            new Sala { Id = 5, Nombre = "Sala 3D 5", Capacidad = 100 },
            new Sala { Id = 6, Nombre = "Sala Standard 6", Capacidad = 90 }
        };

        public static readonly List<Pelicula> Peliculas = new()
        {
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
                Sinopsis = "Un grupo de amigos despierta después de una fiesta sin recordar nada de la noche anterior.",
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
            },
            new Pelicula
            {
                Id = 7,
                Titulo = "Interstellar",
                GeneroId = 4,
                Sinopsis = "Un grupo de astronautas viaja a través de un agujero de gusano en busca de un nuevo hogar para la humanidad.",
                DuracionMinutos = 169,
                ImagenUrl = "https://m.media-amazon.com/images/M/MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg"
            },
            new Pelicula
            {
                Id = 8,
                Titulo = "The Shawshank Redemption",
                GeneroId = 3,
                Sinopsis = "La historia de amistad entre dos prisioneros a lo largo de varios años en una prisión.",
                DuracionMinutos = 142,
                ImagenUrl = "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg"
            }
        };

        /// <summary>
        /// Genera funciones para múltiples días basadas en las películas y salas disponibles
        /// </summary>
        /// <param name="diasAGenerar">Número de días para generar funciones</param>
        /// <returns>Lista de funciones generadas</returns>
        public static List<Funcion> GenerarFunciones(int diasAGenerar = 7)
        {
            var funciones = new List<Funcion>();
            var baseDate = DateTime.Today.ToUniversalTime();

            // Horarios típicos de funciones
            var horarios = new[] { 10, 12, 14, 16, 18, 20, 22 };

            for (int dia = 0; dia < diasAGenerar; dia++)
            {
                var fechaActual = baseDate.AddDays(dia);

                foreach (var pelicula in Peliculas)
                {
                    // Asignar 2-3 funciones por película por día
                    var horariosParaPelicula = horarios.Take(3).ToArray();
                    
                    foreach (var hora in horariosParaPelicula)
                    {
                        // Rotar salas para variedad
                        var salaId = ((pelicula.Id + hora + dia) % Salas.Count) + 1;
                        var sala = Salas.First(s => s.Id == salaId);

                        funciones.Add(new Funcion
                        {
                            PeliculaId = pelicula.Id,
                            SalaId = salaId,
                            HoraInicio = fechaActual.AddHours(hora),
                            EntradasDisponibles = sala.Capacidad
                        });
                    }
                }
            }

            return funciones;
        }
    }
}