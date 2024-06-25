using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        private readonly ApplicationDBContext _context;

        public ChatRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Chat> GetChatByParticipantsAsync(string userId1, string userId2, bool isUser1Owner)
        {
            if (isUser1Owner)
            {
                return await _context.Chats
                    .FirstOrDefaultAsync(c => c.OwnerId == userId1 && c.CustomerId == userId2);
            }
            else
            {
                return await _context.Chats
                    .FirstOrDefaultAsync(c => c.OwnerId == userId2 && c.CustomerId == userId1);
            }
        }

        public async Task AddAsync(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
