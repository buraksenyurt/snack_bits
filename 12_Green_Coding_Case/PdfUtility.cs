namespace MarineCorp.Erp.Utility;

public class PdfDocument
{
    private readonly List<PdfPage> _pdfs;
    public PdfDocument()
    {
        _pdfs = new List<PdfPage>();
    }
    public void AddPage(PdfPage page)
    {
        _pdfs.Add(page);
    }

    public Pdf Open(MemoryStream memoryStream, PdfDocumentOpenMode import)
    {
        Console.WriteLine("PDF Üretimi gerçekleştiriliyor");
        // Parametre olarak gelen memoryStream ve enum değerini kullanarak bir Pdf üretir
        return new Pdf();
    }

    public void Save(MemoryStream stream, bool saveCriteria)
    {
        Console.WriteLine("Gelen memory stream içeriği PDF'e kaydediliyor");
    }
}

public enum DocumentFormatTypes
{
    PDF
}

public enum PdfDocumentOpenMode
{
    Import
}

public class PdfPage
{
    public byte[] Content { get; set; }
}

public class Pdf
{
    public List<PdfPage> Pages { get; set; }
}