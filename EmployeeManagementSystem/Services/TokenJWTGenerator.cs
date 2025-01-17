using EmployeeManagementSystem.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystem.Services;

public class TokenJWTGenerator {
	private readonly IConfiguration configuration;

	public TokenJWTGenerator(IConfiguration configuration) {
		this.configuration = configuration;
	}
	public string JwtGeneratorToken(Employee user, IList<string> roles) {
		var jwtSettings = configuration.GetSection("JwtSettings");
		var claims = new List<Claim> {
			new Claim(JwtRegisteredClaimNames.Sub,user.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Name, user.UserName)
		};
		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var token = new JwtSecurityToken(
			issuer: jwtSettings["Issuer"],
			audience: jwtSettings["Audience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
			signingCredentials: credentials
			);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}

}
