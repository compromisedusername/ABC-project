using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC.Models;

[Table("Contarcts_SoftwareSystems")]
[PrimaryKey(nameof(IdContract),nameof(IdSoftwareSystem))]
public class ContractsSoftwareSystems
{
    
    public int? IdContract { get; set; }
    public int? IdSoftwareSystem { get; set; }
    
    [ForeignKey(nameof(IdContract))]
    public Contract? Contract { get; set; }
    [ForeignKey(nameof(IdSoftwareSystem))]
    public SoftwareSystem? SoftwareSystem { get; set; }
}