using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using Core.Entities;

namespace Application.Services
{
    public class UserOTPService : IUserOTPService
    {
        private readonly IUserOTPRepository _userOTPRepository;
        private readonly IEmailService emailService;

        public UserOTPService(IUserOTPRepository userOTPRepository, IEmailService emailService)
        {
            _userOTPRepository = userOTPRepository;
            this.emailService = emailService;
        }

        public async Task SaveAndSendOTPAsync(string email, string firstName, string lastName)
        {
            var otp = GenerateRandomCode.GetCode();

            var userOTP = new UserOTP
            {
                OTP = otp,
                Email = email,
                OTPGeneratedTime = DateTime.Now
            };

            await _userOTPRepository.SaveOTPAsync(userOTP);

            EmailDTO emailDTO = new EmailDTO
            {
                To = email,
                Subject = "Farah Account Verification OTP",
                Body = FormatEmail.CreateDesignForConfirmEmail(otp, $"{firstName} {lastName}", DateTime.Now.ToString("dd MMM, yyyy"))
            };
            await emailService.sendEmailAsync(emailDTO);
        }

        public async Task<bool> VerifyOTPAsync(string email, string otp)
        {
            var userOTP = await _userOTPRepository.GetOTPAsync(email, otp);

            if (userOTP == null || CalcOTPExpirationTime.IsOTPExpired(userOTP.OTPGeneratedTime))
            {
                if (userOTP != null && CalcOTPExpirationTime.IsOTPExpired(userOTP.OTPGeneratedTime))
                {
                    await _userOTPRepository.DeleteOTPAsync(userOTP);
                }
                return false;
            }

            return true;
        }

    }
}
