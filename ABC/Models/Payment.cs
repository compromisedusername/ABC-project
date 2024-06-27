using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC.Models;

public class Payment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [DataType("decimal")]
    [Precision(19,4)]
    public decimal MoneyAmount { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    
    public int IdContract { get; set; }
    public int IdClient { get; set; }
    
    [ForeignKey(nameof(IdContract))]
    public Contract Contract { get; set; }
    
    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; }
    
}