using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CanopyExample
{
    public class CanopyApiBase
    {
        protected Task<HttpClient> CreateHttpClientAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpClient());
        }

        protected async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var result = new HttpRequestMessage();

            var authenticatedUser = await CanopyAuthentication.Instance.GetAuthenticatedUser();
            result.Headers.Add("Authorization", "Bearer " + authenticatedUser.AccessToken);

            return result;
        }

        protected virtual void PrepareRequest(HttpClient request, ref string url)
        {
            // ASP.NET Web API doesn't like having trailing ampersands in URLs.
            if (url.EndsWith("&"))
            {
                url = url.Substring(0, url.Length - 1);
            }
        }
    }
}