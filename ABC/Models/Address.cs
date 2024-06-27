using System.ComponentModel.DataAnnotations;

namespace ABC.Models;

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