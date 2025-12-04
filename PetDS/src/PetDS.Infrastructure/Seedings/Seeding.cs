using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Position;
using PetDS.Domain.Position.VO;
using PetDS.Domain.Shered;
using PetDS.Infrastructure.DataBaseConnections;

namespace PetDS.Infrastructure.Seeding;

public class Seeding : ISeeding
{
    // Конфигурация
    private const int BatchSize = 100;
    private const int LocationsCount = 50;
    private const int DepartamentsCount = 50;
    private const int PositionsCount = 50;
    private const int MaxRetryAttempts = 3;
    private const int RetryDelayMs = 1000;

    // SQL команды для очистки БД
    private static readonly string ClearDepartamentPositionsSql = "DELETE FROM \"departamentPositions\"";
    private static readonly string ClearDepartamentLocationsSql = "DELETE FROM \"departamentLocations\"";
    private static readonly string ClearPositionsSql = "DELETE FROM \"positions\"";
    private static readonly string ClearDepartamentsSql = "DELETE FROM \"departaments\"";
    private static readonly string ClearLocationsSql = "DELETE FROM \"locations\"";
    private static readonly string ResetParentIdsSql = "UPDATE \"departaments\" SET \"parent_id\" = NULL";

    // Данные для генерации
    private readonly string[] _cities = { "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань" };
    private readonly ApplicationDbContext _dbContext;
    private readonly string[] _departamentTypes = { "IT", "HR", "Finance", "Sales", "Marketing" };
    private readonly ILogger<Seeding> _logger;
    private readonly string[] _positionLevels = { "Junior", "Middle", "Senior" };
    private readonly string[] _positionTitles = { "Developer", "Manager", "Analyst", "Designer", "Engineer" };
    private readonly string[] _regions = { "Europe/Moscow", "Europe/London", "Europe/Paris" };
    private readonly string[] _streets = { "Ленина", "Пушкина", "Гагарина", "Мира", "Советская" };

