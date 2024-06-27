using System.ComponentModel.DataAnnotations;
using Azure.Core;

namespace ABC.Models;

public class Discount
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [Range(0,100)]
    public int Value { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateFrom { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateTo { get; set; }
    
}