using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;
using PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;
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
using System.Threading;
using System.Threading.Tasks;

namespace PetDSTests
{
    public class DepartamentUpdateHierarchy : IAsyncLifetime, IClassFixture<FactoryTest>
    {
        public IServiceProvider Services { get; set; }

        private readonly Func<Task> _resetDataBase;

        public DepartamentUpdateHierarchy(FactoryTest factoryTest)
        {
            Services = factoryTest.Services;
            _resetDataBase = factoryTest.ResetDataBase;
        }

        [Fact]
        public async Task DepartamentUpdateHierarchy_Valid_data_newParant_win()
        {
            var cancellationToken = CancellationToken.None;

            await using var scopeDbContext = Services.CreateAsyncScope();

            var dbContext = scopeDbContext.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2").Value;

            await dbContext.Locations.AddAsync(locFirct, cancellationToken);

            var dept = Departament.Create(DepartamentName.Create("name").Value, DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept, cancellationToken);

            var dept2 = Departament.Create(DepartamentName.Create("nameq").Value, DepartamentIdentifier.Create("qqqqq").Value, dept, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept2, cancellationToken);

            var dept3 = Departament.Create(DepartamentName.Create("namew").Value, DepartamentIdentifier.Create("qqqqw").Value, dept, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept3, cancellationToken);

            var dept4 = Departament.Create(DepartamentName.Create("namee").Value, DepartamentIdentifier.Create("qqqqe").Value, dept2, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept4, cancellationToken);

            var dept5 = Departament.Create(DepartamentName.Create("namer").Value, DepartamentIdentifier.Create("qqqqr").Value, null, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept5, cancellationToken);

            await dbContext.SaveChangesAsync();


            var result = await ExecuteHandler((handler) =>
            {
                var command = new UpdateDepartamentHierarchyCommand(dept.Id.ValueId, new UpdateDepartamentHierarchyDto(dept5.Id.ValueId));

                return handler.Handler(command, cancellationToken);
            });

            var res2 = dbContext.Departaments.Any(q => q.Id == dept.Id && q.Parent == dept5);

            Assert.True(res2);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DepartamentUpdateHierarchy_Valid_data_nullParant_win()
        {
            var cancellationToken = CancellationToken.None;

            await using var scopeDbContext = Services.CreateAsyncScope();

            var dbContext = scopeDbContext.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2").Value;

            await dbContext.Locations.AddAsync(locFirct, cancellationToken);

            var dept = Departament.Create(DepartamentName.Create("name").Value, DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept, cancellationToken);

            var dept2 = Departament.Create(DepartamentName.Create("nameq").Value, DepartamentIdentifier.Create("qqqqq").Value, dept, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept2, cancellationToken);

            var dept3 = Departament.Create(DepartamentName.Create("namew").Value, DepartamentIdentifier.Create("qqqqw").Value, dept, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept3, cancellationToken);

            var dept4 = Departament.Create(DepartamentName.Create("namee").Value, DepartamentIdentifier.Create("qqqqe").Value, dept2, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept4, cancellationToken);

            await dbContext.SaveChangesAsync();


            var result = await ExecuteHandler((handler) =>
            {
                var command = new UpdateDepartamentHierarchyCommand(dept4.Id.ValueId, new UpdateDepartamentHierarchyDto(null));

                return handler.Handler(command, cancellationToken);
            });

            var res2 = dbContext.Departaments.FirstAsync(q => q.Id == dept4.Id).Result;

            Assert.NotNull(res2.Parent);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DepartamentUpdateHierarchy_Valid_data_realIdParant_fail()
        {
            var cancellationToken = CancellationToken.None;

            await using var scopeDbContext = Services.CreateAsyncScope();

            var dbContext = scopeDbContext.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var locFirct = Location.Create(LocationName.Create("НеЛокация").Value, "Nogorod", "Evope", "ylia", "2").Value;

            await dbContext.Locations.AddAsync(locFirct, cancellationToken);

            var dept = Departament.Create(DepartamentName.Create("name").Value, DepartamentIdentifier.Create("qqqq").Value, null, [locFirct.Id]).Value;

            await dbContext.Departaments.AddAsync(dept, cancellationToken);

            await dbContext.SaveChangesAsync();

            var result = await ExecuteHandler((handler) =>
            {
                var command = new UpdateDepartamentHierarchyCommand(dept.Id.ValueId, new UpdateDepartamentHierarchyDto(dept.Id.ValueId));

                return handler.Handler(command, cancellationToken);
            });

            Assert.True(result.IsFailure);
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync()
        {
            await _resetDataBase();
        }


        private async Task<T> ExecuteHandler<T>(Func<UpdateDepartamentHierarchyServise, Task<T>> axtion)
        {
            await using var scope = Services.CreateAsyncScope();

            var handler = scope.ServiceProvider.GetRequiredService<UpdateDepartamentHierarchyServise>();

            return await axtion(handler);
        }
    }
}
