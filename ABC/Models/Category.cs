using System.ComponentModel.DataAnnotations;

namespace ABC.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [MaxLength(200)]
    public int Description { get; set; }
    [MaxLength(50)]
    public int Name { get; set; }
}