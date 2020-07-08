using EthereumTransactionSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Services
{
	public interface ITransactionSearchService
	{
		Task<TransactionSearchResponse> Search(TransactionSearchRequest transactionSearchRequest);
	}
}
