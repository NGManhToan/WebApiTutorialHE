using WebApiTutorialHE.Models.Mail;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IMailService
    {
        Task<bool> SendMail(MailData mailData, CancellationToken ct);
    }
}
