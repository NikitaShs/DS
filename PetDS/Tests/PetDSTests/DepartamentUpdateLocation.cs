using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Contract;
using PetDS.Contract.Departamen;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using PetDS.Infrastructure.DataBaseConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDSTests
{
    public class DepartamentUpdateLocation : IAsyncLifetime, IClassFixture<FactoryTest>
    {
        public IServiceProvider Service { get; set; }

        private readonly Func<Task> _resetDataBase;
        
        public DepartamentUpdateLocation(FactoryTest factoryTest)
        {
            Service = factoryTest.Services;
            _resetDataBase = factoryTest.ResetDataBase;
        }

        [Fact]
        public async Task DepartamentUpdateLocation_Valid_data_win()
        {
            var cancellationToken = CancellationToken.None;

            await using var sripeDbCont = Service.CreateAsyncScope();

            var dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var locReplacement = Location.Create(LocationName.Create("локация").Value, "Novgorod", "Evrope", "yliza", "2").Value;

            await dbContext.Locations.AddAsync(locReplacement, cancellationToken);

            var locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2").Value;

            await dbContext.Locations.AddAsync(locFirct, cancellationToken);

            var dept = Departament.Create(DepartamentName.Create("name").Value, DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept, cancellationToken);

            dbContext.SaveChanges();

            var result = await ExecuteHandler((handler) =>
            {
                var command = new UpdateDepartamentLocationsCommand(new UpdateDepartamentLocationsDto([locReplacement.Id.ValueId]), dept.Id.ValueId);

                return handler.Handler(command, cancellationToken);
            });

            var res2 = dbContext.DepartamentLocations.Any(q => q.DepartamentId == DepartamentId.Create(result.Value) && q.LocationId == locReplacement.Id);


            Assert.True(res2);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DepartamentUpdateLocation_Location_Fictional_Fail()
        {
            var cancellationToken = CancellationToken.None;

            await using var sripeDbCont = Service.CreateAsyncScope();

            var dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2").Value;

            await dbContext.Locations.AddAsync(locFirct, cancellationToken);

            var dept = Departament.Create(DepartamentName.Create("name").Value, DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept, cancellationToken);

            dbContext.SaveChanges();

            var result = await ExecuteHandler((handler) =>
            {
                var command = new UpdateDepartamentLocationsCommand(new UpdateDepartamentLocationsDto([Guid.NewGuid()]), dept.Id.ValueId);

                return handler.Handler(command, cancellationToken);
            });

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task DepartamentUpdateLocation_Not_Location_Fail()
        {
            var cancellationToken = CancellationToken.None;

            await using var sripeDbCont = Service.CreateAsyncScope();

            var dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2").Value;

            await dbContext.Locations.AddAsync(locFirct, cancellationToken);

            var dept = Departament.Create(DepartamentName.Create("name").Value, DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept, cancellationToken);

            dbContext.SaveChanges();

            var result = await ExecuteHandler((handler) =>
            {
                var command = new UpdateDepartamentLocationsCommand(new UpdateDepartamentLocationsDto([]), dept.Id.ValueId);

                return handler.Handler(command, cancellationToken);
            });

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task DepartamentUpdateLocation_Departament_Fictional_Fail()
        {
            var cancellationToken = CancellationToken.None;

            await using var sripeDbCont = Service.CreateAsyncScope();

            var dbContext = sripeDbCont.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var locReplacement = Location.Create(LocationName.Create("локация").Value, "Novgorod", "Evrope", "yliza", "2").Value;

            await dbContext.Locations.AddAsync(locReplacement, cancellationToken);

            dbContext.SaveChanges();

            var result = await ExecuteHandler((handler) =>
            {
                var command = new UpdateDepartamentLocationsCommand(new UpdateDepartamentLocationsDto([locReplacement.Id.ValueId]), Guid.NewGuid());

                return handler.Handler(command, cancellationToken);
            });

            Assert.True(result.IsSuccess);
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync()
        {
            await _resetDataBase();
        }

        private async Task<T> ExecuteHandler<T>(Func<UpdateDepartamentLocationsServise, Task<T>> axtion)
        {
            await using var scope = Service.CreateAsyncScope();

            var handler = scope.ServiceProvider.GetRequiredService<UpdateDepartamentLocationsServise>();

            return await axtion(handler);
        }

    }
}
