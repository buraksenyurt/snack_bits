namespace Paranoid
{
    internal class Program
    {
        static void Main()
        {
            var generics = new RequestForChange
            {
                Id = 1,
                Title = "Tip Sisteminde Generic Mimari Gereksinimi",
                Content = "Programlama dilimizin bu versiyonunda generic tür desteği bulunmuyor. Bu destek ile alakalı olarak aşağıdaki spec'lerin..."
            };
            #region Bad Practice

            var thomas = new Editor();
            var lisa = new Subscriber();
            thomas.Add(generics);
            thomas.Vote(generics);
            lisa.Vote(generics);
            lisa.Add(new RequestForChange
            {
                Id = 2,
                Title = "SSL Güvenlik Riskleri",
                Content = "Güncel siber güvenlik tehditleri kapsamında sistemde kullanılan SSL sertifikalarının..."
            });

            #endregion

            #region Refactored Practice

            var belinda = new Author();
            var stark = new User();
            belinda.Add(generics);
            belinda.Update(generics);
            stark.Vote(generics);

            #endregion
        }
    }
}
