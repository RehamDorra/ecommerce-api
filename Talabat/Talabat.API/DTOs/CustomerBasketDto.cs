using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.API.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasktetItemDto> Items { get; set; }
    }
}
