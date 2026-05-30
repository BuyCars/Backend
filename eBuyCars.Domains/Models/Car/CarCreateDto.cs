using System.ComponentModel.DataAnnotations;

namespace eBuyCars.Domain.Models.Car
{
    public class CarCreateDto
    {
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand is required")]
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "Invalid year")]
        public int Year { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Mileage cannot be negative")]
        public int Mileage { get; set; }

        [StringLength(50)]
        public string Fuel { get; set; } = string.Empty;

        [StringLength(50)]
        public string Transmission { get; set; } = string.Empty;

        [StringLength(20)]
        public string Condition { get; set; } = "used";
    }
}
