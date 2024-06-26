using System.ComponentModel.DataAnnotations;
using Azure.Core;

namespace ABC.Models;

public class Discount
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}