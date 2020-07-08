namespace EthereumTransactionSearch.Dtos.Infura
{
	public class InfuraTransactionSearchRequest
	{
		public string jsonrpc { get; set; }

		public string method { get; set; }

		public object[] @params { get; set;}

		public int id { get; set; }
	}
}
