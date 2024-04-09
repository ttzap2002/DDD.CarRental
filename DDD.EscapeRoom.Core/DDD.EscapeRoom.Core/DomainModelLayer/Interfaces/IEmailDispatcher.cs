using System.Net.Mail;
using System.Text;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Interfaces
{
    public interface IEmailDispatcher
    {
        void Send(MailMessage mailMessage);
    }
}
