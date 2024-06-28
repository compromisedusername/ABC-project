using System.ComponentModel.DataAnnotations;

namespace ABC.DTOs;

public class RequestContractCreateDto
{
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int SoftwareSystemId { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [Required]
    public int SupportUpdatePeriodInYears { get; set; }
    public int DiscountId { get; set; }
}