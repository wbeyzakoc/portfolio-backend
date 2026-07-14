using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly MailService _mailService;

    public MailController(MailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendMailRequest request)
    {
        await _mailService.SendAsync(
            request.From,
            request.Subject,
            request.Message);

        return Ok(new
        {
            success = true,
            message = "Mail gönderildi."
        });
    }
}