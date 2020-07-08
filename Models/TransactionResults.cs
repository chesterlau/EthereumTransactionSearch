namespace EthereumTransactionSearch.Models
{
	public class TransactionResults
	{
		public string BlockHash { get; set; }

		public int BlockNumber { get; set; }

		public string From { get; set; }

		public string To { get; set; }

		public decimal Gas { get; set; }

		public string Hash { get; set; }

		public decimal Value { get; set; }

		public int TransactionIndex { get; set; }
	}
}
