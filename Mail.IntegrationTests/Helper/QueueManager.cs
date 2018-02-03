using RabbitMQ.Client;

namespace Mail.IntegrationTests.Helper
{
    public class QueueManager
    {
        private string _host;
        private string _userName;
        private string _pass;

        public QueueManager(string host, string userName, string pass)
        {
            _host = host;
            _userName = userName;
            _pass = pass;
        }

        public void ClearQueue(string name)
        {
            ConnectionFactory factory = new ConnectionFactory();

            factory.HostName = _host;
            factory.UserName = _userName;
            factory.Password = _pass;

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueuePurge(name);
                }
            }
        }
    }
}
