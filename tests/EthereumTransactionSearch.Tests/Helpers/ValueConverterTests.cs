using EthereumTransactionSearch.Helpers;
using Xunit;

namespace EthereumTransactionSearch.Tests.Helpers
{
	public class ValueConverterTests
	{
		[Theory]
        [InlineData("0x5BAD55", 6008149)]
        [InlineData("0x9F08A2", 10422434)]
        [InlineData("0x2879", 10361)]
		public void HexToInteger_Converts_Value_Correctly(string hexValue, long expectedResult)
		{
            //Act
            var result = ValueConverter.HexToLong(hexValue);

            //Assert
            Assert.Equal(expectedResult, result);
		}

        [Theory]
        [InlineData("0x174876e800", 100000000000)]
        public void HexToBigInt_Converts_Value_Correctly(string hexValue, long expectedResult)
        {
            //Act
            var result = ValueConverter.HexToBigInt(hexValue);

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
