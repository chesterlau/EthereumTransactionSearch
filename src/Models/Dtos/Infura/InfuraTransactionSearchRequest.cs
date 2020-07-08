namespace EthereumTransactionSearch.Dtos.Infura
{
	public class InfuraTransactionSearchRequest
	{
		public string Jsonrpc { get; set; }

		public string Method { get; set; }

		public object[] Params { get; set;}

		public int Id { get; set; }
	}
}
