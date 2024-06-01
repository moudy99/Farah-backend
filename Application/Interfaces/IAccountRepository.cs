using Application.DTOS;
using Core.Entities;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        public Task<AuthUserDTO> OwnerRegisterAsync(Owner owner, OwnerRegisterDTO registerDto);
        public Task<AuthUserDTO> CustomerRegisterAsync(OwnerRegisterDTO RegisterModel);
        public Task<dynamic> Login(LoginUserDTO loginUser);
    }
}
