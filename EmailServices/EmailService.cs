using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace SCANHUB___INVENTARIO_Y_CAJA.EmailServices
{
    internal class EmailService
    {
        private string smtpServer = "smtp.gmail.com"; // Servidor SMTP de Gmail
        private int smtpPort = 587; // Puerto SMTP de Gmail
        private string emailFrom = "noreply.scanhub@gmail.com"; // Reemplaza con tu correo de Gmail
        private string password = "jdmp eaee gxct muyl"; // Reemplaza con la contraseña o App Password de Gmail

        public void SendEmail(string emailTo, string subject, string messageBody)
        {
            try
            {
                // Crear un nuevo mensaje de correo
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("SCANHUB", emailFrom)); // Remitente
                message.To.Add(new MailboxAddress("", emailTo)); // Destinatario
                message.Subject = subject; // Asunto del correo

                // Crear el cuerpo del mensaje
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = messageBody;
                message.Body = bodyBuilder.ToMessageBody();

                // Configurar el cliente SMTP
                using (var client = new SmtpClient())
                {
                    client.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

                    // Autenticar con las credenciales de Gmail
                    client.Authenticate(emailFrom, password);

                    // Enviar el correo
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }
    }
}
