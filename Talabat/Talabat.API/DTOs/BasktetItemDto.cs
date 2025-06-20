﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs
{
    public class BasktetItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range (1 , double.MaxValue , ErrorMessage = "price must be greater than zero")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}