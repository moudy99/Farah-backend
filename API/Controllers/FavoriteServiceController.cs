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

        //[HttpGet]
        //public IActionResult GetAll(int page = 1, int pageSize = 6)
        //{
        //    string CustomerID = User.FindFirstValue("uid");
        //    try
        //    {
        //        var response = FavoriteService.GetAll(page, pageSize, CustomerID);
        //    }
        
        //}
    }
}
