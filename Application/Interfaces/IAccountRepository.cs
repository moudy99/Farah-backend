﻿using Application.DTOS;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        public Task<AuthUserDTO> OwnerRegisterAsync(Owner owner, OwnerRegisterDTO registerDto);
        public Task<AuthUserDTO> CustomerRegisterAsync(Customer customer, CustomerRegisterDTO RegisterModel);
        public Task<AuthUserDTO> Login(LoginUserDTO loginUser);
        public Task<IdentityResult> ChangePassword(ApplicationUser user, ChangePasswordDTO changePasswordModel);

    }
}