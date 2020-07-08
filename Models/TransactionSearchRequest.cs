using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Models
{
	public class TransactionSearchRequest
	{
		public int BlockNumber { get; set; }

		public string Address { get; set; }
	}
}
