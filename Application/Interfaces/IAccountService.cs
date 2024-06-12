using Application.DTOS;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        public Task<CustomResponseDTO<AuthUserDTO>> OwnerRegisterAsync(OwnerRegisterDTO RegisterModel);
        public Task<CustomResponseDTO<AuthUserDTO>> CustomerRegisterAsync(CustomerRegisterDTO RegisterModel);
        public Task<CustomResponseDTO<AuthUserDTO>> Login(LoginUserDTO loginUser);
        public Task<CustomResponseDTO<bool>> ChangePasswordAsync(ChangePasswordDTO changePasswordModel, string userId);
        public Task<CustomResponseDTO<AuthUserDTO>> ConfirmEmailAsync(string email, string otp);
        public Task<CustomResponseDTO<string>> SendNewOTPAsync(string email);

    }
}
