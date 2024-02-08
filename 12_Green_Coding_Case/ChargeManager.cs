using System.Data;
using System.Text;
using MarineCorp.Erp.Model;
using MarineCorp.Erp.Common;
using MarineCorp.Erp.Utility;

namespace MarineCorp.Erp.Business;

public class ChargeManager
{
    public virtual byte[] PrintAllDocument(Context context, ref string errorMessage)
    {
        int chargeDetailId = Convert.ToInt32(context.Get("chargeDetailId"));
        short searchType = Convert.ToInt16(context.Get("searchType"));
        int firmId = Convert.ToInt32(context.Get("firmId"));
        short moduleId = Convert.ToInt16(context.Get("moduleId"));
        short documentTypeId = Convert.ToInt16(context.Get("documentTypeId"));
        int userId = Convert.ToInt32(context.Get("userId"));
        var failReport = new StringBuilder();

        var chargeDetail = new ChargePlanDetail()
        {
            Id = chargeDetailId,
            SearchType = searchType
        };
        var dataTable = GetChargePlanDetails(chargeDetail);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            errorMessage = MessageUtility.Get(ReturnCode.MainAccountNotFound);
            return null;
        }

        var mainPdfDocument = new PdfDocument();
        var fileType = DocumentFormatTypes.PDF;

        var documentManager = new DocumentManager();
        foreach (DataRow row in dataTable.Rows)
        {
            var chargeNumber = row["charge_number"].ToString();
            var chargeDate = Convert.ToDateTime(row["charge_date"].ToString());

            if (string.IsNullOrEmpty(chargeNumber) || chargeDate == DateTime.MinValue)
            {
                failReport.Append($"{chargeNumber} - ");
                continue;
            }
            var document = new ChargeSearch
            {
                CustomerId = firmId,
                ModuleId = moduleId,
                DocumentTypeId = documentTypeId,
                DocumentNumber = chargeNumber,
                DocumentDateStart = chargeDate,
                DocumentDateEnd = chargeDate,
                UserId = userId,
            };
            DataTable chargeDataTable = documentManager.Search(document);
            if (chargeDataTable == null || chargeDataTable.Rows.Count > 1)
            {
                failReport.Append($"{chargeNumber} - ");
                continue;
            }

            var documentNumber = chargeDataTable.Rows[0]["document_number"].ToString();
            byte[] documentContent = null;
            ReturnCode retCode = documentManager.GetDocumentByNumber(documentNumber, ref documentContent, ref fileType);
            if (retCode != ReturnCode.Success)
            {
                failReport.Append($"{chargeNumber} - ");
            }

            Pdf pdf = mainPdfDocument.Open(new MemoryStream(documentContent), PdfDocumentOpenMode.Import);
            foreach (PdfPage page in pdf.Pages)
            {
                mainPdfDocument.AddPage(page);
            }
        }

        if (!string.IsNullOrEmpty(failReport.ToString()))
        {
            errorMessage = failReport.Append(MessageUtility.Get(ReturnCode.NumberedDocumentNotFound)).ToString();
            return null;
        }

        MemoryStream stream = new MemoryStream();
        mainPdfDocument.Save(stream, false);
        return stream.ToArray();
    }

    private DataTable GetChargePlanDetails(ChargePlanDetail chargePlanDetail)
    {
        Console.WriteLine("Teminat formları listesi çekilir");
        // Plana ait müşteri hesap detaylarını DataTable olarak döndürür

        var dataTable = new DataTable();
        dataTable.Columns.Add("charge_number", typeof(int));
        dataTable.Columns.Add("charge_date", typeof(DateTime));
        var dataRow = dataTable.NewRow();
        dataRow["charge_number"] = 1001;
        dataRow["charge_date"] = DateTime.Now.AddDays(-10);
        dataTable.Rows.Add(dataRow);
        dataRow = dataTable.NewRow();
        dataRow["charge_number"] = 1005;
        dataRow["charge_date"] = DateTime.Now.AddDays(-5);
        dataTable.Rows.Add(dataRow);

        return new DataTable();
    }
}