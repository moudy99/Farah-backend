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



        [HttpGet("my-chats")]
        public async Task<IActionResult> GetMyChats( int page = 1, int pageSize = 6)
        {
            try
            {
                string userId = User.FindFirstValue("uid");

                ApplicationUser user = await _userManager.FindByIdAsync(userId);

                bool isOwner = user is Owner;

                var response = _chatMessageService.GetMyChats(page, pageSize, userId, isOwner);

                if (response.Data == null || !response.Data.Any())
                {
                    return NotFound(new CustomResponseDTO<List<AllChatsDTO>>
                    {
                        Data = null,
                        Message = "مفيش شات ليك",
                        Succeeded = false,
                        Errors = null
                    });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResponseDTO<List<AllChatsDTO>>
                {
                    Data = null,
                    Message = $"ايرور يا معلم ابلع: {ex.Message}",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }
        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO dto)
        {
            var senderId = User.FindFirstValue("uid");


            ApplicationUser sender = await _userManager.FindByIdAsync(senderId);
            bool isSenderOwner = sender is Owner;

            await _chatMessageService.SendMessageAsync(senderId, dto.ReceiverId, dto.Message, isSenderOwner);

            await _chatHubContext.Clients.Users(senderId,dto.ReceiverId).SendAsync("ReceiveMessage", new
            {
                SenderId = senderId,
                Message = dto.Message,
                SentAt = DateTime.UtcNow
            });

            return Ok();
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChatById(int chatId)
        {
            try
            {
                var userId = User.FindFirstValue("uid");
                ApplicationUser user = await _userManager.FindByIdAsync(userId);

                bool isOwner = user is Owner;

                var chatDetails = await _chatMessageService.GetChatByIdAsync(chatId, userId, isOwner);
                return Ok(new CustomResponseDTO<ChatDetailsDTO>
                {
                    Data = chatDetails,
                    Message = "Chat retrieved successfully",
                    Succeeded = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResponseDTO<ChatDetailsDTO>
                {
                    Data = null,
                    Message = $"ابلع ايروووووووور: {ex.Message}",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet("GetChatBetweenOwnerandCustomer")]
        public async Task<ActionResult> GetChatBetweenOwnerandCustomer(string ownerID)
        {
            try
            {
                var customerId = User.FindFirstValue("uid");
                ApplicationUser user = await _userManager.FindByIdAsync(customerId);

                bool isCustomer = user is Customer;

                if (!isCustomer) 
                    return BadRequest();

                var chatDetails = await _chatMessageService.GetChatBetweenOwnerandCustomer(customerId, ownerID);
                return Ok(new CustomResponseDTO<ChatDetailsDTO>
                {
                    Data = chatDetails,
                    Message = "جالك الشات تمام",
                    Succeeded = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResponseDTO<ChatDetailsDTO>
                {
                    Data = null,
                    Message = $"ابلع ايروووووووور: {ex.Message}",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
