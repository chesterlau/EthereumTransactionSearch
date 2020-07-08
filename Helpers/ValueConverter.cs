using System;
using System.Numerics;

namespace EthereumTransactionSearch.Helpers
{
	public class ValueConverter
	{
		public static int HexToInteger(string prefixedHex)
		{
			int value = Convert.ToInt32(prefixedHex, 16);
			return value;
		}

		public static BigInteger HexToBigInt(string hexString)
		{
			hexString = hexString.Replace("x", string.Empty);

			BigInteger value = BigInteger.Parse(
				hexString,
				System.Globalization.NumberStyles.AllowHexSpecifier);

			return value;
		}

	}
}
