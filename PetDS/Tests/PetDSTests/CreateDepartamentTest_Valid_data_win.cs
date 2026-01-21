using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Contract;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using PetDS.Infrastructure.DataBaseConnections;
using SharedKernel.Exseption;

namespace PetDSTests;

public class
    CreateDepartamentTest : IAsyncLifetime,
    IClassFixture<FactoryTest> // IClassFixture для того чтобы конструктор родителя 1 раз сработал
{
    private readonly Func<Task> _resetDataBase;

    public CreateDepartamentTest(FactoryTest factoryTest)
    {
        Services = factoryTest.Services;
        _resetDataBase = factoryTest.ResetDataBase;
    }

    private IServiceProvider Services { get; }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _resetDataBase();

    [Fact]
    public async Task CreateDepartamentTest_Valid_data_win()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        await using AsyncServiceScope scopeDbContext = Services.CreateAsyncScope();

        ApplicationDbContext DbContectTest = scopeDbContext.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Result<Location, Error> location =
            Location.Create(LocationName.Create("КПЧК").Value, "Нижний Новгород", "Evropa", "Qee", "2");

        await DbContectTest.Locations.AddAsync(location.Value, cancellationToken);

        DbContectTest.SaveChanges();

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            CreateDepartamentCommand command =
                new(new CreateDepartamentDto("Buxarin", "Trocki", new List<Guid> { location.Value.Id.ValueId }),
                    cancellationToken);

            return handler.Handler(command, cancellationToken);
        });

        Departament dept = await DbContectTest.Departaments.FirstAsync(q => q.Id == DepartamentId.Create(result.Value));

        Assert.NotNull(dept);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateDepartamentTest_Not_Location_Fail()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            CreateDepartamentCommand command = new(new CreateDepartamentDto("Buxarin", "Trocki", new List<Guid>()),
                cancellationToken);

            return handler.Handler(command, cancellationToken);
        });

        await using AsyncServiceScope scopeProv = Services.CreateAsyncScope();

        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task CreateDepartamentTest_Fictional_Fail()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            CreateDepartamentCommand command =
                new(new CreateDepartamentDto("Buxarin", "Trocki", new List<Guid> { Guid.NewGuid() }),
                    cancellationToken);

            return handler.Handler(command, cancellationToken);
        });

        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task CreateDepartamentTest_Many_Loc_Win()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        await using AsyncServiceScope scopeDbContext = Services.CreateAsyncScope();

        ApplicationDbContext DbContectTest = scopeDbContext.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Result<Location, Error> location =
            Location.Create(LocationName.Create("КПЧК").Value, "Нижний Новгород", "Evropa", "Wee", "2");

        Result<Location, Error> location2 =
            Location.Create(LocationName.Create("assdasd").Value, "Нижний Новгород", "Evropa", "Qee", "2");

        await DbContectTest.Locations.AddAsync(location.Value, cancellationToken);

        await DbContectTest.Locations.AddAsync(location2.Value, cancellationToken);

        DbContectTest.SaveChanges();

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            CreateDepartamentCommand command =
                new(
                    new CreateDepartamentDto("Buxarin", "Trocki",
                        new List<Guid> { location.Value.Id.ValueId, location2.Value.Id.ValueId }), cancellationToken);

            return handler.Handler(command, cancellationToken);
        });

        Departament dept = await DbContectTest.Departaments.FirstAsync(q => q.Id == DepartamentId.Create(result.Value));

        Assert.NotNull(dept);

        Assert.True(result.IsSuccess);
    }

    private async Task<T> ExecuteHandler<T>(Func<DepartamentCreateServise, Task<T>> axtion)
    {
        await using AsyncServiceScope scope = Services.CreateAsyncScope();

        DepartamentCreateServise handler = scope.ServiceProvider.GetRequiredService<DepartamentCreateServise>();

        return await axtion(handler);
    }
}