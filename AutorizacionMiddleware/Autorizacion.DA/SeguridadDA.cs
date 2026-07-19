using Autorizacion.Abstracciones.DA;
using Autorizacion.Abstracciones.Modelos;
using Autorizacion.DA.Repositorios;
using Dapper;
using Helpers;
using System.Data;


namespace Autorizacion.DA
{
    public class SeguridadDA : ISeguridadDA
    {
        IRepositorioDapper repositorioDapper;
        private IDbConnection _connection;

        public SeguridadDA(IRepositorioDapper repositorioDapper)
        {
            this.repositorioDapper = repositorioDapper;
            _connection = repositorioDapper.ObtenerRepositorioDapper();
        }

        //public async Task<IEnumerable<Perfil>> ObtenerPerfilesxUsuario(Usuario usuario)
        //{
        //    String sql = @"[ObtenerPerfilesxUsuario]";
        //    var consulta = await _connection.QueryAsync<Abstracciones.Entidades.Perfil>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
        //    return Convertidor.ConvertirLista<Abstracciones.Entidades.Perfil, Abstracciones.Modelos.Perfil>(consulta);
        //}
        public async Task<IEnumerable<Perfil>> ObtenerPerfilesxUsuario(Usuario usuario)
        {
            //using var connetion = _repositorioDapper.ObtenerRepositorio();

            const string sql = @"
        SELECT *
        FROM sigsac.obtener_perfiles_usuario(@Usuario);
    ";

            var consulta = await _connection.QueryAsync<Abstracciones.Entidades.Perfil>(
                sql,
                new
                {
                    Usuario = string.IsNullOrWhiteSpace(usuario.NombreUsuario)
                        ? usuario.CorreoElectronico
                        : usuario.NombreUsuario
                });

            return Convertidor.ConvertirLista<
                Abstracciones.Entidades.Perfil,
                Abstracciones.Modelos.Perfil>(consulta);
        }
        //public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        //{
        //    const string sql = @" SELECT * FROM sigsac.obtener_usuario(@Usuario)";
        //    var consulta = await _connection.QueryAsync<Abstracciones.Entidades.Usuario>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
        //    return Convertidor.Convertir<Abstracciones.Entidades.Usuario, Abstracciones.Modelos.Usuario>(consulta.FirstOrDefault());
        //}

        public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        {
            //using var conexion = _repositorioDapper.ObtenerRepositorio();

            const string sql = @"
        SELECT *
        FROM sigsac.obtener_usuario(@Usuario);
    ";

            var consulta =
                await _connection.QueryFirstOrDefaultAsync<
                    Abstracciones.Entidades.Usuario>(
                    sql,
                    new
                    {
                        Usuario = string.IsNullOrWhiteSpace(usuario.NombreUsuario)
                            ? usuario.CorreoElectronico
                            : usuario.NombreUsuario
                    });

            return Convertidor.Convertir<
                Abstracciones.Entidades.Usuario,
                Abstracciones.Modelos.Usuario>(consulta);
        }
    }
}
