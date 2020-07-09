namespace EthereumTransactionSearch.Models
{
	public class TransactionResults
	{
		public string BlockHash { get; set; }

		public long BlockNumber { get; set; }

		public string From { get; set; }

		public string To { get; set; }

		public decimal Gas { get; set; }

		public string Hash { get; set; }

		public decimal Value { get; set; }

		public long TransactionIndex { get; set; }
	}
}
