using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.LoginUser;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.Commands.UpdateUserOnlyActive;
using BookManagement.Application.Models;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.WebApp.Services;

public class UserService : IUserService
{
    private const string apiEndpoint = "/api/users/";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
        _contextAccessor = contextAccessor;
    }

    public async Task<ResultViewModel<IEnumerable<UserViewModel>>> GetAllUsers()
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
            var userViewModel = new List<UserViewModel>();
            var result = new ResultViewModel<IEnumerable<UserViewModel>>(userViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<IEnumerable<UserViewModel>>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<UserViewModel>> GetUserById(int id)
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
            UserViewModel userViewModel = null;
            var result = new ResultViewModel<UserViewModel>(userViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<UserViewModel>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<UserLoansViewModel>> GetUserByIdLoans(int id)
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
            UserLoansViewModel userLoansViewModel = null;
            var result = new ResultViewModel<UserLoansViewModel>(userLoansViewModel, false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<UserLoansViewModel>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel<int>> CreateUser(CreateUserCommand command)
    {
        ResultViewModel<int> resultViewModel;

        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var user = JsonSerializer.Serialize(command);

        StringContent content = new StringContent(user, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(apiEndpoint, content);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            resultViewModel = new ResultViewModel<int>(0, false, "User Unauthorized");

            return resultViewModel;
        }

        

        var apiResponse = await response.Content.ReadAsStreamAsync();

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            ProblemDetails resultError;
            resultError = JsonSerializer.Deserialize<ProblemDetails>(apiResponse, _options);
            resultViewModel = new ResultViewModel<int>(0, false, resultError.Detail);

            return resultViewModel;
        }

        resultViewModel = JsonSerializer.Deserialize<ResultViewModel<int>>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel> UpdateUser(UpdateUserCommand command)
    {
        ResultViewModel resultViewModel;

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

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            ProblemDetails resultError;
            resultError = JsonSerializer.Deserialize<ProblemDetails>(apiResponse, _options);
            resultViewModel = new ResultViewModel(false, resultError.Detail);

            return resultViewModel;
        }

        resultViewModel = JsonSerializer.Deserialize<ResultViewModel>(apiResponse, _options);

        return resultViewModel;
    }

    public async Task<ResultViewModel> UpdateUserOnlyActive(int id, UpdateUserOnlyActiveCommand command)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");
        var token = _contextAccessor.HttpContext.Request.Cookies["X-Access-Token"];

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.PatchAsJsonAsync(apiEndpoint + "updateUserActive/" + id, command, _options);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var result = new ResultViewModel(false, "User Unauthorized");

            return result;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();
        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel>(apiResponse);

        return resultViewModel;
    }

    public async Task<ResultViewModel> DeleteUser(int id)
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

    public async Task<ResultViewModel<LoginUserViewModel>> LoginUser(LoginUserCommand command)
    {
        var client = _clientFactory.CreateClient("BookManagementApi");

        var user = JsonSerializer.Serialize(command);

        StringContent content = new(user, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(apiEndpoint + "login", content);

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var resultViewModel = await JsonSerializer.DeserializeAsync<ResultViewModel<LoginUserViewModel>>(apiResponse, _options);

        return resultViewModel;
    }

}
