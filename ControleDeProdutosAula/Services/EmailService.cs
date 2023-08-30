using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace ControleDeProdutosAula.Services
{
	public class EmailService
	{
		public async Task EnviarEmail()
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
			message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
			message.Subject = "How you doin'?";

			message.Body = new TextPart("plain")
			{
				Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
			};

			using (var client = new SmtpClient())
			{
				client.Connect("smtp.google.com", 587, false);

				// Note: only needed if the SMTP server requires authentication
				client.Authenticate("domtec@gmail.com", "");

				client.Send(message);
				client.Disconnect(true);
			}

		}
	}
}
