namespace HighwayToHell
{
    public interface INotification
    {
        void Send(Topic topic);
    }

    public class MailNotification
        : INotification
    {
        public void Send(Topic topic)
        {
            Console.WriteLine($"{topic.Message} konusu için email bildirimi.");
        }
    }

    public class SystemNotification
        : INotification
    {
        public void Send(Topic topic)
        {
            Console.WriteLine($"{topic.Message} işletim sistemi kayıtlarına gönderiliyor");
        }
    }

    public class NotifyService
    {
        private readonly INotification _notification;
        public NotifyService(INotification notification)
        {
            _notification = notification;
        }

        public void Notify(Topic topic)
        {
            _notification.Send(topic);
        }
    }
}
