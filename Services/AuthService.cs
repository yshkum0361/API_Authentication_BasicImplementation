using API_Authentication_BasicImplementation.Data;
using API_Authentication_BasicImplementation.Entities;
using API_Authentication_BasicImplementation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Authentication_BasicImplementation.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<string?> LoginAsync(UserDTO request)
        {

            var user = await context.UserLoginDetails.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null)
            {
                return null;
            }

            if(new PasswordHasher<User>().VerifyHashedPassword(user,user.PasswordHash,request.Password)==PasswordVerificationResult.Failed)
            {
                return null;
            }


           return CreateToken(user);
        }

        public async Task<User?> RegisterAsync(UserDTO request)
        {
            if(await context.UserLoginDetails.AnyAsync(u => u.UserName == request.UserName))
            {
                return null;
            }

            var user = new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user,request.Password);

            user.UserName = request.UserName;   
            user.PasswordHash = hashedPassword;

            context.UserLoginDetails.Add(user);
            await context.SaveChangesAsync();

            return user;
        }


        //----------------Json Web Token Code---------------//
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString())

            };


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
                );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDiscriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDiscriptor);


        }
    }
}
