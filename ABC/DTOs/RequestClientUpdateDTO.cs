namespace ABC.DTOs;
public class RequestClientUpdateDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IdAddress { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string CompanyName { get; set; } 
}