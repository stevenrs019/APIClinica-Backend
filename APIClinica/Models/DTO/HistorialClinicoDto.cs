namespace APIClinica.Models.DTO
{
    public class HistorialClinicoDto
    {
        public int? IdHistorial { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public int? IdCita { get; set; }
        public string Prescripcion { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public int? IdReceta { get; set; }
        public string Receta { get; set; } = string.Empty;
    }
}
