namespace EspacioModels
{
    public class Usuario
    {
        int id;
        string? nombreDeUsuario;

        // Propiedades
        public int Id { get => id; set => id = value; }
        public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    }
}