# Database Seeding Documentation

## Organización de los Seeders

Los seeders han sido organizados en archivos independientes dentro de la carpeta `DB/Configuration/`:

### Archivos

1. **`SeedDataConstants.cs`** - Contiene todas las constantes de datos:
   - Lista de géneros predefinidos
   - Lista de salas con diferentes capacidades
   - Lista de películas con información completa
   - Generador dinámico de funciones para múltiples días

2. **`DatabaseSeeder.cs`** - Contiene la lógica de seeding:
   - Método principal `SeedData()` como extensión del contexto
   - Métodos individuales para cada entidad
   - Métodos auxiliares para limpiar y recrear datos
   - Logging detallado de las operaciones

## Uso

### Seeding Automático
El seeding se ejecuta automáticamente al iniciar la aplicación en `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CineDomiruthContext>();
    context.Database.Migrate();
    context.SeedData(); // Usa 7 días por defecto
}
```

### Opciones de Configuración

#### Generar funciones para más días:
```csharp
context.SeedData(14); // Genera funciones para 14 días
```

#### Limpiar todos los datos:
```csharp
context.ClearAllData();
```

#### Recrear todos los datos:
```csharp
context.RecreateData(10); // Limpia y vuelve a sembrar con 10 días de funciones
```

## Datos Incluidos

### Géneros (10 tipos)
- Acción, Comedia, Drama, Ciencia Ficción
- Terror, Romance, Aventura, Animación
- Suspenso, Musical

### Salas (6 salas)
- Sala Premium 1 (120 asientos)
- Sala Standard 2 (80 asientos)  
- Sala VIP 3 (50 asientos)
- Sala IMAX 4 (150 asientos)
- Sala 3D 5 (100 asientos)
- Sala Standard 6 (90 asientos)

### Películas (8 películas)
- Avengers: Endgame (Acción)
- The Hangover (Comedia)
- Inception (Ciencia Ficción)
- The Dark Knight (Acción)
- Toy Story 4 (Animación)
- Titanic (Romance)
- Interstellar (Ciencia Ficción)
- The Shawshank Redemption (Drama)

### Funciones
- Se generan automáticamente para el número de días especificado
- Múltiples horarios por día: 10:00, 12:00, 14:00, 16:00, 18:00, 20:00, 22:00
- Rotación inteligente de salas para variedad
- Capacidad completa disponible al inicio

## Beneficios de esta Organización

1. **Separación de responsabilidades**: Datos y lógica en archivos separados
2. **Mantenibilidad**: Fácil agregar nuevos datos o modificar existentes  
3. **Flexibilidad**: Configuración de días de funciones según necesidades
4. **Logging**: Información detallada de las operaciones de seeding
5. **Utilidades**: Métodos para limpiar y recrear datos durante desarrollo
6. **Escalabilidad**: Estructura preparada para agregar más tipos de datos

## Para Desarrolladores

Para agregar nuevos datos:

1. **Nuevos géneros/salas/películas**: Modificar las listas en `SeedDataConstants.cs`
2. **Nueva lógica de seeding**: Agregar métodos en `DatabaseSeeder.cs`
3. **Configuración específica**: Usar los parámetros opcionales en `SeedData()`

La aplicación automáticamente detectará si ya existen datos y evitará duplicaciones.