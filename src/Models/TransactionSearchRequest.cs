using System.ComponentModel.DataAnnotations;

namespace EthereumTransactionSearch.Models
{
	public class TransactionSearchRequest
	{
		public int BlockNumber { get; set; }

		[Required]
		public string Address { get; set; }
	}
}
