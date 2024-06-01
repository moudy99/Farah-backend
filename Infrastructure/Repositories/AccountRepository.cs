using Application.DTOS;
using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(ApplicationDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<AuthUserDTO> OwnerRegisterAsync(Owner owner, OwnerRegisterDTO registerDto)
        {
            if (await _userManager.FindByEmailAsync(owner.Email) is not null)
            {
                return new AuthUserDTO() { Message = "Email is already registered" };
            }

            if (await _userManager.FindByNameAsync(owner.UserName) is not null)
            {
                return new AuthUserDTO() { Message = "Username is already registered" };
            }

            var result = await _userManager.CreateAsync(owner, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(owner, RolesEnum.Owner.ToString());

                // Generate Token 
                var securityToken = await CreateJwtToken(owner);
                return new AuthUserDTO()
                {
                    Message = "Registration successful",
                    IsAuthenticated = true,
                    Username = owner.UserName,
                    Email = owner.Email,
                    ExpireTIme = securityToken.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    Role = RolesEnum.Owner.ToString()
                };
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(error => error.Description));
                return new AuthUserDTO() { Message = errors, IsAuthenticated = false };
            }
        }

        public async Task<AuthUserDTO> CustomerRegisterAsync(OwnerRegisterDTO RegisterModel)
        {

            throw new NotImplementedException();

        }

        public async Task<dynamic> Login(LoginUserDTO loginUser)
        {
            throw new NotImplementedException();
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0uB1C+Kd1K+UPqBPTJRrYCzbAryqyHnAyyBDHMIU94w="));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "http://localhost:49475/",
                audience: "http://localhost:4200",
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
