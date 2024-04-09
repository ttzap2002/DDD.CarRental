using DDD.EscapeRoom.Core.DomainModelLayer.Events;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DDD.EscapeRoom.Core.ApplicationLayer.DomainEventListeners
{
    public class SendEmailWhenPlayerCreatedDomainEventHandler : IEventHandler<PlayerCreatedDomainEvent>
    {
        private IEmailDispatcher _emailDispatcher;
        public SendEmailWhenPlayerCreatedDomainEventHandler(IEmailDispatcher emailDispatcher)
        {
            this._emailDispatcher = emailDispatcher;
        }

        public void Handle(PlayerCreatedDomainEvent eventData)
        {
            string from = "EscapeRoom@gmail.com";
            string to = eventData.Email;
            string subject = "New player created...";
            string body = "Activate player...";
            MailMessage mailMessage = new MailMessage(from, to, subject, body);

            // wywołuje serwis z warstwy infrastruktury
            this._emailDispatcher.Send(mailMessage);
        }
    }
}
