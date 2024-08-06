using System.IdentityModel.Tokens.Jwt;
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
    public async Task<string> GenerateToken(string? role, string userName)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userName)
        };
        if (role == "Admin")
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
        {
            Expires = null,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = creds
        };
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
    
}