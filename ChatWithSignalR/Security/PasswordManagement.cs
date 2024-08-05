using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ChatWithSignalR.Security;

public class PasswordManagement
{
    private readonly IConfiguration _configuration;

    public PasswordManagement(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public byte[] GetPasswordHash(string password, byte[] passwordSalt)
    {
        var hashed = KeyDerivation.Pbkdf2(
            password: password,
            salt: passwordSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32);
        return hashed;
    }

    public byte[] CreatePasswordSalt()
    {
        var size = new byte[16];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(size);
        }
        return size;
    }

    public bool IsValidPassowrHash(byte[] userPasswordHash, byte[] generatePasswordHash)
    {
        for (int i = 0; i < userPasswordHash.Length; i++)
        {
            if (userPasswordHash[i] != generatePasswordHash[i])
            {
                return false;
            }
        }
        return true;
    }
}