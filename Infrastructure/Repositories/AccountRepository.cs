using Application.DTOS;
using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
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
        private readonly IConfiguration _configuration;

        public AccountRepository(ApplicationDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<AuthUserDTO> RegisterUserAsync(ApplicationUser user, string password, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                return new AuthUserDTO() { Message = "Email is already registered" };
            }

            if (await _userManager.FindByNameAsync(user.UserName) is not null)
            {
                return new AuthUserDTO() { Message = "Username is already registered" };
            }

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);

                // Generate Token 
                var securityToken = await CreateJwtToken(user);
                return new AuthUserDTO()
                {
                    Message = "Registration successful",
                    IsAuthenticated = true,
                    Username = user.UserName,
                    Email = user.Email,
                    ExpireTIme = securityToken.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    Role = role
                };
            }
            else
            {
                return new AuthUserDTO()
                {
                    Message = string.Join(", ", result.Errors.Select(error => error.Description)),
                    IsAuthenticated = false,
                    Errors = result.Errors.Select(error => error.Description).ToList()
                };
            }
        }



        public async Task<AuthUserDTO> OwnerRegisterAsync(Owner owner, OwnerRegisterDTO registerDto)
        {
            return await RegisterUserAsync(owner, registerDto.Password, RolesEnum.Owner.ToString());
        }

        public async Task<AuthUserDTO> CustomerRegisterAsync(Customer customer, CustomerRegisterDTO registerDto)
        {
            return await RegisterUserAsync(customer, registerDto.Password, RolesEnum.Customer.ToString());
        }

        public async Task<AuthUserDTO> Login(LoginUserDTO loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user != null)
            {

                bool found = await _userManager.CheckPasswordAsync(user, loginUser.Password);
                if (found)
                {
                    // Generate Token 
                    var securityToken = await CreateJwtToken(user);
                    return new AuthUserDTO()
                    {
                        Message = "Login successful",
                        IsAuthenticated = true,
                        Username = user.UserName,
                        Email = user.Email,
                        ExpireTIme = securityToken.ValidTo,
                        Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                        Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                    };
                }

            }
            return new AuthUserDTO()
            {
                Message = "Login Failed",
                IsAuthenticated = false,
                Errors = new List<string> { "Email or Password is incorrect" }
            };
        }

        public async Task<IdentityResult> ChangePassword(ApplicationUser user, ChangePasswordDTO changePasswordModel)
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

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var durationDaysString = _configuration["JWT:DurationDays"];

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:IssuerIss"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
