using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Middleware
{
	public class LogContextMiddleware
	{
		private readonly RequestDelegate _next;
		private const string UserAgentHeaderKey = "User-Agent";

		public LogContextMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			string userAgentFromHeader = context.Request.Headers.ContainsKey(UserAgentHeaderKey) ? context.Request.Headers[UserAgentHeaderKey].ToString() : null;

			LogContext.PushProperty("UserAgent", userAgentFromHeader);

			await _next(context);
		}
	}
}
