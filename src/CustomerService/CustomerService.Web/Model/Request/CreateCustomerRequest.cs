namespace CustomerService.Web.Model.Request;

public class CreateCustomerRequest
{
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public int Score { get; set; }
}