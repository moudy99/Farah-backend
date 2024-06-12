using Application.Interfaces;
using Core.Entities;

namespace Infrastructure.Repositories
{
    public class UserOTPRepository : IUserOTPRepository
    {
        private readonly ApplicationDBContext _context;

        public UserOTPRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task SaveOTPAsync(UserOTP userOTP)
        {
            _context.UserOTPs.Add(userOTP);
            await _context.SaveChangesAsync();
        }
    }
}
