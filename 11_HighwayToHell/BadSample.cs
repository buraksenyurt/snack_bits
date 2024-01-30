namespace HighwayToHell
{
    class EmailNotification
    {
        public void Send(Topic topic)
        {
            Console.WriteLine($"{topic.Message} konusu için email bildirimi.");
        }
    }

    class NotificationService
    {
        private readonly EmailNotification _emailNotification;
        public NotificationService()
        {
            _emailNotification = new EmailNotification();
        }
        public void Notify(Topic topic)
        {
            _emailNotification.Send(topic);
        }
    }
}
