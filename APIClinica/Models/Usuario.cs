using Microsoft.AspNetCore.Identity;

namespace APIClinica.Models
{
    public class Usuario
    {
        public int ID_USUARIO {  get; set; }
        public string EMAIL { get; set; }
        public string CONTRASENA { get; set; }
        public int ID_ROL {  get; set; }
        public int ID_PERSONA {  get; set; }
    }
}
