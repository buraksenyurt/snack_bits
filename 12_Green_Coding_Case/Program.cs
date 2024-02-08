using MarineCorp.Erp.Business;
using MarineCorp.Erp.Model;

namespace MarineCorp.Presentation.Web;

class Program
{
    static void Main(string[] args)
    {
        ChargeManager chargeManager = new ChargeManager();
        Context context = new Context();
        context.Add("chargeDetailId", 283);
        context.Add("searchType", 5);
        context.Add("customerId", 1902);
        context.Add("moduleId", 9);
        context.Add("documentTypeId", 1);
        context.Add("userId", 4);
        string errorMessage = string.Empty;
        var content = chargeManager.PrintAllDocument(context, ref errorMessage);
        Console.WriteLine($"{errorMessage}");
        if (content.Length > 1024 * 1024 * 10)
        {
            chargeManager.SendFtp(content);
        }
        else
        {
            chargeManager.SendMail(content, "jhon.doe@marinecorp.comm");
        }
    }
}
