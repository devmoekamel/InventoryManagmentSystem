using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryClassLibrary.Models
{
    public class BaseModel
    {
        public  int Id { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ApplicationUser))]
        public string? CreatedBy { get; set; }
        public ApplicationUser ApplicationUser { get; set; } 
    }
}
