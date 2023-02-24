using Common.UOW;
using System;
using Xunit;

namespace Numerology.Test
{
    public class CommonUnitTest
    {
        [Fact]
        public void CheckIfUnitOfWorkCanConnectToDB()
        {
            // Act 
            var uow = new UnitOfWork();

            // Assert
            Assert.True(uow.Session != null);
        }
    }
}
