using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Models;

public class ClientCompany : Client
{
    
    [Required] 
    public string CompanyName { get; set; }
    
    [Required]
    [MinLength(9)]
    [MaxLength(14)]
    public string KRS { get; set; }
}