using WebApiTutorialHE.Models.Mail;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IMailService
    {
        Task<bool> SendMail(MailDataWithAttachments mailData, CancellationToken ct);
        //Task SendWithAttachmentsAsync(MailDataWithAttachments mailData, CancellationToken ct);
    }
}
