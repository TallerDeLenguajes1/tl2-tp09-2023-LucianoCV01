using System.Data.SQLite;
using EspacioModels;

namespace EspacioRepositorios
{
    class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/TP08-CosentinoLuciano.db;Cache=Shared";

        public void Create(Usuario usuario)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario) VALUES (@name)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(int id, Usuario usuario)
        {
            var query = $"UPDATE Usuario SET nombre_de_usuario = (@name) WHERE id_usuario = (@id);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));

                command.Parameters.Add(new SQLiteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            Id = Convert.ToInt32(reader["id_usuario"]),
                            NombreDeUsuario = reader["nombre_de_usuario"].ToString()
                        };
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }
        public Usuario GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Usuario WHERE id_usuario = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                }
            }
            connection.Close();

            return usuario;
        }
        public void Remove(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Usuario WHERE id_usuario = @id;";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}