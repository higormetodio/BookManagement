using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateBookOnlyActive;
using BookManagement.Application.Commands.UpdateBookStock;
using BookManagement.Application.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BookManagement.WebApp.Services;

public class BookService : IBookService
{
    private const string apiEndpoint = "/api/books/";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public BookService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
        _contextAccessor = contextAccessor;
    }

    public async Task<ResultViewModel<IEnumerable<BookViewModel>>> GetAllBooks()
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
            var bookViewModel = new List<BookViewModel>();
            var result = new ResultViewModel<IEnumerable<BookViewModel>>(bookViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<IEnumerable<BookViewModel>>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<BookDetailViewModel>> GetBookById(int id)
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
            BookDetailViewModel bookDetailViewModel = null;
            var result = new ResultViewModel<BookDetailViewModel>(bookDetailViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<BookDetailViewModel>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<BookLoansViewModel>> GetBookByIdLoans(int id)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.GetAsync(apiEndpoint + "loans/" + id);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            BookLoansViewModel bookLoansViewModel = null;
            var result = new ResultViewModel<BookLoansViewModel>(bookLoansViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<BookLoansViewModel>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<int>> CreateBook(CreateBookCommand command)
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
        
        var resultViewModel = JsonSerializer.Deserialize<ResultViewModel<int>>(apiResponse,_options);

        return resultViewModel;        
    }

    public async Task<ResultViewModel> UpdateBookStock(UpdateBookStockCommand command)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.PutAsJsonAsync(apiEndpoint + "stock", command);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var result = new ResultViewModel(false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();
        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel> UpdateBook(UpdateBookCommand command)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.PutAsJsonAsync(apiEndpoint, command);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var result = new ResultViewModel(false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();
        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel> UpdateBookOnlyActive(int id, UpdateBookOnlyActiveCommand command)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.PatchAsJsonAsync(apiEndpoint + "updateBookActive/" + id, command, _options);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var result = new ResultViewModel(false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();
        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel>(apiResponse);

        return resultViewModel;
    }

    public async Task<ResultViewModel> DeleteBook(int id)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.DeleteAsync(apiEndpoint + id);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var result = new ResultViewModel(false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();
        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel>(apiResponse);

        return resultViewModel;
    }
}
