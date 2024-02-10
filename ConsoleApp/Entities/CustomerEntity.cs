using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Entities;

internal class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Email { get; set; } = null!;

    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!; //Foreign Key

    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!; //Foreign Key
}
