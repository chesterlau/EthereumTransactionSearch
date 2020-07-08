using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Dtos.Infura
{
	public class InfuraTransactionSearchResponse
	{
		public string Jsonrpc { get; set; }

		public string Id { get; set; }

		public Result Result { get; set; }
	}

	public class Result
	{
		public List<Transactions> Transactions { get; set; }
	}

	public class Transactions
	{
		public string BlockHash { get; set; }
		
		public string BlockNumber { get; set; }

		public string From { get; set; }

		public string To { get; set; }

		public string Gas { get; set; }

		public string Hash { get; set; }

		public string Value { get; set; }

		public string TransactionIndex { get; set; }
	}
}
