using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteServiceController : ControllerBase
    {
        private readonly IFavoriteServiceLayer FavoriteService;

        public FavoriteServiceController(IFavoriteServiceLayer _favoriteService)
        {
            FavoriteService = _favoriteService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            string CustomerID = User.FindFirstValue("uid");
            try
            {
                var response = FavoriteService.GetAll(CustomerID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
