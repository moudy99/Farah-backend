namespace Application.Interfaces
{
    public interface IUserOTPService
    {
        public Task SaveAndSendOTPAsync(string email, string firstName, string lastName);
        public Task<bool> VerifyOTPAsync(string email, string otp);
    }
}
