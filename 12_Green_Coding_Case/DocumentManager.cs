using System.Data;
using MarineCorp.Erp.Common;
using MarineCorp.Erp.Model;
using MarineCorp.Erp.Utility;

namespace MarineCorp.Erp.Business;

public class DocumentManager
{
    internal ReturnCode GetDocumentByNumber(string? documentNumber, ref byte[]? documentContent, ref DocumentFormatTypes fileType)
    {
        // Doküman numarasına göre döküman arama işlemini gerçekleştirir
        // Bu arama işlemi Döküman Yönetim Sunucusundan bir servis çağrısı araclığıyla yapılır.
        Console.WriteLine($"https://marincorp.docmngr/api/docs servisinden {documentNumber} nolu döküman aranır");
        documentContent = new byte[1024];
        fileType = DocumentFormatTypes.PDF;
        return ReturnCode.Success;
    }

    public DataTable Search(ChargeSearch chargeSearch)
    {
        // Veritabanından işlem yapılması istenen doküman listesini çeker
        Console.WriteLine("Veritabanından işlem yapılacak döküman listesi");

        var dataTable = new DataTable();
        dataTable.Columns.Add("document_number", typeof(string));
        var dataRow = dataTable.NewRow();
        dataRow["charge_number"] = "DOC-TMN-1024";
        dataTable.Rows.Add(dataRow);
        dataRow = dataTable.NewRow();
        dataRow["charge_number"] = "DOC-TMN-1893";
        dataTable.Rows.Add(dataRow);
        
        return new DataTable();
    }
}