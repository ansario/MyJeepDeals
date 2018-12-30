using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public class MockHttpMessageHandler
    : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsyncFunc;

        public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsyncFunc)
        {
            _sendAsyncFunc = sendAsyncFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _sendAsyncFunc.Invoke(request, cancellationToken);
        }
    }
}