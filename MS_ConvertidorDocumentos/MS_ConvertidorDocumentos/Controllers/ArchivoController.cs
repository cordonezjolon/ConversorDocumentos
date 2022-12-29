using Microsoft.AspNetCore.Mvc;
using MS_ConvertidorDocumentos.BusinessLogic;
using MS_ConvertidorDocumentos.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MS_ConvertidorDocumentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        blArchivo _archivo = new blArchivo();
        [Route("Convert_Doc")]
        [HttpPost]
        public IActionResult Convert_Doc([FromBody] Archivo archivo)
        {
            OutputJson _respuesta = new OutputJson();
            try
            {
                var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),"data");
                if (_archivo.validaExisteArchivo(archivo))
                {

                    

                    String _extencion = _archivo.getExtension(archivo.input).ToUpper();
                    if (string.IsNullOrEmpty(archivo.id_tipo_Doc) || System.DBNull.Value.Equals(archivo.id_tipo_Doc))
                    {

                        if (!_extencion.Equals("PDF"))
                        {
                            //Convertir Normal DevExpress
                            _respuesta = _archivo.convertirDocumentoPDF(archivo);

                        }
                        else
                        {
                            _respuesta.result = "OK";
                            _respuesta.id = 1;
                            _respuesta.err = "Documento PDF creado exitosamente";
                        }
                    }
                    else if (!_archivo.validarFormatoDocumento(Convert.ToInt32(archivo.id_tipo_Doc)))
                    {
                        if (!_extencion.ToUpper().Equals("PDF"))
                        {
                            //Convertir Normal DevExpress
                            _respuesta = _archivo.convertirDocumentoPDF(archivo);
                        }
                        else
                        {
                            _respuesta.result = "OK";
                            _respuesta.id = 1;
                            _respuesta.err = "Documento PDF creado exitosamente";
                        }
                    }
                    else if (_extencion.Equals("RTF"))
                    {
                        //Conversion de Doc/Docx a RTF y luego PDF en caso de fallo va a interop serice
                        _respuesta = _archivo.ProcesarDocToRTF(archivo);

                    }
                    else if (!_extencion.Equals("PDF"))
                    {
                        //Conversion directa InteropService
                        _respuesta = _archivo.convertFilePDFTools(archivo);
                    }
                    else
                    {
                        _respuesta.result = "OK";
                        _respuesta.id = 1;
                        _respuesta.err = "Documento PDF creado exitosamente";
                    }
                }
                else
                {
                    _respuesta.result = "ERROR";
                    _respuesta.id = -1;
                    _respuesta.err = "No se logro localizar el archivo en la ruta indicada " +  archivo.input;
                }
                return new OkObjectResult(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.id = -1;
                _respuesta.result = "ERROR";
                _respuesta.err = ex.ToString();

                return  new JsonResult(_respuesta)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

            }
            
        }
        [Route("Convert_RTFtoPdf")]
        [HttpPost]
        public IActionResult Convert_RTFtoPdf()
        {
            return new OkObjectResult(new OutputJson { id = 123, result = "Hero" });
        }
    }
}
