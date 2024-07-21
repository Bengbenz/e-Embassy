// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.SharedKernel;
using RestSharp;

namespace Bengbenz.Embassy.eServices.Client.Services;

public class HttpService(HttpClient httpClient) : IDisposable
{
  private readonly RestClient _restClient = new (httpClient);

  public async Task<T?> GetAsync<T>(string uri) where T : class
  {
    var request = new RestRequest(uri);
    request.AddHeader("Accept", "application/json");
    return await _restClient.GetAsync<T>(request);
  }
  
  public async Task<T?> PostAsync<T>(string uri, object dataToSend) where T : class
  {
    var request = new RestRequest(uri, Method.Post);
    request.AddJsonBody(dataToSend);
    return await _restClient.PostAsync<T>(request);
  }
  
  public async Task<T?> PutAsync<T>(string uri, object dataToSend) where T : class
  {
    var request = new RestRequest(uri, Method.Put);
    request.AddJsonBody(dataToSend);
    return await _restClient.PutAsync<T>(request);
  }
  
  public async Task<T?> DeleteAsync<T>(string uri, int id) where T : class
  {
    var request = new RestRequest($"{uri}/{id}", Method.Delete);
    return await _restClient.DeleteAsync<T>(request);
  }
  
  public void Dispose()
  {
    httpClient.Dispose();
    _restClient.Dispose();
    GC.SuppressFinalize(this);
  }
}
