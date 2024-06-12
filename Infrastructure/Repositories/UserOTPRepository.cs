using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserOTPRepository : IUserOTPRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserOTPRepository(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        public async Task SaveOTPAsync(UserOTP userOTP)
        {
            _context.userOTPs.Add(userOTP);
            await _context.SaveChangesAsync();
        }

        public async Task<UserOTP> GetOTPAsync(string email, string otp)
        {
            return await _context.userOTPs
                .FirstOrDefaultAsync(u => u.Email == email && u.OTP == otp);
        }

        public async Task DeleteOTPAsync(UserOTP userOTP)
        {
            _context.userOTPs.Remove(userOTP);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


    }
}
