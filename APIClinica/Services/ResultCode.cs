namespace APIClinica.Services
{
    public enum ResultCode
    {
        Exito = 0,

        // Validaciones de datos (usuario)
        DatosIncompletos = -10,

        // Errores del sistema o lógica
        ErrorInterno = -1,
        ErrorBaseDatos = -2,
        ErrorDesconocidoBaseDatos = -99,
        SP_SinRespuesta = -3
    }
}
