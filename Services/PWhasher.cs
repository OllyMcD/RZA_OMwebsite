using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace RZA_OMwebsite.Services
{
    public class PasswordHashingService
{
    private readonly PasswordHasher<object> passwordHasher = new PasswordHasher<object>();

    public string HashPassword(string password)
    {
        return passwordHasher.HashPassword(new object(), password);
    }

    public PasswordVerificationResult VerifyPassword(string hashedPassword, string password)
    {
        return passwordHasher.VerifyHashedPassword(new object(), hashedPassword, password);
    }

    public bool IsPasswordHashed(string hashedPassword, string plaintextPassword)
    {
        var verificationResult = passwordHasher.VerifyHashedPassword(new object(), hashedPassword, plaintextPassword);
        return verificationResult != PasswordVerificationResult.Failed;
    }
}

}
