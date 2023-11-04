using System.Data.SQLite;
using EspacioModels;

namespace EspacioRepositorios
{
    class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/TP08-CosentinoLuciano.db;Cache=Shared";

        // public Tarea Create(int idTablero);
        public void Update(int id, Tarea tarea)
        {
            var query = $"UPDATE Tarea SET id_tablero = (@tablero), nombre = (@name), estado = (@estado), descripcion = (@descripcion), color = (@color), id_usuario_asignado = (@usuario) WHERE id_tarea = (@id);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@tablero", tarea.IdTablero));

                command.Parameters.Add(new SQLiteParameter("@name", tarea.Nombre));

                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));

                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));

                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));

                command.Parameters.Add(new SQLiteParameter("@usuario", tarea.IdUsuarioAsignado)); // Modifico esto o no hace falta por el otro metodo?

                command.Parameters.Add(new SQLiteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Tarea GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tarea = new Tarea();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE id_tarea = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"])); // Hacer metodo por separado?
                        tarea.Descripcion = reader["descricion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();

            return tarea;
        }
        public List<Tarea> GetByIdUsuario(int idUsuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            List<Tarea> tareas = new List<Tarea>();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE id_usuario_asignado = @asignado";
            command.Parameters.Add(new SQLiteParameter("@asignado", idUsuario));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new Tarea
                    {
                        Id = Convert.ToInt32(reader["id_tarea"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"])), // Hacer metodo por separado?
                        Descripcion = reader["descricion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                    };
                    tareas.Add(tarea);
                }
            }
            connection.Close();

            return tareas;
        }
        public List<Tarea> GetByIdTablero(int idTablero)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            List<Tarea> tareas = new List<Tarea>();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE id_tablero = @tablero";
            command.Parameters.Add(new SQLiteParameter("@tablero", idTablero));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new Tarea
                    {
                        Id = Convert.ToInt32(reader["id_tarea"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"])), // Hacer metodo por separado?
                        Descripcion = reader["descricion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                    };
                    tareas.Add(tarea);
                }
            }
            connection.Close();

            return tareas;
        }
        public void Remove(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tarea WHERE id_tarea = @id;";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AsignarUsuario(int idTarea, int idUsuario)
        {
            var query = $"UPDATE Tarea SET id_usuario_asignado = (@usuario) WHERE id_tarea = (@id);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@usuario", idUsuario));

                command.Parameters.Add(new SQLiteParameter("@id", idTarea));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}