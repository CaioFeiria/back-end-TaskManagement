using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TaskManagementSystem.Repositories
{
    public class Tarefa
    {
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;
        private readonly string cacheKey;
        private readonly int defaultCacheTimeInSeconds;

        public Tarefa(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand() { Connection = conn };
            cacheKey = "Tarefas";
            defaultCacheTimeInSeconds = Configurations.Cache.GetDefaultCacheTimeInSeconds();
        }

        public async Task<List<Models.Tarefa>> Select()
        {
            var tarefas = (List<Models.Tarefa>)Utils.Cache.Get(cacheKey);

            if (tarefas != null)
                return tarefas;

            tarefas = new List<Models.Tarefa>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, titulo, descricao, prazo, prioridade, estado, id_responsavel FROM Tarefas ORDER BY prioridade DESC, prazo ASC;";
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var tarefa = new Models.Tarefa();
                            tarefa.Id = (int)dr["id"];
                            tarefa.Titulo = dr["titulo"].ToString();
                            tarefa.Descricao = dr["descricao"].ToString();
                            tarefa.Prazo = Convert.ToDateTime(dr["prazo"]);
                            tarefa.Prioridade = Convert.ToBoolean(dr["prioridade"]);
                            tarefa.Estado = Convert.ToBoolean(dr["estado"]);
                            tarefa.Id_responsavel = (int)dr["id_responsavel"];

                            tarefas.Add(tarefa);
                        }
                    }
                }
            }
            Utils.Cache.Set(cacheKey, tarefas, defaultCacheTimeInSeconds);

            return tarefas;
        }

        public async Task<Models.Tarefa> Select(int id)
        {
            var tarefa = new Models.Tarefa();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, titulo, descricao, prazo, prioridade, estado, id_responsavel FROM Tarefas WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            tarefa.Id = (int)dr["id"];
                            tarefa.Titulo = dr["titulo"].ToString();
                            tarefa.Descricao = dr["descricao"].ToString();
                            tarefa.Prazo = Convert.ToDateTime(dr["prazo"]);
                            tarefa.Prioridade = Convert.ToBoolean(dr["prioridade"]);
                            tarefa.Estado = Convert.ToBoolean(dr["estado"]);
                            tarefa.Id_responsavel = (int)dr["id_responsavel"];

                        }
                    }
                }
            }
            Utils.Cache.Remove(cacheKey);

            return tarefa;
        }

        public async Task<List<Models.Tarefa>> Select(string titulo)
        {
            var tarefas = new List<Models.Tarefa>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, titulo, descricao, prazo, prioridade, estado, id_responsavel FROM Tarefas WHERE titulo LIKE @titulo;";
                    cmd.Parameters.Add(new SqlParameter("@titulo", System.Data.SqlDbType.VarChar)).Value = $"%{titulo}%";
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var tarefa = new Models.Tarefa();
                            tarefa.Id = (int)dr["id"];
                            tarefa.Titulo = dr["titulo"].ToString();
                            tarefa.Descricao = dr["descricao"].ToString();
                            tarefa.Prazo = Convert.ToDateTime(dr["prazo"]);
                            tarefa.Prioridade = Convert.ToBoolean(dr["prioridade"]);
                            tarefa.Estado = Convert.ToBoolean(dr["estado"]);
                            tarefa.Id_responsavel = (int)dr["id_responsavel"];

                            tarefas.Add(tarefa);
                        }
                    }
                }
            }

            return tarefas;
        }

        public async Task<List<Models.Tarefa>> SelectByPrazo(DateTime prazo)
        {
            var tarefas = new List<Models.Tarefa>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, titulo, descricao, prazo, prioridade, estado, id_responsavel FROM Tarefas WHERE prazo = @prazo;";
                    cmd.Parameters.Add(new SqlParameter("@prazo", System.Data.SqlDbType.Date)).Value = prazo;
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var tarefa = new Models.Tarefa();
                            tarefa.Id = (int)dr["id"];
                            tarefa.Titulo = dr["titulo"].ToString();
                            tarefa.Descricao = dr["descricao"].ToString();
                            tarefa.Prazo = Convert.ToDateTime(dr["prazo"]);
                            tarefa.Prioridade = Convert.ToBoolean(dr["prioridade"]);
                            tarefa.Estado = Convert.ToBoolean(dr["estado"]);
                            tarefa.Id_responsavel = (int)dr["id_responsavel"];

                            tarefas.Add(tarefa);
                        }
                    }
                }
            }

            return tarefas;
        }

        public async Task Insert(Models.Tarefa tarefa)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO Tarefas(titulo, descricao, prazo, prioridade, estado, id_responsavel) VALUES (@titulo, @descricao, @prazo, @prioridade, @estado, @id_responsavel); SELECT CONVERT(int, scope_identity());";
                    cmd.Parameters.Add(new SqlParameter("@titulo", System.Data.SqlDbType.VarChar)).Value = tarefa.Titulo;
                    if (tarefa.Descricao is null)
                        cmd.Parameters.Add(new SqlParameter("@descricao", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(new SqlParameter("@descricao", System.Data.SqlDbType.VarChar)).Value = tarefa.Descricao;
                    cmd.Parameters.Add(new SqlParameter("@prazo", System.Data.SqlDbType.Date)).Value = tarefa.Prazo;
                    cmd.Parameters.Add(new SqlParameter("@prioridade", System.Data.SqlDbType.Bit)).Value = tarefa.Prioridade;
                    cmd.Parameters.Add(new SqlParameter("@estado", System.Data.SqlDbType.Bit)).Value = tarefa.Estado;
                    cmd.Parameters.Add(new SqlParameter("@id_responsavel", System.Data.SqlDbType.Int)).Value = tarefa.Id_responsavel;

                    tarefa.Id = (int) await cmd.ExecuteScalarAsync();
                }

            }

            Utils.Cache.Remove(cacheKey);
        }

        public async Task<bool> Update(Models.Tarefa tarefa)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Tarefas SET titulo = @titulo, descricao = @descricao, prazo = @prazo, prioridade = @prioridade, estado = @estado, id_responsavel = @id_responsavel WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@titulo", System.Data.SqlDbType.VarChar)).Value = tarefa.Titulo;
                    cmd.Parameters.Add(new SqlParameter("@descricao", System.Data.SqlDbType.VarChar)).Value = tarefa.Descricao;
                    cmd.Parameters.Add(new SqlParameter("@prazo", System.Data.SqlDbType.Date)).Value = tarefa.Prazo;
                    cmd.Parameters.Add(new SqlParameter("@prioridade", System.Data.SqlDbType.Bit)).Value = tarefa.Prioridade;
                    cmd.Parameters.Add(new SqlParameter("@estado", System.Data.SqlDbType.Bit)).Value = tarefa.Estado;
                    cmd.Parameters.Add(new SqlParameter("@id_responsavel", System.Data.SqlDbType.Int)).Value = tarefa.Id_responsavel;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = tarefa.Id;

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            Utils.Cache.Remove(cacheKey);

            return linhasAfetadas == 1;
        }

        public async Task<bool> Delete(int id)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "DELETE FROM Tarefas WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            Utils.Cache.Remove(cacheKey);

            return linhasAfetadas == 1;
        }
    }
}