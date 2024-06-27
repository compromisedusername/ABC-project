using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Models;

public abstract class Client
{
    [Key]
    public int Id { get; set; }
    
    [Required] 
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
    public int IdAddress { get; set; }
    [ForeignKey(nameof(IdAddress))]
    public Address Address { get; set; }
    
    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

    
}