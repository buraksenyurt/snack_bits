# If Challenge

C# kodlarından oluşan legacy yığınlarına bakarken kod kalitesini artırmak için sıklıkla yaptığımız işlerden birisi de switch ve if bloklarını ortadan kaldırmaktır. Bu sayede bir fonksiyonun olası Cognitive Complexity değerini azaltmak mümkün hale gelir. Kod okunurluğu artar, sorumluluklar daha anlaşılır bir hal alır. Bu çalışmada ele almak istediğim örnekse aşağıdaki gibi. 

Kodun ilk sürümü.

```csharp
using InternalDomain;
using ExternalDomain;

var partyID = new IdentityType
{
    SchemaName = "TAX_NUMBER",
    Value = "123456789"
};
var header = new InvoiceHeader
{
    Id = 1,
    PurchaseDate = DateTime.Now,
    Title = "Zamzong Wide Screen Monitor 32 inch"
};

// Aşağıdaki if kullanımını terk etmek istiyorum
if (partyID.SchemaName == ContextConstants.IdentityType.CitizenNumber)
{
    header.SupplierCitizenNumber = partyID.Value;
}
if (partyID.SchemaName == ContextConstants.IdentityType.TaxNumber)
{
    header.SupplierTaxNumber = partyID.Value;
}
if (partyID.SchemaName == ContextConstants.IdentityType.CustomerNumber)
{
    header.SupplierCustomerNumber = partyID.Value;
}
if (partyID.SchemaName == ContextConstants.IdentityType.PhoneNumber)
{
    header.SupplierPhoneNumber = partyID.Value;
}
if (partyID.SchemaName == ContextConstants.IdentityType.BranchNumber)
{
    header.SupplierBranchNumber = partyID.Value;
}
if (partyID.SchemaName == ContextConstants.IdentityType.SubscriberNo)
{
    header.SupplierTaxNumber = partyID.Value;
}
if (partyID.SchemaName == ContextConstants.IdentityType.PassaportNumber)
{
    header.SupplierPassportNumber = partyID.Value;
}
if (partyID.SchemaName == ContextConstants.IdentityType.FileNumber)
{
    header.SupplierFileNumber = partyID.Value;
}
Console.WriteLine($"Header Supplier Tax Number Value {header.SupplierTaxNumber}");

namespace InternalDomain
{
    public class InvoiceHeader
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Title { get; set; }
        public string SupplierFileNumber { get; set; }
        public string SupplierPassportNumber { get; set; }
        public string SupplierSubscriberNo { get; set; }
        public string SupplierBranchNumber { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public string SupplierCustomerNumber { get; set; }
        public string SupplierTaxNumber { get; set; }
        public string SupplierCitizenNumber { get; set; }
    }

    public static class ContextConstants
    {
        public static class IdentityType
        {
            public static readonly string TaxNumber = "TAX_NUMBER";
            public static readonly string CitizenNumber = "CITIZEN_NUMBER";
            public static readonly string CustomerNumber = "CUSTOMER_NUMBER";
            public static readonly string PhoneNumber = "PHONE_NUMBER";
            public static readonly string BranchNumber = "BRANCH_NUMBER";
            public static readonly string SubscriberNo = "SUBSCRIBER_NO";
            public static readonly string FileNumber = "FILE_NUMBER";
            public static readonly string PassaportNumber = "PASAPORT_NUMBER";
        }
    }
}

namespace ExternalDomain
{
    // Dış kaynaktan serileşerek gelen veriler için
    public class IdentityType
    {
        public string SchemaName { get; set; }
        public string Value { get; set; }
    }
}
```

