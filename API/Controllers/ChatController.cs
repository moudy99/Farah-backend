using Application.DTOS;
using Application.Interfaces;
using Application.Services;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Presentation.Hubs;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(IChatMessageService chatMessageService, IHubContext<ChatHub> chatHubContext, UserManager<ApplicationUser> userManager)
        {
            _chatMessageService = chatMessageService;
            _chatHubContext = chatHubContext;
            _userManager = userManager;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO dto)
        {
            var senderId = User.FindFirstValue("uid");


            ApplicationUser sender = await _userManager.FindByIdAsync(senderId);
            bool isSenderOwner = sender is Owner;

            await _chatMessageService.SendMessageAsync(senderId, dto.ReceiverId, dto.Message, isSenderOwner);

            await _chatHubContext.Clients.All.SendAsync("ReceiveMessage", new
            {
                SenderId = senderId,
                Message = dto.Message,
                SentAt = DateTime.UtcNow
            });

            return Ok();
        }

    }
}
