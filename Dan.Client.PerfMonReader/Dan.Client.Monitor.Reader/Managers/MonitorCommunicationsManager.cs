//using System;
//using System.Configuration;
//using System.Globalization;
//using System.Runtime.Remoting.Messaging;
//using ServiceBusClient;


//namespace Monitor.Common
//{
//    public class MessageManager
//    {
//        private readonly IMessageService _client;

//        public MessageManager(IMessageService client)
//        {
//            _client = client;
//        }


//        public bool SendMessage()
//        {
//            try
//            {
//                _client.Publish();
//            }
//            catch (Exception)
//            {
                
//                throw;
//            }
          
//        }
//    }




//}
