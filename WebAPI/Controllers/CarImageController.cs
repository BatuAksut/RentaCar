using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImageController : ControllerBase

    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CarImageController(MyDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(int carId, IFormFile file)
        {
            if (_context.CarImages.Count(c => c.CarId == carId) >= 5)
            {
                return BadRequest("A car can have a maximum of 5 images.");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var guid = Guid.NewGuid().ToString();
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", $"{guid}{Path.GetExtension(file.FileName)}");

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var carImage = new CarImage
            {
                CarId = carId,
                ImagePath = path,
                Date = DateTime.UtcNow
            };

            _context.CarImages.Add(carImage);
            await _context.SaveChangesAsync();

            return Ok(carImage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var carImage = await _context.CarImages.FindAsync(id);
            if (carImage == null)
            {
                return NotFound();
            }

            var path = carImage.ImagePath;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.CarImages.Remove(carImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            var carImage = await _context.CarImages.FindAsync(id);
            if (carImage == null)
            {
                return NotFound();
            }

            var path = carImage.ImagePath;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var guid = Guid.NewGuid().ToString();
            var newPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", $"{guid}{Path.GetExtension(file.FileName)}");

            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            carImage.ImagePath = newPath;
            carImage.Date = DateTime.UtcNow;

            _context.CarImages.Update(carImage);
            await _context.SaveChangesAsync();

            return Ok(carImage);
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetImages(int carId)
        {
            var carImages = await _context.CarImages.Where(c => c.CarId == carId).ToListAsync();

            if (carImages == null || !carImages.Any())
            {
                var defaultImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "default.png");
                return Ok(new List<string> { defaultImagePath });
            }

            return Ok(carImages.Select(c => c.ImagePath));
        }
    }
}
