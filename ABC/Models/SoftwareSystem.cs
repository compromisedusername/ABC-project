using System.ComponentModel.DataAnnotations;

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
    public Category Category { get; set; }
}