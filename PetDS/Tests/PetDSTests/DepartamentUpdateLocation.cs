using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;
using PetDS.Contract.Departamen;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using PetDS.Infrastructure.DataBaseConnections;
using SharedKernel.Exseption;

namespace PetDSTests;

public class DepartamentUpdateLocation : IAsyncLifetime, IClassFixture<FactoryTest>
{
    private readonly Func<Task> _resetDataBase;

    public DepartamentUpdateLocation(FactoryTest factoryTest)
    {
        Service = factoryTest.Services;
        _resetDataBase = factoryTest.ResetDataBase;
    }

    public IServiceProvider Service { get; set; }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _resetDataBase();

    [Fact]
    public async Task DepartamentUpdateLocation_Valid_data_win()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        await using AsyncServiceScope sripeDbCont = Service.CreateAsyncScope();

        ApplicationDbContext dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Location? locReplacement =
            Location.Create(LocationName.Create("локация").Value, "Novgorod", "Evrope", "yliza", "2").Value;

        await dbContext.Locations.AddAsync(locReplacement, cancellationToken);

        Location? locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2")
            .Value;

        await dbContext.Locations.AddAsync(locFirct, cancellationToken);

        Departament? dept = Departament.Create(DepartamentName.Create("name").Value,
            DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

        await dbContext.Departaments.AddAsync(dept, cancellationToken);

        dbContext.SaveChanges();

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            UpdateDepartamentLocationsCommand command =
                new(new UpdateDepartamentLocationsDto([locReplacement.Id.ValueId]), dept.Id.ValueId);

            return handler.Handler(command, cancellationToken);
        });

        bool res2 = dbContext.DepartamentLocations.Any(q =>
            q.DepartamentId == DepartamentId.Create(result.Value) && q.LocationId == locReplacement.Id);


        Assert.True(res2);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task DepartamentUpdateLocation_Location_Fictional_Fail()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        await using AsyncServiceScope sripeDbCont = Service.CreateAsyncScope();

        ApplicationDbContext dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Location? locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2")
            .Value;

        await dbContext.Locations.AddAsync(locFirct, cancellationToken);

        Departament? dept = Departament.Create(DepartamentName.Create("name").Value,
            DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

        await dbContext.Departaments.AddAsync(dept, cancellationToken);

        dbContext.SaveChanges();

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            UpdateDepartamentLocationsCommand command = new(new UpdateDepartamentLocationsDto([Guid.NewGuid()]),
                dept.Id.ValueId);

            return handler.Handler(command, cancellationToken);
        });

        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task DepartamentUpdateLocation_Not_Location_Fail()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        await using AsyncServiceScope sripeDbCont = Service.CreateAsyncScope();

        ApplicationDbContext dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Location? locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2")
            .Value;

        await dbContext.Locations.AddAsync(locFirct, cancellationToken);

        Departament? dept = Departament.Create(DepartamentName.Create("name").Value,
            DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

        await dbContext.Departaments.AddAsync(dept, cancellationToken);

        dbContext.SaveChanges();

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            UpdateDepartamentLocationsCommand command = new(new UpdateDepartamentLocationsDto([]), dept.Id.ValueId);

            return handler.Handler(command, cancellationToken);
        });

        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task DepartamentUpdateLocation_Departament_Fictional_Fail()
    {
        CancellationToken cancellationToken = CancellationToken.None;

        await using AsyncServiceScope sripeDbCont = Service.CreateAsyncScope();

        ApplicationDbContext dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Location? locReplacement =
            Location.Create(LocationName.Create("локация").Value, "Novgorod", "Evrope", "yliza", "2").Value;

        await dbContext.Locations.AddAsync(locReplacement, cancellationToken);

        dbContext.SaveChanges();

        Result<Guid, Errors> result = await ExecuteHandler(handler =>
        {
            UpdateDepartamentLocationsCommand command =
                new(new UpdateDepartamentLocationsDto([locReplacement.Id.ValueId]), Guid.NewGuid());

            return handler.Handler(command, cancellationToken);
        });

        Assert.True(result.IsSuccess);
    }

    private async Task<T> ExecuteHandler<T>(Func<UpdateDepartamentLocationsServise, Task<T>> axtion)
    {
        await using AsyncServiceScope scope = Service.CreateAsyncScope();

        UpdateDepartamentLocationsServise handler =
            scope.ServiceProvider.GetRequiredService<UpdateDepartamentLocationsServise>();

        return await axtion(handler);
    }
}