using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MobileSnake.Common.Contracts.Options;
using MobileSnake.Common.Contracts.Services;

namespace MobileSnake.Common.Services;

internal class JwtTokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions?.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
    }
    
    public string CreateToken(int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = GetTokenDescriptor(userId);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? GetUserIdByToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            return int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        }
        catch
        {
            return null;
        }
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Key)),
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
        };
    }

    private IEnumerable<Claim> GetClaims(int userId)
    {
        return new[] { new Claim("id", userId.ToString()) };
    }
    
    private SecurityTokenDescriptor GetTokenDescriptor(int userId)
    {
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GetClaims(userId)),
            Expires = DateTime.UtcNow.AddSeconds(_jwtOptions.TokenLifeTime),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}
