using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Models;

[Table("Address")]
public class Address
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(60)]
    public string City { get; set; }
    [Required]
    [MaxLength(60)]
    public string Country { get; set; }
    [Required]
    [MaxLength(60)]
    public string Street { get; set; }
    [Required]
    [MaxLength(20)]
    public string HouseNumber { get; set; }

    public ICollection<Client> Clients { get; set; } = new HashSet<Client>();
}