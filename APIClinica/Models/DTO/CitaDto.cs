namespace APIClinica.Models.DTO
{
    public class CitaDto
    {
        public int ID_CITA { get; set; }

        public int ID_USUARIO { get; set; }
        public int ID_MEDICO { get; set; }
        public int ID_PACIENTE { get; set; }
        public int ID_HORARIO { get; set; }
        public int DIA { get; set; }
        public int MES { get; set; }
        public int ANIO { get; set; }

        public string NOMBRE_PACIENTE { get; set; } = string.Empty;
        public string HORA_INICIO { get; set; } = string.Empty;
        public string HORA_FIN { get; set; } = string.Empty;
        public string ESTADO { get; set; } = string.Empty;
    }

}
