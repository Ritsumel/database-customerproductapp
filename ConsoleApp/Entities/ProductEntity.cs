using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp.Entities;

internal class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!; //Foreign Key
}
