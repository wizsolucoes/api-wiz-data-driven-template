using AutoBogus;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Wiz.Template.API.Controllers;
using Wiz.Template.API.Models.Services;
using Wiz.Template.API.Services.ViaCEP;
using Xunit;

namespace Wiz.Template.Unit.Tests.Controllers
{
    public class ViaCepControllerTest
    {
        private readonly Mock<IViaCepService> _viacepServiceMock;
        private readonly Faker _faker;

        public ViaCepControllerTest()
        {
            _viacepServiceMock = new Mock<IViaCepService>();
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task GetAll_ReturnListCustomerDapperTest()
        {
            var cep = _faker.Random.Int(1000000);
            
            _viacepServiceMock.Setup(x => x.GetByCEPAsync(cep))
                .ReturnsAsync(AutoFaker.Generate<ViaCep>());

            var addressController = new ViaCepController();
            var addressData = await addressController.GetByCep(cep, _viacepServiceMock.Object);

            var actionValue = Assert.IsType<OkObjectResult>(addressData.Result);

            Assert.IsAssignableFrom<ViaCep>(actionValue.Value);
        }
    }
}
