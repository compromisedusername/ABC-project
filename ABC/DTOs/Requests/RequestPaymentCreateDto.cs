using System.ComponentModel.DataAnnotations;

namespace ABC.DTOs;

public class RequestPaymentCreateDto
{
    [Required]
    public int ContractId { get; set; }
    [Required]
    public int ClientId { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime PaymentDate { get; set; }
}