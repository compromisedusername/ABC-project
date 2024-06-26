using System.ComponentModel.DataAnnotations;

namespace ABC.Models;

public abstract class Client
{
    [Key]
    public int Id { get; set; }
    
    [Required] 
    [EmailAddress]
    public string Email { get; set; }
    
    [Required] 
    public string Address { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
}