Güya harici bir domain'den örneğin bir servis üstünden program çalışma zamanına bir nesne iniyor. Nesne kendi çalışma ortamımızda Deserialize olunca IdentityType şeklinde bir sınıf örneğine dönüşmekte. Bu nesnenin özelliği bir kimlik türünü ve değerini ifade etmesi. Her iki bilgi de string olarak akmakta. Bunu değiştirme gücümüz yok çünkü dış sistem bize bu şekilde veriyor. Kendi sistemimizde ise bu nesneden gelen kimlik türüne bakıp bir başka nesnenin aynı isimli özelliğine yine dış nesneden gelen değeri atıyoruz. Akla gelen ilk yol tabii ki kimlik tipini belli sabit değerlerle karşılaştırmak ve ona göre sınıfın ilgili özelliğine ulaşıp gelen Value bilgisini aktarmak. Örnekte dış sistem bize TAX_NUMBER bilgisini ve değerini göndermekte. Buna göre InvoiceHeader sınıfına ait nesne örneğindeki SupplierTaxNumber özelliğinin gelen Value bilgisi ile beslenmesi gerekiyor. Neyse ki dış sistemden gelen kimlik türleri standart.

Sorumuz yukarıdaki örnekte yer alan if bloklarından nasıl kurtulabileceğimiz. Ya da en azından benim yapmak istediğim şey bu.

```csharp
using InternalDomain;
using ExternalDomain;

var partyID = new IdentityType
{
    SchemaName = "TAX_NUMBER",
    Value = "123456789"
};
var header = new InvoiceHeader
{
    Id = 1,
    PurchaseDate = DateTime.Now,
    Title = "Zamzong Wide Screen Monitor 32 inch"
};

var propMap = new Dictionary<string, string>(){
        { ContextConstants.IdentityType.CitizenNumber, "SupplierCitizenNumber" },
        { ContextConstants.IdentityType.TaxNumber, "SupplierTaxNumber"},
        { ContextConstants.IdentityType.CustomerNumber, "SupplierCustomerNumber"},
        { ContextConstants.IdentityType.PhoneNumber, "SupplierPhoneNumber"},
        { ContextConstants.IdentityType.BranchNumber, "SupplierBranchNumber"},
        { ContextConstants.IdentityType.SubscriberNo, "SupplierSubscriberNo"},
        { ContextConstants.IdentityType.PassaportNumber, "SupplierPassportNumber"},
        { ContextConstants.IdentityType.FileNumber, "SupplierFileNumber"},
    };

var propertyName = propMap[partyID.SchemaName];
var property = header.GetType().GetProperty(propertyName);
if (property != null)
{
    property.SetValue(header, partyID.Value);
    var pValue = property.GetValue(header, null);
    Console.WriteLine($"Property Name {property.Name}\nProperty Value {pValue}");
}

namespace InternalDomain
{
    public class InvoiceHeader
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Title { get; set; }
        public string SupplierFileNumber { get; set; }
        public string SupplierPassportNumber { get; set; }
        public string SupplierSubscriberNo { get; set; }
        public string SupplierBranchNumber { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public string SupplierCustomerNumber { get; set; }
        public string SupplierTaxNumber { get; set; }
        public string SupplierCitizenNumber { get; set; }
    }

    public static class ContextConstants
    {
        public static class IdentityType
        {
            public static readonly string TaxNumber = "TAX_NUMBER";
            public static readonly string CitizenNumber = "CITIZEN_NUMBER";
            public static readonly string CustomerNumber = "CUSTOMER_NUMBER";
            public static readonly string PhoneNumber = "PHONE_NUMBER";
            public static readonly string BranchNumber = "BRANCH_NUMBER";
            public static readonly string SubscriberNo = "SUBSCRIBER_NO";
            public static readonly string FileNumber = "FILE_NUMBER";
            public static readonly string PassaportNumber = "PASAPORT_NUMBER";
        }
    }
}

namespace ExternalDomain
{
    // Dış kaynaktan serileşerek gelen veriler için
    public class IdentityType
    {
        public string SchemaName { get; set; }
        public string Value { get; set; }
    }
}
```

Sonuçları tartışmak gerekebilir elbette. Bu çözümde kimlik türlerine karşılık gelen özellik adları bir dictionary'de tutulur. Gelen kimlik tipine karşılık gelen özellik adı tespit edildikten sonra reflection nimetlerinden yararlanılarak InvoiceHeader nesne örneğinde karşılık gelen özelliğin değeri değiştirilir.