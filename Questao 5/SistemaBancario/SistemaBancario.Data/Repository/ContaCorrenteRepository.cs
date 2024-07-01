using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using SistemaBancario.Model;

namespace SistemaBancario.Data.Repository
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetByIdAsync(string id);
        Task<ContaCorrente> GetByNumeroAsync(int numero);
        Task<bool> IsActiveAsync(string id);
        Task AddMovimentoAsync(Movimento movimento);
        Task<decimal> GetSaldoAsync(string id);
    }

    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly string _connectionString;

        public ContaCorrenteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ContaCorrente> GetByIdAsync(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(
                    "SELECT * FROM contacorrente WHERE idcontacorrente = @Id", new { Id = id });
            }
        }

        public async Task<ContaCorrente> GetByNumeroAsync(int numero)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(
                    "SELECT * FROM contacorrente WHERE numero = @Numero", new { Numero = numero });
            }
        }

        public async Task<bool> IsActiveAsync(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<bool>(
                    "SELECT ativo FROM contacorrente WHERE idcontacorrente = @Id", new { Id = id });
            }
        }

        public async Task AddMovimentoAsync(Movimento movimento)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(
                    "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)",
                    movimento);
            }
        }

        public async Task<decimal> GetSaldoAsync(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var creditos = await connection.ExecuteScalarAsync<decimal>(
                    "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'C'", new { Id = id });
                var debitos = await connection.ExecuteScalarAsync<decimal>(
                    "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'D'", new { Id = id });

                return creditos - debitos;
            }
        }
    }

}
