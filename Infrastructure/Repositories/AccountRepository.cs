using Application.DTOS;
using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserOTPService _userOTPService;

        public AccountRepository(ApplicationDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IUserOTPService userOTPService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            this._userOTPService = userOTPService;
        }
        public async Task<AuthUserDTO> RegisterUserAsync(ApplicationUser user, string password, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                return new AuthUserDTO() { Message = "Email is already registered" };
            }

            if (await _context.Users.AnyAsync(u => u.SSN == user.SSN))
            {
                return new AuthUserDTO() { Message = "SSN is already registered" };
            }

            user.UserName = GenerateUsernameFromEmail(user.Email);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);

                await _userOTPService.SaveAndSendOTPAsync(user.Email, user.FirstName, user.LastName);
                return new AuthUserDTO()
                {
                    Message = "Registration successful",
                    IsEmailConfirmed = user.EmailConfirmed,
                    Name = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    Succeeded = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(await CreateJwtToken(user)),
                    Role = role
                };
            }
            else
            {
                return new AuthUserDTO()
                {
                    Message = string.Join(", ", result.Errors.Select(error => error.Description)),
                    IsEmailConfirmed = false,
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



        public async Task<AuthUserDTO> ConfirmEmailAsync(string email, string otp)
        {
            var isValid = await _userOTPService.VerifyOTPAsync(email, otp);
            if (!isValid)
            {
                return new AuthUserDTO { Message = "Invalid or expired OTP. Please request a new OTP." };
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthUserDTO { Message = "User not found" };
            }

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            var checkUserType = (await _userManager.GetRolesAsync(user)).FirstOrDefault();


            return new AuthUserDTO
            {
                Message = "Email confirmed successfully.",
                IsEmailConfirmed = true,
                Succeeded = true,
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Token = checkUserType != "Owner" ? new JwtSecurityTokenHandler().WriteToken(await CreateJwtToken(user)) : null,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
        }

        public async Task<bool> SendNewOTPAsync(string email)
        {
            var currentUser = await _userManager.FindByEmailAsync(email);

            if (currentUser == null)
            {
                return false;
            }

            var checkUserType = (await _userManager.GetRolesAsync(currentUser)).FirstOrDefault();
            ApplicationUser user;

            if (checkUserType == "Owner")
            {
                user = await _context.Owners.FirstOrDefaultAsync(u => u.Id == currentUser.Id);
            }
            else
            {
                user = await _context.Customers.FirstOrDefaultAsync(u => u.Id == currentUser.Id);
            }

            if (user == null)
            {
                return false;
            }

            await _userOTPService.SendNewOTPAsync(email, user.FirstName, user.LastName);

            return true;
        }


        public async Task<AuthUserDTO> Login(LoginUserDTO loginUser)
        {
            // If i change the mail in sql i return null with the new mail

            //var user = await _userManager.FindByEmailAsync(loginUser.Email);

            //It work with the new mail 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);

            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, loginUser.Password);
                if (found)
                {
                    var checkUserType = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    Owner owner = null;

                    if (checkUserType == "Owner")
                    {
                        owner = await _context.Owners.FirstOrDefaultAsync(u => u.Id == user.Id);
                    }
                    var Token = await CreateJwtToken(user);

                    if (!user.EmailConfirmed)
                    {

                        return new AuthUserDTO()
                        {
                            Message = " Email not confirmed",
                            IsEmailConfirmed = false,
                            ExpireTIme = Token.ValidTo,
                            Token = new JwtSecurityTokenHandler().WriteToken(Token),
                            Succeeded = true,
                            Role = checkUserType,
                            AccountStatus = owner?.AccountStatus.ToString(),
                            Name = user.FirstName + " " + user.LastName,
                            Email = user.Email,
                        };
                    }
                    return new AuthUserDTO()
                    {
                        Message = "Login successful",
                        IsEmailConfirmed = true,
                        Succeeded = true,
                        Name = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        ExpireTIme = Token.ValidTo,
                        Token = new JwtSecurityTokenHandler().WriteToken(Token),
                        Role = checkUserType,
                        AccountStatus = owner?.AccountStatus.ToString()
                    };
                }
                else
                {
                    return new AuthUserDTO()
                    {
                        Message = "Login failed: Invalid email or password",
                        IsEmailConfirmed = user.EmailConfirmed,
                        Succeeded = false
                    };
                }
            }

            return new AuthUserDTO()
            {
                Message = "Login failed: User not found",
                IsEmailConfirmed = false,
                Succeeded = false
            };
        }


        public async Task<bool> ForgetPassword(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            if (User is null)
            {
                return false;
            }
            var TokenGenerated = await CreateJwtToken(User);
            var Token = new JwtSecurityTokenHandler().WriteToken(TokenGenerated);

            var result = _userOTPService.SendForgetPasswordLinkAsync(Email, Token, User.FirstName, User.LastName);

            if (result is null)
            {
                return false;
            }
            return true;


        }
        public async Task<IdentityResult> ChangePasswordAsync(string userEmail, ChangePasswordDTO changePasswordModel)
        {
            //var user = await _userManager.FindByEmailAsync(userEmail);

            // todo remove it .. it just for test  Cause i Change the main from the SQL 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "المستخدم غير موجود" });
            }

            if (user.YourFavirotePerson != changePasswordModel.SecurityQuestionAnswer)
            {
                return IdentityResult.Failed(new IdentityError { Description = "إجابة سؤال الأمان غير صحيحة" });
            }

            var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, changePasswordModel.OldPassword);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return IdentityResult.Failed(new IdentityError { Description = "كلمة المرور القديمة غير صحيحة" });
            }

            if (changePasswordModel.OldPassword == changePasswordModel.NewPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "لا يمكن أن تكون كلمة المرور الجديدة مطابقة للكلمة القديمة" });
            }

            return await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
        }


        private string GenerateUsernameFromEmail(string email)
        {
            return email.ToLower();
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
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
            );


            return jwtSecurityToken;
        }


        /// Get Profile info 
        public async Task<Owner> GetOwnerInfo(string Email)
        {
            Owner owner = _context.Owners.FirstOrDefault(email => email.Email == Email);
            if (owner == null)
            {
                return null;
            }
            return owner;
        }

        public async Task<bool> UpdateOwnerInfo(Owner owner)
        {
            try
            {
                _context.Owners.Update(owner);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
