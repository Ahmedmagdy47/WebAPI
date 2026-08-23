using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace WebAPI.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> Permissions)
        {
            // claims to be included in the token
            Claim[] claims = [
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new(JwtRegisteredClaimNames.Jti, Guid.CreateVersion7().ToString()),
                new(nameof(roles), JsonSerializer.Serialize(roles), JsonClaimValueTypes.JsonArray),
                new(nameof(Permissions), JsonSerializer.Serialize(Permissions), JsonClaimValueTypes.JsonArray)
            ];

            // The secret key used to sign the token
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            // The signing credentials specify the security key and the algorithm used to sign the token
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Token expiration time in minutes
            //var expiresIn = _jwtOptions.ExpireMinutes;
            // Add the expiration time to the current UTC time
            //var expiresDate = DateTime.UtcNow.AddMinutes(expiresIn);

            //generate the token
            var token = new JwtSecurityToken(
                //Who issued the token:my application
                issuer: _jwtOptions.Issuer,
                //Who is the intended audience of the token:my application users
                audience: _jwtOptions.Audience,
                //The claims to be included in the token
                claims: claims,
                //The expiration time of the token
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes),

                signingCredentials: signingCredentials
            );
            // token and expiresIn the returning values of the method
            // new JwtSecurityTokenHandler() create and validate the token
            //WriteToken() convert the token from object (JwtSecurityToken) to string
            return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: _jwtOptions.ExpireMinutes * 60);

        }

        public string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
