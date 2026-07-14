using System.Text;
using System.Text.Json;

namespace PortfolioAPI.Services;

public class MailService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public MailService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task SendAsync(string from, string subject, string message)
    {
        var apiKey = _configuration["Resend:ApiKey"];

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var body = new
        {
            from = "Portfolio <onboarding@resend.dev>",
            to = new[]
            {
    "beyzakocc21@hotmail.com"
            },
            subject = subject,
            html = $"""
                    <h2>Yeni Portfolio Mesajı</h2>

                    <p><strong>Gönderen:</strong> {from}</p>

                    <hr>

                    <p>{message}</p>
                    """
        };

        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            "https://api.resend.com/emails",
            content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}