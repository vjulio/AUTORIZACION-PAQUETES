
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Autorizacion.Abstracciones.DA;


namespace Autorizacion.DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuration;
        

        public RepositorioDapper(IConfiguration configuration)
        {
            _configuration =  configuration;
            
        }

        public IDbConnection ObtenerRepositorioDapper()
        {
            return new NpgsqlConnection( _configuration.GetConnectionString("BDSeguridad") );
        }
    }
}
