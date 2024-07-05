using Application.DTOS;
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

        public IQueryable<AllChatsDTO> GetMyChats(string userId, bool isOwner)
        {
            if (isOwner)
            {
                return _context.Chats
                    .Include(c => c.Messages)
                    .Include(c => c.Customer)
                    .Where(c => c.OwnerId == userId)
                    .Select(c => new AllChatsDTO
                    {
                        chatId = c.Id,
                        User = new UserDTO
                        {
                            Id = c.Customer.Id,
                            UserName = $"{c.Customer.FirstName} {c.Customer.LastName}",
                            ProfileImage = c.Customer.ProfileImage
                        },
                        lastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().Message,
                        lastMessageSentAt = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().SentAt,
                        isRead = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().IsRead
                    });
            }
            else
            {
                return _context.Chats
                    .Include(c => c.Messages)
                    .Include(c => c.Owner)
                    .Where(c => c.CustomerId == userId)
                    .Select(c => new AllChatsDTO
                    {
                        chatId = c.Id,
                        User = new UserDTO
                        {
                            Id = c.Owner.Id,
                            UserName = $"{c.Owner.FirstName} {c.Owner.LastName}",
                            ProfileImage = c.Owner.ProfileImage
                        },
                        lastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().Message,
                        lastMessageSentAt = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().SentAt,
                        isRead = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().IsRead
                    });
            }
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await _context.Chats
                .Include(c => c.Messages)
                .Include(c => c.Owner)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task MarkMessagesAsReadAsync(int chatId, string userId)
        {
            var unreadMessages = await _context.ChatMessages
                .Where(m => m.ChatId == chatId && m.ReceiverId == userId && !m.IsRead)
                .ToListAsync();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.IsRead = true;
                }

                await _context.SaveChangesAsync();
            }
        }

        public Chat GetChatBetweenOwnerandCustomer(string customerId, string ownerID)
        {
            return  _context.Chats
                    .Include(c => c.Messages)
                    .Include(c => c.Owner)
                    .Include(c => c.Customer)
                    .FirstOrDefault(c => c.CustomerId == customerId && c.OwnerId == ownerID);
        }
    }
}
