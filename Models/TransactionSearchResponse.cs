using System.Collections.Generic;

namespace EthereumTransactionSearch.Models
{
	public class TransactionSearchResponse
	{
		public List<TransactionResults> Transactions { get; set; }
	}
}