using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace ZorUI.Core.Http;

/// <summary>
/// Simple HTTP API client for ZorUI applications.
/// </summary>
public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiClient(string baseUrl, HttpClient? httpClient = null)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }

    /// <summary>
    /// GET request.
    /// </summary>
    public async Task<T?> Get<T>(string path)
    {
        try
        {
            var response = await _httpClient.GetAsync(path);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            throw new ApiException($"GET {path} failed", ex);
        }
    }

    /// <summary>
    /// POST request.
    /// </summary>
    public async Task<TResponse?> Post<TRequest, TResponse>(string path, TRequest data)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(path, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        catch (Exception ex)
        {
            throw new ApiException($"POST {path} failed", ex);
        }
    }

    /// <summary>
    /// POST request without response.
    /// </summary>
    public async Task Post<TRequest>(string path, TRequest data)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(path, data);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApiException($"POST {path} failed", ex);
        }
    }

    /// <summary>
    /// PUT request.
    /// </summary>
    public async Task<TResponse?> Put<TRequest, TResponse>(string path, TRequest data)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(path, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        catch (Exception ex)
        {
            throw new ApiException($"PUT {path} failed", ex);
        }
    }

    /// <summary>
    /// DELETE request.
    /// </summary>
    public async Task Delete(string path)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApiException($"DELETE {path} failed", ex);
        }
    }

    /// <summary>
    /// Set authorization header.
    /// </summary>
    public ApiClient WithAuth(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return this;
    }

    /// <summary>
    /// Set custom header.
    /// </summary>
    public ApiClient WithHeader(string name, string value)
    {
        _httpClient.DefaultRequestHeaders.Add(name, value);
        return this;
    }

    /// <summary>
    /// Set timeout.
    /// </summary>
    public ApiClient WithTimeout(TimeSpan timeout)
    {
        _httpClient.Timeout = timeout;
        return this;
    }
}

/// <summary>
/// API exception.
/// </summary>
public class ApiException : Exception
{
    public ApiException(string message, Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Helper for reactive HTTP requests in components.
/// </summary>
public class AsyncState<T>
{
    public bool IsLoading { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }
    public bool HasError => Error != null;
    public bool HasData => Data != null && !HasError;
}

/// <summary>
/// Hook for making async requests.
/// </summary>
public static class AsyncHelper
{
    /// <summary>
    /// Execute async request and update state.
    /// </summary>
    public static async Task<AsyncState<T>> UseAsync<T>(
        Func<Task<T>> asyncFunc,
        Action<AsyncState<T>> setState)
    {
        var state = new AsyncState<T> { IsLoading = true };
        setState(state);

        try
        {
            var data = await asyncFunc();
            state = new AsyncState<T> { Data = data, IsLoading = false };
        }
        catch (Exception ex)
        {
            state = new AsyncState<T> { Error = ex.Message, IsLoading = false };
        }

        setState(state);
        return state;
    }
}
