using FluentAssertions;
using MottuBusiness;
using MottuModel;
using Moq;
using MottuData;
using Microsoft.EntityFrameworkCore;

namespace MottuTestes;

public class MottuServiceTests
{
    private IMottuService BuildService()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        var ctx = new ApplicationDbContext(options);

        return new MottuService(ctx);
    }

    [Fact]
    public void Deve_Criar_Moto()
    {
        var svc = BuildService();

        var moto = new Moto { Modelo = "CG160", Placa = "AAA0001", ZonaId = 1 };

        var result = svc.Criar(moto);

        result.Id.Should().NotBeNull();
    }
}
