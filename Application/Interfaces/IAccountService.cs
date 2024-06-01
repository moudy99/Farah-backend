using Application.DTOS;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        public Task<CustomResponseDTO<AuthUserDTO>> OwnerRegisterAsync(OwnerRegisterDTO RegisterModel);
        public Task<CustomResponseDTO<AuthUserDTO>> CustomerRegisterAsync(CustomerRegisterDTO RegisterModel);
        public Task<CustomResponseDTO<AuthUserDTO>> Login(LoginUserDTO loginUser);
    }
}
