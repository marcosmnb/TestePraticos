using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using SistemaBancario.Model;

namespace SistemaBancario.Data.Repository
{
    public interface IIdempotenciaRepository
    {
        Task<Idempotencia> GetByChaveAsync(string chave);
        Task SaveAsync(Idempotencia idempotencia);
    }

    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly string _connectionString;

        public IdempotenciaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Idempotencia> GetByChaveAsync(string chave)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Idempotencia>(
                    "SELECT * FROM idempotencia WHERE chaveidempotencia = @Chave", new { Chave = chave });
            }
        }

        public async Task SaveAsync(Idempotencia entry)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                object value = await connection.ExecuteAsync(
                    "INSERT INTO idempotencia (chaveidempotencia, requisicao, resultado) VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)",
                    entry);
            }
        }
    }
}
