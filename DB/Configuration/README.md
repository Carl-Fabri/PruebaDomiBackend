# Database Seeding Documentation

## Organizaci�n de los Seeders

Los seeders han sido organizados en archivos independientes dentro de la carpeta `DB/Configuration/`:

### Archivos

1. **`SeedDataConstants.cs`** - Contiene todas las constantes de datos:
   - Lista de g�neros predefinidos
   - Lista de salas con diferentes capacidades
   - Lista de pel�culas con informaci�n completa
   - Generador din�mico de funciones para m�ltiples d�as

2. **`DatabaseSeeder.cs`** - Contiene la l�gica de seeding:
   - M�todo principal `SeedData()` como extensi�n del contexto
   - M�todos individuales para cada entidad
   - M�todos auxiliares para limpiar y recrear datos
   - Logging detallado de las operaciones

## Uso

### Seeding Autom�tico
El seeding se ejecuta autom�ticamente al iniciar la aplicaci�n en `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CineDomiruthContext>();
    context.Database.Migrate();
    context.SeedData(); // Usa 7 d�as por defecto
}
```

### Opciones de Configuraci�n

#### Generar funciones para m�s d�as:
```csharp
context.SeedData(14); // Genera funciones para 14 d�as
```

#### Limpiar todos los datos:
```csharp
context.ClearAllData();
```

#### Recrear todos los datos:
```csharp
context.RecreateData(10); // Limpia y vuelve a sembrar con 10 d�as de funciones
```

## Datos Incluidos

### G�neros (10 tipos)
- Acci�n, Comedia, Drama, Ciencia Ficci�n
- Terror, Romance, Aventura, Animaci�n
- Suspenso, Musical

### Salas (6 salas)
- Sala Premium 1 (120 asientos)
- Sala Standard 2 (80 asientos)  
- Sala VIP 3 (50 asientos)
- Sala IMAX 4 (150 asientos)
- Sala 3D 5 (100 asientos)
- Sala Standard 6 (90 asientos)

### Pel�culas (8 pel�culas)
- Avengers: Endgame (Acci�n)
- The Hangover (Comedia)
- Inception (Ciencia Ficci�n)
- The Dark Knight (Acci�n)
- Toy Story 4 (Animaci�n)
- Titanic (Romance)
- Interstellar (Ciencia Ficci�n)
- The Shawshank Redemption (Drama)

### Funciones
- Se generan autom�ticamente para el n�mero de d�as especificado
- M�ltiples horarios por d�a: 10:00, 12:00, 14:00, 16:00, 18:00, 20:00, 22:00
- Rotaci�n inteligente de salas para variedad
- Capacidad completa disponible al inicio

## Beneficios de esta Organizaci�n

1. **Separaci�n de responsabilidades**: Datos y l�gica en archivos separados
2. **Mantenibilidad**: F�cil agregar nuevos datos o modificar existentes  
3. **Flexibilidad**: Configuraci�n de d�as de funciones seg�n necesidades
4. **Logging**: Informaci�n detallada de las operaciones de seeding
5. **Utilidades**: M�todos para limpiar y recrear datos durante desarrollo
6. **Escalabilidad**: Estructura preparada para agregar m�s tipos de datos

## Para Desarrolladores

Para agregar nuevos datos:

1. **Nuevos g�neros/salas/pel�culas**: Modificar las listas en `SeedDataConstants.cs`
2. **Nueva l�gica de seeding**: Agregar m�todos en `DatabaseSeeder.cs`
3. **Configuraci�n espec�fica**: Usar los par�metros opcionales en `SeedData()`

La aplicaci�n autom�ticamente detectar� si ya existen datos y evitar� duplicaciones.