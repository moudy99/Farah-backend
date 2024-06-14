using Application.DTOS;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        public AllServicesDTO GetOwnerServices(string ownerID);
        public Task<CustomResponseDTO<AuthUserDTO>> OwnerRegisterAsync(OwnerRegisterDTO RegisterModel);
        public Task<CustomResponseDTO<AuthUserDTO>> CustomerRegisterAsync(CustomerRegisterDTO RegisterModel);
        public Task<CustomResponseDTO<AuthUserDTO>> Login(LoginUserDTO loginUser);
        public Task<CustomResponseDTO<bool>> ChangePasswordAsync(ChangePasswordDTO changePasswordModel, string userId);

    }
}
