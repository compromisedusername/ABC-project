using System.ComponentModel.DataAnnotations;

namespace ABC.DTOs;
public class RequestClientUpdateDto
{
    [Required]
    public int IdClient { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Required]
    public int IdAddress { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } 
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } 
    [MaxLength(50)]
    public string CompanyName { get; set; } 
}