using BuyCars.Api.Filters;
using BuyCars.BusinessLogic;
using BuyCars.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;

namespace BuyCars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly BussinesLogic _bl = new();

        [HttpGet("{userId:int}")]
        [UserMod]
        public IActionResult GetFavorites(int userId)
        {
            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            // Users can only view their own favorites; admin can view anyone's
            if (currentUser.Id != userId && currentUser.Role != "admin")
                return StatusCode(403, new { message = "§¯§Ö§ä §Õ§à§ã§ä§å§á§Ñ §Ü §é§å§Ø§Ú§Þ §Ú§Ù§Ò§â§Ñ§ß§ß§í§Þ" });

            var favBl = _bl.GetFavoriteBL();
            var favorites = favBl.GetUserFavorites(userId);
            return Ok(favorites);
        }

        [HttpPost]
        [UserMod]
        public IActionResult AddFavorite([FromBody] FavoriteRequest request)
        {
            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            var favBl = _bl.GetFavoriteBL();
            var favorite = favBl.AddFavorite(currentUser.Id, request.CarId);

            if (favorite == null)
                return BadRequest(new { message = "§µ§Ø§Ö §Ó §Ú§Ù§Ò§â§Ñ§ß§ß§à§Þ §Ú§Ý§Ú §Ñ§Ó§ä§à §ß§Ö §ß§Ñ§Û§Õ§Ö§ß§à" });

            return Created($"/api/favorite/{currentUser.Id}", favorite);
        }

        [HttpDelete("{carId:int}")]
        [UserMod]
        public IActionResult RemoveFavorite(int carId)
        {
            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            var favBl = _bl.GetFavoriteBL();
            var removed = favBl.RemoveFavorite(currentUser.Id, carId);

            if (!removed)
                return NotFound(new { message = "§©§Ñ§á§Ú§ã§î §Ó §Ú§Ù§Ò§â§Ñ§ß§ß§à§Þ §ß§Ö §ß§Ñ§Û§Õ§Ö§ß§Ñ" });

            return NoContent();
        }

        [HttpGet("check/{carId:int}")]
        [UserMod]
        public IActionResult CheckFavorite(int carId)
        {
            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            var favBl = _bl.GetFavoriteBL();
            var isFav = favBl.IsFavorite(currentUser.Id, carId);

            return Ok(new { isFavorite = isFav });
        }
    }

    public class FavoriteRequest
    {
        public int CarId { get; set; }
    }
}
