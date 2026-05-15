using BuyCars.Api.Filters;
using BuyCars.BusinessLogic;
using BuyCars.Domain.Entities.User;
using BuyCars.Domain.Models.Car;
using Microsoft.AspNetCore.Mvc;

namespace BuyCars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly BussinesLogic _bl = new();

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] string? category,
            [FromQuery] string? condition,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? search)
        {
            var carBl = _bl.GetCarBL();
            var cars = carBl.GetAllCars(category, condition, minPrice, maxPrice, search);
            return Ok(cars);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var carBl = _bl.GetCarBL();
            var car = carBl.GetCarById(id);

            if (car == null)
                return NotFound(new { message = $"§¡§Ó§ä§à§Þ§à§Ò§Ú§Ý§î §ã ID {id} §ß§Ö §ß§Ñ§Û§Õ§Ö§ß" });

            return Ok(car);
        }

        [UserMod]
        public IActionResult Create([FromBody] CarCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            var carBl = _bl.GetCarBL();
            var car = carBl.CreateCar(dto, currentUser.Id);

            return Created($"/api/car/{car.Id}", car);
        }

        [HttpPut("{id:int}")]
        [UserMod]
        public IActionResult Update(int id, [FromBody] CarCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            var carBl = _bl.GetCarBL();
            var updated = carBl.UpdateCar(id, dto, currentUser.Id, currentUser.Role);

            if (updated == null)
                return NotFound(new { message = "§°§Ò§ì§ñ§Ó§Ý§Ö§ß§Ú§Ö §ß§Ö §ß§Ñ§Û§Õ§Ö§ß§à §Ú§Ý§Ú §ß§Ö§ä §á§â§Ñ§Ó §ß§Ñ §â§Ö§Õ§Ñ§Ü§ä§Ú§â§à§Ó§Ñ§ß§Ú§Ö" });

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [UserMod]
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            var carBl = _bl.GetCarBL();
            var deleted = carBl.DeleteCar(id, currentUser.Id, currentUser.Role);

            if (!deleted)
                return NotFound(new { message = "§°§Ò§ì§ñ§Ó§Ý§Ö§ß§Ú§Ö §ß§Ö §ß§Ñ§Û§Õ§Ö§ß§à §Ú§Ý§Ú §ß§Ö§ä §á§â§Ñ§Ó §ß§Ñ §å§Õ§Ñ§Ý§Ö§ß§Ú§Ö" });

            return NoContent();
        }
    }
}
