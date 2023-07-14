using BusinessLib.Constant;

namespace BusinessLib.Data;

public class Customer
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Region { get; set; }
    public CustomerType CustomerType { get; set; }
}