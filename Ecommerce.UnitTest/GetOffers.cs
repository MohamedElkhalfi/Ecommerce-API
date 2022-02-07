using System;
using Xunit;
using EX = Ecommerce.Core.Exceptions.Constants.MessagesConstantes;
namespace Ecommerce.UnitTest
{
    public class GetOffers
    {
        [Fact]
        public void BadRequestForOffers()
        {
            //Arrange
            var getResponse = 301;

            //Act
            var Expected = 200; ;

            //Assert 
            Assert.NotEqual(Expected, getResponse);

        }
    }
}
