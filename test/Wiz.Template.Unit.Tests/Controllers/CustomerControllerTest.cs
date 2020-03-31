using AutoBogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Controllers;
using Wiz.Template.API.Data;
using Wiz.Template.API.Infra.UoW;
using Wiz.Template.API.Models;
using Wiz.Template.API.Models.Dapper;
using Xunit;

namespace Wiz.Template.Unit.Tests.Controllers
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerData> _customerDataMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CustomerControllerTest()
        {
            _customerDataMock = new Mock<ICustomerData>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task GetAll_ReturnListCustomerDapperTest()
        {
            _customerDataMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(AutoFaker.Generate<IEnumerable<CustomerDapper>>());

            var customerController = new CustomerController(_customerDataMock.Object, _unitOfWorkMock.Object);
            var customersData = await customerController.GetAll();

            var actionValue = Assert.IsType<OkObjectResult>(customersData.Result);

            Assert.IsAssignableFrom<IEnumerable<CustomerDapper>>(actionValue.Value);
        }      
        
        [Fact]
        public async Task GetById_ReturnCustomerDapperTest()
        {
            var id = 1;

            _customerDataMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(AutoFaker.Generate<CustomerDapper>());

            var customerController = new CustomerController(_customerDataMock.Object, _unitOfWorkMock.Object);
            var customerData = await customerController.GetById(id);

            var actionValue = Assert.IsType<OkObjectResult>(customerData.Result);

            Assert.IsAssignableFrom<CustomerDapper>(actionValue.Value);
        }       
        
        [Fact]
        public void Post_ReturnCreatedResultTest()
        {
            var customer = AutoFaker.Generate<Customer>();

            _customerDataMock.Setup(x => x.Add(customer));

            var customerController = new CustomerController(_customerDataMock.Object, _unitOfWorkMock.Object);
            var customerData = customerController.Post(customer);

            var actionValue = Assert.IsType<CreatedResult>(customerData.Result);

            Assert.Equal(StatusCodes.Status201Created, actionValue.StatusCode);
        }      
        
        [Fact]
        public void PostWithReturn_ReturnCustomerCreatedTest()
        {
            var customer = AutoFaker.Generate<Customer>();

            _customerDataMock.Setup(x => x.AddWithReturn(customer))
                .Returns(customer);

            var customerController = new CustomerController(_customerDataMock.Object, _unitOfWorkMock.Object);
            var customerData = customerController.Post(customer);

            var actionValue = Assert.IsType<CreatedResult>(customerData.Result);

            Assert.NotNull(actionValue.Value);
        }

        [Fact]
        public async Task Put_ReturnNoContentTest()
        {
            var id = 1;
            var customer = new AutoFaker<Customer>()
                    .RuleFor(fake => fake.Id, 1).Generate();
            
            var customerDapper = new AutoFaker<CustomerDapper>()
                    .RuleFor(fake => fake.Id, 1).Generate();


            _customerDataMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(customerDapper);
            _customerDataMock.Setup(x => x.Update(customer));

            var customerController = new CustomerController(_customerDataMock.Object, _unitOfWorkMock.Object);
            var customerService = await customerController.Put(id, customer);

            var actionResult = Assert.IsType<NoContentResult>(customerService);
            Assert.Equal(StatusCodes.Status204NoContent, actionResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnNoContent()
        {
            var id = 1;
            var customer = new AutoFaker<Customer>()
                    .RuleFor(fake => fake.Id, 1).Generate();

            var customerDapper = new AutoFaker<CustomerDapper>()
                    .RuleFor(fake => fake.Id, 1).Generate();

            _customerDataMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(customerDapper);

            _customerDataMock.Setup(x => x.Remove(customer));

            var customerController = new CustomerController(_customerDataMock.Object, _unitOfWorkMock.Object);
            var customerService = await customerController.Delete(id);

            var actionResult = Assert.IsType<NoContentResult>(customerService);

            Assert.Equal(StatusCodes.Status204NoContent, actionResult.StatusCode);
        }
    }
}
