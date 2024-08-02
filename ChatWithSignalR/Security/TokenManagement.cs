using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ChatWithSignalR.Security;

public class TokenManagement
{
    private readonly IConfiguration _configuration;

    public TokenManagement(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<string> GenerateToken(string role, int userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetConnectionString("Token")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
    }
    
}