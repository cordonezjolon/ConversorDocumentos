using MS_ConvertidorDocumentos.Models;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace MS_ConvertidorDocumentos.BusinessLogic
{
    public class blCorreoElectronico
    {
        appConfig _configuracion = new appConfig();
        public bool correoEnviado = false;
        public bool enviarCorreoElectronico(Email correo)
        {
            bool _respuesta = true;

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_configuracion.userCorreo);
                message.To.Add(correo.destinatario);
                message.Subject = correo.asunto;
                message.SubjectEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Body = correo.cuerpo;
                message.Priority = MailPriority.High;
                message.BodyEncoding = Encoding.UTF8;
                smtp.Port = Convert.ToInt32(_configuracion.puertoCorreo);
                smtp.Host = _configuracion.hostCorreo;

                //Valida si servidor necesita habilitar SSL en la comunicacion, valor disponible para configuracion en WebConfig
                if (_configuracion.sslCorreo.Equals("1"))
                {
                    smtp.EnableSsl = true;
                }
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_configuracion.userCorreo, _configuracion.passwordCorreo);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(message);
            }
            catch (Exception)
            {
                _respuesta = false;
                return _respuesta;
            }
            return _respuesta;
        }
        public bool crearCorreoAlerta(Archivo archivo, string metodoEjecutado, string msjException)
        {
            bool _respuesta = false;
            try
            {
                if (correoEnviado == false)
                {

                    string cuerpo = "";
                    cuerpo = string.Concat(cuerpo, "<h2>Error al realizar la conversión en: <u>", metodoEjecutado, ".</u> </h2> ");
                    cuerpo = string.Concat(cuerpo, "<p><b> Descripción del error: </b>", msjException, ".</p>");
                    cuerpo = string.Concat(cuerpo, "<p><b> SERVICIO POST: </b> Convertidor de documentos devExpress<br></p>");
                    cuerpo = string.Concat(cuerpo, "<p><b> Objeto JSON Enviado: <b><br>", JsonSerializer.Serialize(archivo), "</p><br>");
                    Email correo = new Email();
                    correo.asunto = "CONVERSION DE DOCS FIRMA SGT1";
                    correo.destinatario = _configuracion.destinatariosSoporte;
                    correo.cuerpo = cuerpo;

                    enviarCorreoElectronico(correo);
                    this.correoEnviado = true;
                    _respuesta = true;
                }
            }
            catch (Exception ex)
            {

                _respuesta = false;
            }
            return _respuesta;
        }
    }
}
