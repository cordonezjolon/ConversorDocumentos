namespace MS_ConvertidorDocumentos.Models
{
    public class Archivo
    {
        public String? path { get; set; }
        public String dominio { get; set; }
        public String id_tipo_Doc { get; set; }
        public String input { get; set; }
        public String output { get; set; }
        public String password { get; set; }
        public String usuario { get; set; }
        public String? pathRtf { get; set; }

        public Archivo()
        {
            this.usuario = "adminSgt";
            this.dominio = "OJ";
            this.password = "Sgt.2008";
        }
    }
}
