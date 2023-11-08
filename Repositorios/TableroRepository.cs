using System.Data.SQLite;
using EspacioModels;

namespace EspacioRepositorios
{
    class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/TP08-CosentinoLuciano.db;Cache=Shared";

        public void Create(Tablero tablero) // no recibe id_usuario por ser FK
        {
            var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@usuario, @name, @descripcion)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@usuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@name", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));


                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(int id, Tablero tablero)
        {
            var query = $"UPDATE Tablero SET id_usuario_propietario = (@propietario), nombre = (@name), descripcion = (@descripcion) WHERE id_tablero = (@id);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@propietario", tablero.IdUsuarioPropietario));

                command.Parameters.Add(new SQLiteParameter("@name", tablero.Nombre));

                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                command.Parameters.Add(new SQLiteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Tablero GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tablero = new Tablero();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tablero WHERE id_tablero = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();

            return tablero;
        }
        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id_tablero"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString()
                        };
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }
        public List<Tablero> GetByIdUsuario(int idUsuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            List<Tablero> tableros = new List<Tablero>();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tablero WHERE id_usuario_propietario = @propietario";
            command.Parameters.Add(new SQLiteParameter("@id", idUsuario));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tablero = new Tablero
                    {
                        Id = Convert.ToInt32(reader["id_tablero"]),
                        IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                        Nombre = reader["nombre"].ToString(),
                        Descripcion = reader["descripcion"].ToString()
                    };
                    tableros.Add(tablero);
                }
            }
            connection.Close();

            return tableros;
        }
        public void Remove(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tablero WHERE id_tablero = @id;";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}