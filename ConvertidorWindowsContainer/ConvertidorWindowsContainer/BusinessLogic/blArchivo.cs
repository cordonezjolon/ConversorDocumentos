using DevExpress.XtraRichEdit;
using MS_ConvertidorDocumentos.Models;
using MS_ConvertidorDocumentos.BusinessLogic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using DevExpress.XtraPrinting;

namespace MS_ConvertidorDocumentos.BusinessLogic
{
    public class blArchivo
    {
        public appConfig _configuracion = new appConfig();
        blCorreoElectronico correo = new blCorreoElectronico();
        //blSeguridad _seguridad;
        public bool flagConversion = false;

        public bool validaExisteArchivo(Archivo _archivo)
        {
            bool _respuesta = false;
            try
            {
                    if (File.Exists(_archivo.input))
                    {
                        File.OpenWrite(_archivo.input).Close();
                        _respuesta = true;
                    }
                return _respuesta;
            }
            catch (Exception ex)
            {
                return _respuesta;
            }
        }
        public string getExtension(String path)
        {
            try
            {
                FileInfo archivo = new FileInfo(path);
                return archivo.Extension;
            }
            catch (Exception)
            {

                return "No se logro obtener la extencion del archivo.";
            }
        }
        public OutputJson ConvertirRtfToPdf(Archivo archivo)
        {
            OutputJson _respuesta = new OutputJson();
            try
            {

                using (RichEditDocumentServer server = new RichEditDocumentServer())
                {
                    try
                    {
                        DevExpress.Utils.AzureCompatibility.Enable = true;
                        DevExpress.XtraRichEdit.RichEditControlCompatibility.UsePrintingSystemPdfExport = true;
                        server.LoadDocument(archivo.input);

                    }
                    catch (Exception ex)
                    {
                        _respuesta.result = "ERROR";
                        _respuesta.id = -1;
                        _respuesta.result = "Ocurrio un problema al cargar el documento.";
                        return _respuesta;
                    }


                    string outFileName = Path.ChangeExtension(archivo.input, "pdf");
                    FileStream fsOut = File.Open(outFileName, FileMode.Create);
                    
                    server.ExportToPdf(fsOut);
                    fsOut.Close();

                    _respuesta.result = "OK";
                    _respuesta.id = 1;
                    _respuesta.err = "Documento PDF creado exitosamente.";
                }

                    
                return _respuesta;
            }
            catch (Exception ex)
            {
                _respuesta.result = "OK";
                _respuesta.id = -1;
                _respuesta.err = ex.ToString();
                return _respuesta; 
            }
        }
       
        public bool convertirDocxToRTF(Archivo archivo)
        {
            bool _respuesta = false;
            try
            {
                RichEditDocumentServer server = new RichEditDocumentServer();
                server.LoadDocument(archivo.input, DocumentFormat.OpenXml);
                server.SaveDocument(archivo.pathRtf, DocumentFormat.Rtf);
                _respuesta = true;
                return _respuesta;
            }
            catch (Exception)
            {
                return _respuesta;
            }
        }
        public OutputJson ConvertirDocxToPdf(Archivo archivo)
        {
            OutputJson _respuesta = new OutputJson();
            try
            {
                string outFileName = Path.ChangeExtension(archivo.input, "rtf");
                archivo.pathRtf = outFileName;
                if (convertirDocxToRTF(archivo))
                {
                    archivo.input = outFileName;
                    _respuesta = ConvertirRtfToPdf(archivo);

                }
                else
                {
                    _respuesta.id = -1;
                    _respuesta.result = "ERROR";
                    _respuesta.err = "Ocurrio un problema al convertir docx a pdf.";
                }
                return _respuesta;
            }
            catch (Exception ex)
            {
                _respuesta.id = -1;
                _respuesta.result = "ERROR";
                _respuesta.err = ex.ToString();
                return _respuesta;
            }
        }
        //public OutputJson convertFilePDFTools(Archivo documento)
        //{
        //    OutputJson _respuesta = new OutputJson();
        //    try
        //    {

        //        WebRequest request = WebRequest.Create(_configuracion.urlTools + "archivos/ConvertFileToPdfImpersonate");
        //        request.Method = "POST";
        //        string postData = JsonConvert.SerializeObject(documento);
        //        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //        request.ContentType = "application/json";
        //        request.ContentLength = byteArray.Length;
        //        Stream dataStream = request.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();
        //        WebResponse response = request.GetResponse();
        //        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        //        dataStream = response.GetResponseStream();
        //        StreamReader reader = new StreamReader(dataStream);

        //        _respuesta = JsonConvert.DeserializeObject<OutputJson>(reader.ReadToEnd());
        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();
        //        if (Convert.ToInt32(_respuesta.id) < 0)
        //        {
        //            //Enviar correo de aviso 
        //            correo.crearCorreoAlerta(documento, "convertFilePDFTools", _respuesta.err);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        correo.crearCorreoAlerta(documento, "convertFilePDFTools", ex.ToString());

        //    }
        //    return _respuesta;

        //}
    }
}
