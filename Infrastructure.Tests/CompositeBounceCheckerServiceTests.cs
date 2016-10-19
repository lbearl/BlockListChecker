using Core.Interfaces;
using Core.Models;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Infrastructure.Tests
{
    public class CompositeBounceCheckerServiceTests
    {
        /// <summary>
        /// Requesting all bounces should return a list of view models.
        /// </summary>
        [Fact]
        public void Get_Bounces_Returns_A_List_Of_View_Models()
        {
            // Arrange
            var returnList = new List<SuppressedEmailViewModel>
            {
                new SuppressedEmailViewModel
                {
                    AddedOn = DateTime.Now,
                    EmailAddress = "test@example.org",
                    EmailServiceProvider = EspEnum.UNKNOWN,
                    ErrorCode = string.Empty,
                    ErrorText = string.Empty
                }
            };

            var mockService1 = new Mock<IThirdPartyBounceService>();
            mockService1.Setup(x => x.GetBounces()).Returns(returnList);
            // Initialize the composite service with an array of one third party service.
            var compositeService = new CompositeBounceCheckerService(new[] { mockService1.Object });

            //Act and Assert
            var result = compositeService.GetBounces();

            Assert.Equal(returnList, result);
            mockService1.Verify(x => x.GetBounces(), Times.Once);
        }

        /// <summary>
        /// Requesting an empty email address should throw an exception.
        /// </summary>
        [Fact]
        public void Get_Single_Bounce_With_Single_Address_Fails()
        {
            // Arrange
            var mockService1 = new Mock<IThirdPartyBounceService>();
            var compositeService = new CompositeBounceCheckerService(new[] { mockService1.Object });
            const string emptyString = "";
            //Act, and Assert
            Assert.Throws<ArgumentException>(() => compositeService.GetBounce(emptyString));
        }

        /// <summary>
        /// Requesting a single address should return a list of view models.
        /// </summary>
        [Fact]
        public void Get_Single_Bounce_Returns_A_List_Of_View_Models()
        {
            // Arrange
            var returnList = new List<SuppressedEmailViewModel>
            {
                new SuppressedEmailViewModel
                {
                    AddedOn = DateTime.Now,
                    EmailAddress = "test@example.org",
                    EmailServiceProvider = EspEnum.UNKNOWN,
                    ErrorCode = string.Empty,
                    ErrorText = string.Empty
                }
            };

            var mockService1 = new Mock<IThirdPartyBounceService>();
            mockService1.Setup(x => x.GetBounce(It.IsAny<string>())).Returns(returnList);
            // Initialize the composite service with an array of one third party service.
            var compositeService = new CompositeBounceCheckerService(new[] { mockService1.Object });

            //Act and Assert
            var result = compositeService.GetBounce("test@example.org");

            Assert.Equal(returnList, result);
            mockService1.Verify(x => x.GetBounce(It.IsAny<string>()), Times.Once);
        }


    }
}
