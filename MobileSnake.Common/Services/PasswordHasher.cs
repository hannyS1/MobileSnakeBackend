using System.Security.Cryptography;
using System.Text;
using MobileSnake.Common.Contracts.Services;

namespace MobileSnake.Common.Services;

internal class PasswordHasher : IPasswordHasher
{
    
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
    
    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            _hashAlgorithm,
            KeySize);
        return Convert.ToHexString(hash);
    }

    public bool Verify(string password, string hashedPassword)
    {
        var currentHashedPassword = Hash(password);
        return currentHashedPassword == hashedPassword;
    }
}