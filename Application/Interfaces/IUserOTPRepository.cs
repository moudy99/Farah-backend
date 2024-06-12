using Core.Entities;

namespace Application.Interfaces
{
    public interface IUserOTPRepository
    {
        Task SaveOTPAsync(UserOTP userOTP);

    }
}
