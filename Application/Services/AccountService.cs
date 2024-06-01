﻿using Application.DTOS;
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

        public async Task<CustomResponseDTO<AuthUserDTO>> OwnerRegisterAsync(OwnerRegisterDTO registerModel)
        {
            var owner = _mapper.Map<Owner>(registerModel);
            var registrationResult = await _accountRepository.OwnerRegisterAsync(owner, registerModel);

            return new CustomResponseDTO<AuthUserDTO>
            {
                Data = registrationResult.IsAuthenticated ? registrationResult : null,
                Message = registrationResult.Message,
                Succeeded = registrationResult.IsAuthenticated,
                Errors = registrationResult.Errors
            };
        }

        public async Task<CustomResponseDTO<AuthUserDTO>> CustomerRegisterAsync(CustomerRegisterDTO registerModel)
        {
            var customer = _mapper.Map<Customer>(registerModel);
            var registrationResult = await _accountRepository.CustomerRegisterAsync(customer, registerModel);

            return new CustomResponseDTO<AuthUserDTO>
            {
                Data = registrationResult.IsAuthenticated ? registrationResult : null,
                Message = registrationResult.Message,
                Succeeded = registrationResult.IsAuthenticated,
                Errors = registrationResult.Errors
            };
        }

        public async Task<CustomResponseDTO<AuthUserDTO>> Login(LoginUserDTO loginUser)
        {
            var LoginResult = await _accountRepository.Login(loginUser);
            return new CustomResponseDTO<AuthUserDTO>
            {
                Data = LoginResult.IsAuthenticated ? LoginResult : null,
                Message = LoginResult.Message,
                Succeeded = LoginResult.IsAuthenticated,
                Errors = LoginResult.Errors
            };
        }

        public async Task<CustomResponseDTO<bool>> ChangePasswordAsync(ChangePasswordDTO changePasswordModel, string userEmail)
        {
            var result = await _accountRepository.ChangePasswordAsync(userEmail, changePasswordModel);

            return new CustomResponseDTO<bool>
            {
                Data = result.Succeeded,
                Message = result.Succeeded ? "Password changed successfully" : "Password change failed",
                Succeeded = result.Succeeded,
                Errors = result.Succeeded ? null : result.Errors.Select(e => e.Description).ToList()
            };
        }
    }


}
