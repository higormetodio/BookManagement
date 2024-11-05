using System.Net.Http.Headers;

namespace BookManagement.WebApp.Services;

public class ApiClientHelper
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAcessor;

    public ApiClientHelper(IHttpClientFactory clientFactory, IHttpContextAccessor contextAcessor)
    {
        _clientFactory = clientFactory;
        _contextAcessor = contextAcessor;
    }

    public HttpClient GetAuthenticatedClient()
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAcessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (token is null)
        {
            return null;
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }
}
