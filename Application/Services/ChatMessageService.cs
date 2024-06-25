using Application.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Core.Entities;


namespace Application.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IMapper _mapper;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatRepository _chatRepository;



        public ChatMessageService(IMapper mapper, IChatMessageRepository chatMessageRepository, IChatRepository chatRepository)
        {
            _mapper = mapper;
            _chatMessageRepository = chatMessageRepository;
            _chatRepository = chatRepository;
        }

        public async Task SendMessageAsync(string senderId, string receiverId, string message, bool isSenderOwner)
        {
            var chat = await _chatRepository.GetChatByParticipantsAsync(senderId, receiverId, isSenderOwner);

            if (chat == null)
            {
                chat = new Chat
                {
                    OwnerId = isSenderOwner ? senderId : receiverId,
                    CustomerId = isSenderOwner ? receiverId : senderId,
                    CreatedAt = DateTime.UtcNow
                };
                await _chatRepository.AddAsync(chat);
                await _chatRepository.SaveChangesAsync();
            }

            var chatMessage = new ChatMessage
            {
                ChatId = chat.Id,
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = message,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            await _chatMessageRepository.AddAsync(chatMessage);
            await _chatMessageRepository.SaveChangesAsync();
        }


    }
}
