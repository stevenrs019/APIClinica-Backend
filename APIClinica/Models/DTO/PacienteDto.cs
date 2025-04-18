﻿namespace APIClinica.Models.DTO
{
    public class PacienteDto
    {
        public int ID_PACIENTE { get; set; } // Se incluye para modificar y eliminar
        public string NOMBRE { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public string EDAD { get; set; }
        public string TELEFONO { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
    }
}
