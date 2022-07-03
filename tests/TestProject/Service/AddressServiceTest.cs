using DevInSales.Api.Controllers;
using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using DevInSales.Core.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;

namespace DevInSales.Tests.Service
{
    public class AddressServiceTest
    {
        /*private Mock<IAddressService> _addressServiceMock;
       
        private AddressService _addressService;

        public AddressServiceTest()
        {
            _addressServiceMock = new Mock<IAddressService>();

            _addressService = new AddressService(_addressServiceMock.Object);
        }*/

        //private Mock<IAddressService> _mockAddressService;
        //private Mock<DataContext> _mockDataContext;

        private AddressService _addressService;
        private CityService _cityService;


        public AddressServiceTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             //.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            var context = new DataContext(options);
            _cityService = new CityService(context);
            _addressService = new AddressService(context);
            Seed().Wait();

            //_mockAddressService = new Mock<IAddressService>();
            //_mockDataContext = new Mock<DataContext>();
            

            //_addressService = new AddressService(_mockAddressService.Object);
        }

        private async Task Seed()
        {
            _cityService.Add(new City(1, "Cidade"));
            _addressService.Add(new Address("Rua 1", "11111111111", 1, "Complemento 1", 1));
            _addressService.Add(new Address("Rua 2", "22222222222", 2, "Complemento 2", 1));
            _addressService.Add(new Address("Rua 3", "33333333333", 3, "Complemento 3", 1));
        }

        [Fact]
        public async Task GetById_ShouldReturnAdreess()
        {
            var result = _addressService.GetById(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Theory]
        [InlineData(1, null, null, null)]
        [InlineData(null, 1, null, null)]
        [InlineData(null, null, "Rua 1", null)]
        [InlineData(null, null, null, "11111111111")]
        public async Task GetAll_ReturnAddresses(
            int? stateId,
            int? cityId,
            string? street,
            string? cep
        )
        {
            var result = _addressService.GetAll(stateId, cityId, street, cep);
            Assert.NotEmpty(result);
            //Assert.Contains(1, result.Select(x => x.CityId));
            //Assert.Contains(1, result.Select(x => x.City.StateId));
            Assert.Contains("Rua 1", result.Select(x => x.Street));
            Assert.Contains("11111111111", result.Select(x => x.Cep));
        }
    }
}