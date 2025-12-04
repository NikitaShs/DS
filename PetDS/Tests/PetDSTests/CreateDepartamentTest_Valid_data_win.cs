using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Contract;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using PetDS.Infrastructure.DataBaseConnections;

namespace PetDSTests;

public class CreateDepartamentTest : IAsyncLifetime, IClassFixture<FactoryTest> // IClassFixture для того чтобы конструктор родителя 1 раз сработал
{
    private IServiceProvider Services { get; set; }

    private readonly Func<Task> _resetDataBase;

    public CreateDepartamentTest(FactoryTest factoryTest)
    {
        Services = factoryTest.Services;
        _resetDataBase = factoryTest.ResetDataBase;
    }

    [Fact]
    public async Task CreateDepartamentTest_Valid_data_win()
    {
        var cancellationToken = CancellationToken.None;

        await using var scopeDbContext = Services.CreateAsyncScope();

        var DbContectTest = scopeDbContext.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var location = Location.Create(LocationName.Create("КПЧК").Value, "Нижний Новгород", "Evropa", "Qee", "2");

        await DbContectTest.Locations.AddAsync(location.Value, cancellationToken);

        DbContectTest.SaveChanges();

        await using var scope = Services.CreateAsyncScope();

        var handler = scope.ServiceProvider.GetRequiredService<DepartamentCreateServise>();

        var command = new CreateDepartamentCommand(new CreateDepartamentDto("Buxarin", "Trocki", new List<Guid> { location.Value.Id.ValueId }), cancellationToken);

        var result = await handler.Handler(command, cancellationToken);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateDepartamentTest_Not_Location_Fail()
    {
        var cancellationToken = CancellationToken.None;

        await using var scope = Services.CreateAsyncScope();

        var handler = scope.ServiceProvider.GetRequiredService<DepartamentCreateServise>();

        var command = new CreateDepartamentCommand(new CreateDepartamentDto("Buxarin", "Trocki", new List<Guid> { }), cancellationToken);

        var result = await handler.Handler(command, cancellationToken);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task CreateDepartamentTest_Fictional_Fail()
    {
        var cancellationToken = CancellationToken.None;

        await using var scope = Services.CreateAsyncScope();

        var handler = scope.ServiceProvider.GetRequiredService<DepartamentCreateServise>();

        var command = new CreateDepartamentCommand(new CreateDepartamentDto("Buxarin", "Trocki", new List<Guid> { Guid.NewGuid() }), cancellationToken);

        var result = await handler.Handler(command, cancellationToken);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task CreateDepartamentTest_Many_Loc_Win()
    {
        var cancellationToken = CancellationToken.None;

        await using var scopeDbContext = Services.CreateAsyncScope();

        var DbContectTest = scopeDbContext.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var location = Location.Create(LocationName.Create("КПЧК").Value, "Нижний Новгород", "Evropa", "Wee", "2");

        var location2 = Location.Create(LocationName.Create("assdasd").Value, "Нижний Новгород", "Evropa", "Qee", "2");

        await DbContectTest.Locations.AddAsync(location.Value, cancellationToken);

        await DbContectTest.Locations.AddAsync(location2.Value, cancellationToken);

        DbContectTest.SaveChanges();

        await using var scope = Services.CreateAsyncScope();

        var handler = scope.ServiceProvider.GetRequiredService<DepartamentCreateServise>();

        var command = new CreateDepartamentCommand(new CreateDepartamentDto("Buxarin", "Trocki", new List<Guid> { location.Value.Id.ValueId, location2.Value.Id.ValueId }), cancellationToken);

        var result = await handler.Handler(command, cancellationToken);

        Assert.True(result.IsSuccess);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _resetDataBase();
    }
}