using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<Chat> GetChatByParticipantsAsync(string userId1, string userId2, bool isUser1Owner);
        Task AddAsync(Chat chat);
        Task SaveChangesAsync();
    }
}
