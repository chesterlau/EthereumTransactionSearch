using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Helpers
{
	public class ValueConverter
	{
		public static int HexToInteger(string prefixedHex)
		{
			int value = Convert.ToInt32(prefixedHex, 16);
			return value;
		}

		public static double WeiToEther(long weiValue)
		{
			return 0;
		}

	}
}
