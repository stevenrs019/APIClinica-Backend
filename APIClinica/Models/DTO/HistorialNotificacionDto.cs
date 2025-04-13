namespace APIClinica.Models.DTO
{
    public class HistorialNotificacionDto
    {
        public int ID_HISTORIAL_NOTIFICACION { get; set; } // Solo para obtener
        public int ID_TIPO_NOTIFICACION { get; set; }
        public string MENSAJE { get; set; }
        public string EMAIL { get; set; } = string.Empty;
        public DateTime FECHA_ENVIO { get; set; } // Solo para obtener
    }
}

