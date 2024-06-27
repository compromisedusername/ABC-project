using System.ComponentModel.DataAnnotations;

namespace ABC.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }

    private IEnumerable<SoftwareSystem> SoftwareSystems { get; set; } = new HashSet<SoftwareSystem>();

}