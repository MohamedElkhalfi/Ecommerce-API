using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.UnitTest.DAL.Repository.Products
{
    public class ProductRepositoryUnitTest
    {
        [Fact]
        public void Should_Return_Data_And_Should_Not_Be_Null()
        {
            //Arrange
            var _ProductRepositoryMock = new Mock<IProductRepository>();
            var _ProductService = new ProductService(_ProductRepositoryMock.Object);

            //Act
           var GetAllProducts =   _ProductService.ViewAllProductsServiceAsync().Result.ToList();


            //Assert 
            GetAllProducts.Should().NotBeNull();
            Assert.True(GetAllProducts.Count() >= 1);

        }
    }
}
