using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DDD.EscapeRoom.Core.InfrastructureLayer
{
    public class EmailDispatcher : IEmailDispatcher
    {
        public void Send(MailMessage mailMessage)
        {
            // put email sending code here ...
        }
    }
}
