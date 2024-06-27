using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Models;

[Table("ClientCompany")]

public class ClientCompany : Client
{
    
    [Required] 
    public string CompanyName { get; set; }
    
    [Required]
    [MinLength(9)]
    [MaxLength(14)]
    public string KRS { get; set; }
}