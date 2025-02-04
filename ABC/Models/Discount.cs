using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure.Core;

namespace ABC.Models;

[Table("Discount")]
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
    
    private ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();

    
}