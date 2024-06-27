using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Models;

[Table("ClientNatural")]

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