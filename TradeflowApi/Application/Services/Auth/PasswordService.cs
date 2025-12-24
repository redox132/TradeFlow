using System;
using System.Security.Cryptography;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16; // 128-bit
    private const int KeySize = 32;  // 256-bit
    private const int Iterations = 100_000; // recommended

    public string HashPassword(string password)
    {
        // Generate random salt
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Derive key using PBKDF2
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize
        );

        // Combine salt + key
        byte[] hashBytes = new byte[SaltSize + KeySize];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
        Buffer.BlockCopy(key, 0, hashBytes, SaltSize, KeySize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

        byte[] key = new byte[KeySize];
        Buffer.BlockCopy(hashBytes, SaltSize, key, 0, KeySize);

        byte[] keyToCheck = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize
        );

        // Constant-time comparison
        return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
    }
}
