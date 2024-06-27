namespace ABC.DTOs;

public class RequestClientAddDTO
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IdAddress { get; set; }
    public string ClientType { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string PESEL { get; set; } 
    public string CompanyName { get; set; } 
    public string KRS { get; set; } 
}