namespace HighwayToHell
{
    internal class Program
    {
        static void Main()
        {
            var topic = new Topic
            {
                TopicId = 1,
                Source = "Application",
                Message = "Overload memory"
            };

            #region Bad Practice

            var notifyService = new NotificationService();
            notifyService.Notify(topic);

            #endregion

            #region Good Practice

            var notiService = new NotifyService(new SystemNotification());
            notiService.Notify(topic);

            notiService = new NotifyService(new MailNotification());
            notiService.Notify(topic);

            #endregion
        }
    }
}
