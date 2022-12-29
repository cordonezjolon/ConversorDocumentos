namespace MS_ConvertidorDocumentos.BusinessLogic
{
    public class appConfig
    {
        public String urlDatosEsquema { get; set; }
        //public String URL_OBTENER_INFORMACION { get; set; }
        //  public String URL_INTRA { get; set; }
        public String URI_PUENTE { get; set; }
        public String usuarioArchivos { get; set; }
        public String dominioArchivos { get; set; }
        public String passwordArchivos { get; set; }
        public String urlTools { get; set; }
        public String hostCorreo { get; set; }
        public String userCorreo { get; set; }
        public String passwordCorreo { get; set; }
        public String puertoCorreo { get; set; }
        public String destinatariosSoporte { get; set; }
        public String sslCorreo { get; set; }


        public appConfig()
        {
            urlDatosEsquema = "";
            URI_PUENTE = "";
            usuarioArchivos = "";
            dominioArchivos = "";
            passwordArchivos = "";
            urlTools = "";
            hostCorreo = "";
            passwordCorreo = "";
            userCorreo = "";
            puertoCorreo = "";
            destinatariosSoporte = "";
            sslCorreo = "";
        }
    }
}
