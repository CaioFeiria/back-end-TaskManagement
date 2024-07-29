using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TaskManagementSystem.Repositories
{
    public class Usuario
    {
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;
        private readonly string cacheKey;
        private readonly int defaultCacheTimeInSeconds;

        public Usuario(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand() { Connection = conn };
            cacheKey = "Usuarios";
            defaultCacheTimeInSeconds = Configurations.Cache.GetDefaultCacheTimeInSeconds();
        }

        public async Task<List<Models.Usuario>> Select()
        {
            var usuarios = (List<Models.Usuario>) Utils.Cache.Get(cacheKey);

            if (usuarios != null)
                return usuarios;

            usuarios = new List<Models.Usuario>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, nome, email, cargo FROM Usuarios;";
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var usuario = new Models.Usuario();
                            usuario.Id = (int)dr["id"];
                            usuario.Nome = dr["nome"].ToString();
                            usuario.Email = dr["email"].ToString();
                            //usuario.Senha = dr["senha"].ToString();
                            usuario.Cargo = dr["cargo"].ToString();

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            Utils.Cache.Set(cacheKey, usuarios, defaultCacheTimeInSeconds);

            return usuarios;
        }

        public async Task<Models.Usuario> Select(int id)
        {
            var usuario = new Models.Usuario();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, nome, email, cargo FROM Usuarios WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int)).Value = id;
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            usuario.Id = (int)dr["id"];
                            usuario.Nome = dr["nome"].ToString();
                            usuario.Email = dr["email"].ToString();
                            //usuario.Senha = dr["senha"].ToString();
                            usuario.Cargo = dr["cargo"].ToString();
                        }
                    }
                }
            }
            Utils.Cache.Remove(cacheKey);

            return usuario;
        }

        public async Task<List<Models.Usuario>> Select(string nome)
        {
            var usuarios = new List<Models.Usuario>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, nome, email, cargo FROM Usuarios WHERE nome LIKE @nome;";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = $"%{nome}%";
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var usuario = new Models.Usuario();
                            usuario.Id = (int)dr["id"];
                            usuario.Nome = dr["nome"].ToString();
                            usuario.Email = dr["email"].ToString();
                            //usuario.Senha = dr["senha"].ToString();
                            usuario.Cargo = dr["cargo"].ToString();

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        public async Task<List<Models.Usuario>> SelectByCargo(string cargo)
        {
            var usuarios = new List<Models.Usuario>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, nome, email, cargo FROM Usuarios WHERE cargo LIKE @cargo;";
                    cmd.Parameters.Add(new SqlParameter("@cargo", SqlDbType.VarChar)).Value = $"%{cargo}%";
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var usuario = new Models.Usuario();
                            usuario.Id = (int)dr["id"];
                            usuario.Nome = dr["nome"].ToString();
                            usuario.Email = dr["email"].ToString();
                            //usuario.Senha = dr["senha"].ToString();
                            usuario.Cargo = dr["cargo"].ToString();

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        public async Task Insert(Models.Usuario usuario)
        { 
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO Usuarios(nome, email, cargo) VALUES (@nome, @email, @cargo); SELECT CONVERT(int, scope_identity());";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = usuario.Nome;
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar)).Value = usuario.Email;
                    //cmd.Parameters.Add(new SqlParameter("@senha", SqlDbType.VarChar)).Value = usuario.Senha;
                    cmd.Parameters.Add(new SqlParameter("@cargo", SqlDbType.VarChar)).Value = usuario.Cargo;

                    usuario.Id = (int) await cmd.ExecuteScalarAsync();
                }
            }
            Utils.Cache.Remove(cacheKey);
        }

        public async Task<bool> Update(Models.Usuario usuario)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Usuarios SET nome = @nome, email = @email, cargo = @cargo WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = usuario.Nome;
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar)).Value = usuario.Email;
                    //cmd.Parameters.Add(new SqlParameter("@senha", SqlDbType.VarChar)).Value = usuario.Senha;
                    cmd.Parameters.Add(new SqlParameter("@cargo", SqlDbType.VarChar)).Value = usuario.Cargo;
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int)).Value = usuario.Id;

                    linhasAfetadas = (int)await cmd.ExecuteNonQueryAsync();
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
                    cmd.CommandText = "DELETE FROM Tarefas WHERE id_responsavel = @id; DELETE FROM Usuarios WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int)).Value = id;

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            Utils.Cache.Remove(cacheKey);

            return linhasAfetadas == 1;
        }
    }
}