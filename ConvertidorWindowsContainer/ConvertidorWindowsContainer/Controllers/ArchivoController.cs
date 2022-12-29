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
            int _tipoError = 0;
            int _estado = StatusCodes.Status200OK;
            try
            {
                if (_archivo.validaExisteArchivo(archivo))
                {
                    String _extencion = _archivo.getExtension(archivo.input).ToUpper();
                    if (!_extencion.Equals("PDF"))
                    {
                        if (_extencion.Equals("RTF"))
                        {
                            _respuesta = _archivo.ConvertirRtfToPdf(archivo);
                        }
                        else if (_extencion.Equals("DOC") || _extencion.Equals("DOCX"))
                        {
                            _respuesta = _archivo.ConvertirDocxToPdf(archivo);
                        }
                        else
                        {
                            _respuesta.result = "ERROR";
                            _respuesta.id = -1;
                            _respuesta.err = "Formato no reconocido. Extensión de archivo " + _extencion ;
                            _estado = StatusCodes.Status500InternalServerError;
                            _tipoError = 1;
                        }
                    }
                    else
                    {
                        _respuesta.result = "OK";
                        _respuesta.id = 1;
                        _respuesta.err = "Documento es PDF.";
                    }
                }
                else
                {
                    _respuesta.result = "ERROR";
                    _respuesta.id = -1;
                    _respuesta.err = "No se logro localizar el archivo en la ruta indicada " +  archivo.input;
                    _estado = StatusCodes.Status404NotFound;
                    _tipoError = 1;
                }
                if(_respuesta.id == 1)
                {
                    return new OkObjectResult(_respuesta);
                }
                else
                {
                    _estado= StatusCodes.Status500InternalServerError;
                    _tipoError = 1;
                    throw new InvalidOperationException("Error de conversion de documento.");
                }
               

               
            }
            catch (Exception ex)
            {
                _respuesta.id = -1;
                _respuesta.result = "ERROR";
                if(_tipoError == 0)
                {
                    _respuesta.err = ex.ToString();
                }
                return new JsonResult(_respuesta)
                {
                    StatusCode = _estado
                };

            }
            
        }
    }
}
