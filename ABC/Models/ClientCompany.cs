using System.ComponentModel.DataAnnotations;

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