using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Models;

public class SoftwareSystem
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    [MaxLength(200)]
    public string VersionInformation { get; set; }
    
    public int IdCategory { get; set; }
    [ForeignKey(nameof(IdCategory))]
    public Category Category { get; set; }


    public IEnumerable<Contract> Contracts { get; set; } = new HashSet<Contract>();




}