using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Models;

[Table("Category")]

public class Category
{
    [Key]
    public int Id { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }

    private ICollection<SoftwareSystem> SoftwareSystems { get; set; } = new HashSet<SoftwareSystem>();

}