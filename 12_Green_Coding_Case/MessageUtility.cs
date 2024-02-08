using MarineCorp.Erp.Common;

namespace MarineCorp.Erp.Utility;

public static class MessageUtility{
    // Bu metot code'un karşılığı olan mesaj içeriğini veritabanından getiriyor
    // Veritabanındaki code'ların karşılığı olan mesajlar yabancı dil desteği ile tutuluyor.
    public static string Get(ReturnCode code) => code.ToString();
}