    public Seeding(ApplicationDbContext dbContext, ILogger<Seeding> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<SeedingResult> SeedAsync()
    {
        _logger.LogInformation("🚀 Начало сидирования базы данных");
        DateTime startTime = DateTime.UtcNow;
        SeedingResult result = new();

        try
        {
            // Очистка БД перед началом
            await ClearDatabaseAsync();

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            // 1. Создаем Locations
            _logger.LogInformation("📍 Шаг 1/3: Создание {Count} локаций...", LocationsCount);
            List<Location> locations = await GenerateLocationsAsync();
            if (locations.Any())
            {
                await SaveInBatchesAsync(locations, _dbContext.Locations, "локаций");
                result.LocationsCreated = locations.Count;
                _logger.LogInformation("✅ Создано {Count} локаций", locations.Count);
            }
            else
            {
                _logger.LogError("❌ Не удалось создать ни одной локации!");
                throw new InvalidOperationException("Не удалось создать локации");
            }

            // 2. Создаем Departaments
            _logger.LogInformation("🏢 Шаг 2/3: Создание {Count} департаментов...", DepartamentsCount);
            List<Departament> departaments = await GenerateDepartamentsAsync(locations);

            if (departaments.Any())
            {
                await SaveInBatchesAsync(departaments, _dbContext.Departaments, "департаментов");
                result.DepartamentsCreated = departaments.Count;
                _logger.LogInformation("✅ Создано {Count} департаментов", departaments.Count);
            }
            else
            {
                _logger.LogError("❌ Не удалось создать ни одного департамента!");
                result.DepartamentsCreated = 0;
            }

            // 3. Создаем Positions
            if (departaments.Any())
            {
                _logger.LogInformation("👥 Шаг 3/3: Создание {Count} позиций...", PositionsCount);
                List<Position> positions = await GeneratePositionsAsync(departaments);
                if (positions.Any())
                {
                    await SaveInBatchesAsync(positions, _dbContext.Positions, "позиций");
                    result.PositionsCreated = positions.Count;
                    _logger.LogInformation("✅ Создано {Count} позиций", positions.Count);
                }
                else
                {
                    _logger.LogWarning("⚠️ Не удалось создать ни одной позиции");
                    result.PositionsCreated = 0;
                }
            }
            else
            {
                _logger.LogWarning("📭 Пропуск создания позиций: нет департаментов");
                result.PositionsCreated = 0;
            }

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;

            result.Success = true;
            result.Duration = DateTime.UtcNow - startTime;

            _logger.LogInformation("🎉 Сидирование завершено за {Duration:F2} секунд", result.Duration.TotalSeconds);
            _logger.LogInformation(
                "📊 Итоговые результаты: {Locations} локаций, {Departaments} департаментов, {Positions} позиций",
                result.LocationsCreated, result.DepartamentsCreated, result.PositionsCreated);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
            _logger.LogError(ex, "❌ Критическая ошибка при сидировании базы данных");
            throw;
        }

        return result;
    }

    private async Task<List<Location>> GenerateLocationsAsync()
    {
        _logger.LogDebug("🔧 Генерация {Count} локаций...", LocationsCount);
        List<Location> locations = new();
        Random random = new(42);

        for (int i = 0; i < LocationsCount; i++)
        {
            try
            {
                string city = _cities[random.Next(_cities.Length)];
                string region = _regions[random.Next(_regions.Length)];
                string street = _streets[random.Next(_streets.Length)];
                string houseNumber = random.Next(1, 200).ToString();

                Result<LocationName, Error> locationName = LocationName.Create($"Офис {city} {i + 1}");
                if (locationName.IsFailure)
                {
                    _logger.LogDebug("⚠️ Не удалось создать имя локации: {Error}", locationName.Error);
                    continue;
                }

                Result<Location, Error> location = Location.Create(
                    locationName.Value,
                    city,
                    region,
                    street,
                    houseNumber
                );

                if (location.IsSuccess)
                {
                    locations.Add(location.Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Ошибка при создании локации {Index}", i);
            }
        }

        return locations;
    }

    private async Task<List<Departament>> GenerateDepartamentsAsync(List<Location> locations)
    {
        _logger.LogInformation("🔧 Генерация {Count} департаментов...", DepartamentsCount);

        if (!locations.Any())
        {
            _logger.LogError("❌ Список локаций пуст");
            return new List<Departament>();
        }

        List<Departament> departaments = new();
        Random random = new(42);
        HashSet<string> usedIdentifiers = new();

        // Создаем корневые департаменты (без родителей)
        int rootCount = Math.Min(DepartamentsCount / 4, 10);
        _logger.LogInformation("🔄 Создание {Count} корневых департаментов...", rootCount);

        for (int i = 0; i < rootCount; i++)
        {
            try
            {
                string departamentType = _departamentTypes[random.Next(_departamentTypes.Length)];

                Result<DepartamentName, Error> name = DepartamentName.Create($"Kor{departamentType}{i + 1}");
                if (name.IsFailure)
                {
                    _logger.LogWarning("❌ Ошибка создания имени департамента {Index}: {Error}", i, name.Error);
                    continue;
                }

                // Генерируем идентификатор ТОЛЬКО из букв
                Result<DepartamentIdentifier, Error> identifier =
                    GenerateLetterOnlyIdentifier(departamentType, i, usedIdentifiers);
                if (identifier.IsFailure)
                {
                    _logger.LogWarning("❌ Ошибка создания идентификатора департамента {Index}: {Error}", i,
                        identifier.Error);
                    continue;
                }

                // Выбираем 1-3 локации для департамента
                int locationCount = Math.Min(random.Next(1, 4), locations.Count);
                List<LocationId> selectedLocationIds = locations
                    .Shuffle(random)
                    .Take(locationCount)
                    .Select(l => l.Id)
                    .ToList();

                Result<Departament, Error> departament =
                    Departament.Create(name.Value, identifier.Value, null, selectedLocationIds);

                if (departament.IsSuccess)
                {
                    departaments.Add(departament.Value);
                    _logger.LogDebug("✅ Успешно создан корневой департамент: {Name}", name.Value.ValueName);
                }
                else
                {
                    _logger.LogWarning("❌ Ошибка создания корневого департамента {Index}: {Error}", i,
                        departament.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Неожиданная ошибка при создании корневого департамента {Index}", i);
            }
        }

        // Создаем дочерние департаменты только если есть родители
        if (departaments.Any())
        {
            _logger.LogInformation("🔄 Создание дочерних департаментов...");

            for (int i = rootCount; i < DepartamentsCount; i++)
            {
                try
                {
                    string departamentType = _departamentTypes[random.Next(_departamentTypes.Length)];

                    Result<DepartamentName, Error> name = DepartamentName.Create($"{departamentType}_{i + 1}");
                    if (name.IsFailure)
                    {
                        continue;
                    }

                    // Генерируем идентификатор ТОЛЬКО из букв
                    Result<DepartamentIdentifier, Error> identifier =
                        GenerateLetterOnlyIdentifier(departamentType, i, usedIdentifiers);
                    if (identifier.IsFailure)
                    {
                        continue;
                    }

                    // Выбираем случайного родителя из существующих департаментов
                    Departament parent = departaments[random.Next(departaments.Count)];

                    // Выбираем 1-3 локации для департамента
                    int locationCount = Math.Min(random.Next(1, 4), locations.Count);
                    List<LocationId> selectedLocationIds = locations
                        .Shuffle(random)
                        .Take(locationCount)
                        .Select(l => l.Id)
                        .ToList();

                    Result<Departament, Error> departament =
                        Departament.Create(name.Value, identifier.Value, parent, selectedLocationIds);
                    if (departament.IsSuccess)
                    {
                        departaments.Add(departament.Value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Ошибка при создании дочернего департамента {Index}", i);
                }
            }
        }

        _logger.LogInformation("✅ Итог генерации департаментов: создано {Count} департаментов", departaments.Count);
        return departaments;
    }

    private Result<DepartamentIdentifier, Error> GenerateLetterOnlyIdentifier(string prefix, int index,
        HashSet<string> usedIdentifiers)
    {
        try
        {
            // Очищаем префикс от не-буквенных символов
            string cleanPrefix = new string(prefix.Where(char.IsLetter).ToArray()).ToUpper();
            if (string.IsNullOrEmpty(cleanPrefix))
            {
                cleanPrefix = "DEPT";
            }

            // Убеждаемся, что префикс не слишком длинный
            cleanPrefix = cleanPrefix.Truncate(10);

            string identifier;
            int attempt = 0;

            do
            {
                // Генерируем идентификатор ТОЛЬКО из букв
                // Используем индекс + случайные буквы для уникальности
                string randomSuffix = GenerateRandomLetters(5);
                identifier = $"{cleanPrefix}{randomSuffix}";

                attempt++;
            } while (usedIdentifiers.Contains(identifier) && attempt < 50);

            if (attempt >= 50)
            {
                // Используем GUID без цифр как запасной вариант
                string guidSuffix = new(Guid.NewGuid().ToString("N").Where(char.IsLetter).ToArray());
                identifier = $"{cleanPrefix}{guidSuffix}".Truncate(150);
            }

            usedIdentifiers.Add(identifier);
            Result<DepartamentIdentifier, Error> result = DepartamentIdentifier.Create(identifier);

            if (result.IsFailure)
            {
                _logger.LogWarning("⚠️ Создан невалидный идентификатор: {Identifier} - {Error}", identifier,
                    result.Error);

                // Fallback: простой буквенный идентификатор
                string fallbackIdentifier = $"DEPT{GenerateRandomLetters(10)}";
                result = DepartamentIdentifier.Create(fallbackIdentifier);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Ошибка при генерации идентификатора для префикса {Prefix}", prefix);

            // Ultimate fallback
            string fallbackIdentifier = $"DEPT{GenerateRandomLetters(10)}";
            return DepartamentIdentifier.Create(fallbackIdentifier);
        }
    }

    private string GenerateRandomLetters(int length)
    {
        Random random = new();
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(letters, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private async Task<List<Position>> GeneratePositionsAsync(List<Departament> departaments)
    {
        _logger.LogDebug("🔧 Генерация {Count} позиций...", PositionsCount);

        if (!departaments.Any())
        {
            _logger.LogWarning("📭 Список департаментов пуст");
            return new List<Position>();
        }

        List<Position> positions = new();
        Random random = new(42);

        for (int i = 0; i < PositionsCount; i++)
        {
            try
            {
                string level = _positionLevels[random.Next(_positionLevels.Length)];
                string title = _positionTitles[random.Next(_positionTitles.Length)];

                Result<PositionName, Error> positionName = PositionName.Create($"{level} {title} {i + 1}");
                if (positionName.IsFailure)
                {
                    continue;
                }

                Result<PositionDiscription, Error> positionDescription =
                    PositionDiscription.Create($"Описание позиции {level} {title}");
                if (positionDescription.IsFailure)
                {
                    continue;
                }

                // Выбираем 1-3 департамента для позиции
                int departamentCount = Math.Min(random.Next(1, 4), departaments.Count);
                List<Departament> selectedDepartaments = departaments
                    .Shuffle(random)
                    .Take(departamentCount)
                    .ToList();

                Result<Position, Error> position = Position.Create(
                    positionName.Value,
                    positionDescription.Value,
                    selectedDepartaments
                );

                if (position.IsSuccess)
                {
                    positions.Add(position.Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Ошибка при создании позиции {Index}", i);
            }
        }

        return positions;
    }

    private async Task SaveInBatchesAsync<T>(List<T> entities, DbSet<T> dbSet, string entityName) where T : class
    {
        if (!entities.Any())
        {
            _logger.LogWarning("📭 Нет данных для сохранения {EntityName}", entityName);
            return;
        }

        _logger.LogDebug("💾 Сохранение {Count} {EntityName} батчами по {BatchSize}...",
            entities.Count, entityName, BatchSize);

        for (int i = 0; i < entities.Count; i += BatchSize)
        {
            List<T> batch = entities.Skip(i).Take(BatchSize).ToList();

            for (int attempt = 1; attempt <= MaxRetryAttempts; attempt++)
            {
                try
                {
                    await dbSet.AddRangeAsync(batch);
                    await _dbContext.SaveChangesAsync();

                    int current = Math.Min(i + BatchSize, entities.Count);
                    double percentage = (double)current / entities.Count * 100;
                    _logger.LogDebug("📊 [{EntityName}] Прогресс: {Current}/{Total} ({Percentage:F1}%)",
                        entityName, current, entities.Count, percentage);

                    break;
                }
                catch (Exception ex) when (attempt < MaxRetryAttempts)
                {
                    _logger.LogWarning(ex, "🔄 Попытка {Attempt} сохранения батча {EntityName} не удалась", attempt,
                        entityName);
                    await Task.Delay(RetryDelayMs);
                    _dbContext.ChangeTracker.Clear();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Не удалось сохранить батч {EntityName}", entityName);
                    throw;
                }
            }
        }

        _logger.LogDebug("✅ Успешно сохранено {Count} {EntityName}", entities.Count, entityName);
    }

    private async Task ClearDatabaseAsync()
    {
        _logger.LogInformation("🧹 Начало очистки базы данных...");

        try
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            // Используем безопасные SQL команды без интерполяции
            _logger.LogDebug("🗑️ Очистка таблицы departamentPositions...");
            await _dbContext.Database.ExecuteSqlRawAsync(ClearDepartamentPositionsSql);

            _logger.LogDebug("🗑️ Очистка таблицы departamentLocations...");
            await _dbContext.Database.ExecuteSqlRawAsync(ClearDepartamentLocationsSql);

            _logger.LogDebug("🗑️ Очистка таблицы positions...");
            await _dbContext.Database.ExecuteSqlRawAsync(ClearPositionsSql);

            _logger.LogDebug("🗑️ Сброс parent_id в таблице departaments...");
            await _dbContext.Database.ExecuteSqlRawAsync(ResetParentIdsSql);

            _logger.LogDebug("🗑️ Очистка таблицы departaments...");
            await _dbContext.Database.ExecuteSqlRawAsync(ClearDepartamentsSql);

            _logger.LogDebug("🗑️ Очистка таблицы locations...");
            await _dbContext.Database.ExecuteSqlRawAsync(ClearLocationsSql);

            _logger.LogInformation("✅ База данных успешно очищена");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Ошибка при очистке базы данных");
            throw;
        }
        finally
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }
}

// Extension methods для удобства
public static class SeedingExtensions
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    public static List<T> Shuffle<T>(this List<T> list, Random rng)
    {
        List<T> shuffled = new(list);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }

        return shuffled;
    }
}

// Результат сидирования
public class SeedingResult
{
    public bool Success { get; set; }
    public TimeSpan Duration { get; set; }
    public int LocationsCreated { get; set; }
    public int DepartamentsCreated { get; set; }
    public int PositionsCreated { get; set; }
    public string Error { get; set; }

    public override string ToString() =>
        Success
            ? $"✅ Успешно за {Duration.TotalSeconds:F2}с. 📍{LocationsCreated} 🏢{DepartamentsCreated} 👥{PositionsCreated}"
            : $"❌ Ошибка: {Error}";
}