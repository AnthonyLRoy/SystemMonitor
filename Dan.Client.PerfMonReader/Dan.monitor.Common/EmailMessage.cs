using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan.monitor.Common
{
    public class EmailMessage
    {

        public EmailMessage()
        {
            
        }

        // command seperated list of Contact email addresses
        public string EmailAccounts{ get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        public string From { get; set; }

    }


    public class EmailItem
    {
        public string EmailAddress { get; set; }

    }
}
