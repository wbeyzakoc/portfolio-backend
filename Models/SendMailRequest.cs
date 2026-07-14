namespace PortfolioAPI.Models;

public class SendMailRequest
{
    public string From { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}