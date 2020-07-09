using System;
using System.Numerics;

namespace EthereumTransactionSearch.Helpers
{
	public class ValueConverter
	{
		public static long HexToLong(string prefixedHex)
		{
			long value = Convert.ToInt64(prefixedHex, 16);
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
