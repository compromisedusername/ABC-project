using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC.Models;

[Table("Contract")]

public class Contract
{
    
    [Key]
    public int Id { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateFrom { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateTo { get; set; }
    
    [Required]
    public Boolean IsActive { get; set; }
    
    [DataType("decimal")]
    [Precision(19,4)]
    public decimal Price { get; set; }
    
    [Required]
    public int SupportUpdatePeriodInYears { get; set; }
    
    
    [Required]
    [MaxLength(200)]
    public string UpdateInformation { get; set; }
    
    public int? IdDiscount { get; set; }
    public int IdSoftwareSystem { get; set; }
    public int IdClient { get; set; }

    public Boolean IsSigned { get; set; } = false;
    
    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; }
    [ForeignKey(nameof(IdSoftwareSystem))]
    public SoftwareSystem SoftwareSystem { get; set; }
    [ForeignKey(nameof(IdDiscount))]
    public Discount Discount { get; set; }

    private ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    public ICollection<ContractsSoftwareSystems> ContractsSoftwareSystems { get; set; } = new HashSet<ContractsSoftwareSystems>();





}