using Application.DTOS;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<AuthUserDTO>> OwnerRegisterAsync(OwnerRegisterDTO RegisterModel)
        {
            var owner = _mapper.Map<Owner>(RegisterModel);

            var registrationResult = await _accountRepository.OwnerRegisterAsync(owner, RegisterModel);

            if (registrationResult.IsAuthenticated)
            {
                return new CustomResponseDTO<AuthUserDTO>
                {
                    Data = registrationResult,
                    Message = "Registration successful",
                    Succeeded = true,
                    Errors = null
                };

            }
            else
            {
                return new CustomResponseDTO<AuthUserDTO>
                {
                    Data = null,
                    Message = registrationResult.Message,
                    Succeeded = false,
                    Errors = null
                };
            }
        }

        public async Task<CustomResponseDTO<AuthUserDTO>> CustomerRegisterAsync(CustomerRegisterDTO RegisterModel)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDTO<AuthUserDTO>> Login(LoginUserDTO loginUser)
        {
            throw new NotImplementedException();
        }
    }
}
