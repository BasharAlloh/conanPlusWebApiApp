using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string content);
}

public class SendGridEmailService : IEmailService
{
    private readonly string _apiKey;

    public SendGridEmailService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string content)
    {
        try
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("game.bashar8@gmail.com", "Conan Plus Support");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);

            // تسجيل حالة الاستجابة
            Console.WriteLine($"SendGrid Response Status Code: {response.StatusCode}");

            // SendGrid يعيد حالة مختلفة عن 200OK في بعض الأحيان حتى لو تم بنجاح
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                // تسجيل تفاصيل الخطأ عند حدوث فشل
                var errorResponse = await response.Body.ReadAsStringAsync();
                Console.WriteLine($"Error sending email: {errorResponse}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return false;
        }
    }



}
