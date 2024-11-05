using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Commands.ReturnLoan;
using BookManagement.Application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BookManagement.WebApp.Services;

public class LoanService : ILoanService
{
    private const string apiEndpoint = "/api/loans/";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public LoanService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
        _contextAccessor = contextAccessor;
    }

    public async Task<ResultViewModel<IEnumerable<LoanViewModel>>> GetAllLoans()
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.GetAsync(apiEndpoint);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var loanViewModel = new List<LoanViewModel>();
            var result = new ResultViewModel<IEnumerable<LoanViewModel>>(loanViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<IEnumerable<LoanViewModel>>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<LoanDetailViewModel>> GetLoanById(int id)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.GetAsync(apiEndpoint + id);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            LoanDetailViewModel loanDetailViewModel = null;
            var result = new ResultViewModel<LoanDetailViewModel>(loanDetailViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<LoanDetailViewModel>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<int>> CreateLoan(CreateLoanCommand command)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var book = JsonSerializer.Serialize(command);

        StringContent content = new StringContent(book, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(apiEndpoint, content);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var result = new ResultViewModel<int>(0, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = JsonSerializer.Deserialize<ResultViewModel<int>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel> ReturnLoan(int id)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.PutAsJsonAsync(apiEndpoint + "returnLoan/" + id, id);
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var result = new ResultViewModel(false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();
        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel>(apiResponse, _options);

        return resultViewModel;
    }    
}
