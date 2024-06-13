using Application.DTOS;
using Application.Helpers;
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
            var IDBackImage = await ImageSavingHelper.SaveOneImageAsync(registerModel.IDBackImageFile, "OwnersImages");
            var IDFrontImage = await ImageSavingHelper.SaveOneImageAsync(registerModel.IDFrontImageFile, "OwnersImages");
            var profilePic = await ImageSavingHelper.SaveOneImageAsync(registerModel.ProfileImageFile, "OwnersImages");
            owner.IDBackImage = IDBackImage;
            owner.IDFrontImage = IDFrontImage;
            owner.ProfileImage = profilePic;
            var registrationResult = await _accountRepository.OwnerRegisterAsync(owner, registerModel);

            return new CustomResponseDTO<AuthUserDTO>
            {
                Data = registrationResult,
                Message = registrationResult.Message,
                Succeeded = registrationResult.Succeeded,
                Errors = registrationResult.Errors
            };
        }

        public async Task<CustomResponseDTO<AuthUserDTO>> CustomerRegisterAsync(CustomerRegisterDTO registerModel)
        {
            var customer = _mapper.Map<Customer>(registerModel);
            var registrationResult = await _accountRepository.CustomerRegisterAsync(customer, registerModel);

            return new CustomResponseDTO<AuthUserDTO>
            {
                Data = registrationResult,
                Message = registrationResult.Message,
                Succeeded = registrationResult.Succeeded,
                Errors = registrationResult.Errors
            };
        }

        public async Task<CustomResponseDTO<AuthUserDTO>> ConfirmEmailAsync(string email, string otp)
        {
            var result = await _accountRepository.ConfirmEmailAsync(email, otp);

            return new CustomResponseDTO<AuthUserDTO>
            {
                Data = result.IsEmailConfirmed ? result : null,
                Message = result.Message,
                Succeeded = result.Succeeded,
                Errors = result.Errors
            };
        }
        public async Task<CustomResponseDTO<string>> SendNewOTPAsync(string email)
        {
            var result = await _accountRepository.SendNewOTPAsync(email);
            if (result)
            {
                return new CustomResponseDTO<string>
                {
                    Data = "OTP sent successfully",
                    Succeeded = true
                };
            }
            else
            {
                return new CustomResponseDTO<string>
                {
                    Message = "Failed to send OTP",
                    Succeeded = false
                };
            }
        }


        public async Task<CustomResponseDTO<AuthUserDTO>> Login(LoginUserDTO loginUser)
        {
            var LoginResult = await _accountRepository.Login(loginUser);
            return new CustomResponseDTO<AuthUserDTO>
            {
                Data = LoginResult,
                Message = LoginResult.Message,
                Succeeded = LoginResult.Succeeded,
                Errors = LoginResult.Errors

            };
        }

        public async Task<CustomResponseDTO<string>> ForgetPassword(string Email)
        {
            var result = await _accountRepository.ForgetPassword(Email);
            if (result != null)
            {
                return new CustomResponseDTO<string>()
                {
                    Data = "Reset Password Link sent successfully",
                    Succeeded = true
                };
            }
            return new CustomResponseDTO<string>()
            {
                Data = "Failed to send THe Reset Password Link",
                Succeeded = false
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
