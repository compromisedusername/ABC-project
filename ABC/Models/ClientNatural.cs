using System.ComponentModel.DataAnnotations;

namespace ABC.Models;

public class ClientNatural : Client
{
    [Required]
    [MaxLength(50)]
    public string FristName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    [Required]
    [MinLength(9)]
    [MaxLength(9)]
    public string PESEL { get; set; }
    
}