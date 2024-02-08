
namespace MarineCorp.Erp.Model;

public class ChargeSearch
{
    public int CustomerId { get; internal set; }
    public short ModuleId { get; internal set; }
    public short DocumentTypeId { get; internal set; }
    public string DocumentNumber { get; internal set; }
    public DateTime DocumentDateStart { get; internal set; }
    public DateTime DocumentDateEnd { get; internal set; }
    public int UserId { get; internal set; }
}