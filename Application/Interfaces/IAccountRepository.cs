using Application.DTOS;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        public Task<AuthUserDTO> OwnerRegisterAsync(Owner owner, OwnerRegisterDTO registerDto);
        public Task<AuthUserDTO> CustomerRegisterAsync(Customer customer, CustomerRegisterDTO RegisterModel);
        public Task<AuthUserDTO> Login(LoginUserDTO loginUser);
        public Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordModel);
        public Task<AuthUserDTO> ConfirmEmailAsync(string email, string otp);
        public Task<bool> SendNewOTPAsync(string email);
        public Task<bool> ForgetPassword(string Email);


    }
}
