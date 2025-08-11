using Microsoft.Data.SqlClient;
using System.Data;
using WebEmpleados.Server.Data;
using WebEmpleados.Server.Models;

namespace WebEmpleados.Server.Repositories
{
    public class EmpleadosRepository
    {
        private readonly DbContext _dbContext;
        public EmpleadosRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmpleadosModel>> ConsultaEmpleadosGrid()
        {

            List<EmpleadosModel> _objEmpleados = new List<EmpleadosModel>();

            try
            {              
                var table = await _dbContext.QueryAsync("sp_ConsultaEmpleados", null);

                if (table.Rows.Count > 0)
                {

                    foreach (DataRow r in table.Rows)
                    {

                        var periodos = new EmpleadosModel()
                        {

                            IdEmpleado = int.Parse(r["IdEmpleado"].ToString()),

                            Nombre = r["Nombre"] != null ? r["Nombre"].ToString() : string.Empty,

                            Telefono = r["Telefono"] != null ? r["Telefono"].ToString(): string.Empty,

                            IdRol = int.Parse(r["IdRol"].ToString()),

                            Descripcion = r["Descripcion"] != null ? r["Descripcion"].ToString() : string.Empty,

                        };

                        _objEmpleados.Add(periodos);

                    }

                }

            }

            catch (Exception ex)
            {

                _objEmpleados = null;

            }
            return _objEmpleados;

        }


        public async Task<Respuesta> AgregaEmpleado(EmpleadosModel objEmpleado)
        {

            Respuesta _objrespuesta = new Respuesta();

            try
            {              
                var _params = new[] {

                    new SqlParameter("@Nombre",objEmpleado.Nombre),
                    new SqlParameter("@Telefono",objEmpleado.Telefono),
                    new SqlParameter("@IdRol",objEmpleado.IdRol),

                };

                var result = await _dbContext.ExecuteScalarString("sp_InsertaEmpleado", _params);
                //_objrespuesta.rowId = result.ToString();
                _objrespuesta.codigo = "1";
                _objrespuesta.mensaje = "OK";
            }

            catch (Exception ex)
            {
                _objrespuesta.mensaje = ex.StackTrace.ToString() + '|' + ex.Message.ToString();
                _objrespuesta.codigo = "1000";
                _objrespuesta.error = "A ocurrido un error reporte al administrador";
            }

            return _objrespuesta;

        }


        public async Task<Respuesta> EliminaEmpleado(int IdEmpleado)
        {

            Respuesta _objrespuesta = new Respuesta();

            try
            {
                var _params = new[] {

                    new SqlParameter("@IdEmpleado",IdEmpleado)
       
                };

                var result = await _dbContext.ExecuteScalarString("sp_EliminaEmpleado", _params);
                _objrespuesta.codigo = "1";
                _objrespuesta.mensaje = "OK";
            }

            catch (Exception ex)
            {
                _objrespuesta.mensaje = ex.StackTrace.ToString() + '|' + ex.Message.ToString();
                _objrespuesta.codigo = "1000";
                _objrespuesta.error = "A ocurrido un error reporte al administrador";
            }

            return _objrespuesta;

        }

        public async Task<Respuesta> ActualizaEmpleado(EmpleadosModel objEmpleado)
        {

            Respuesta _objrespuesta = new Respuesta();

            try
            {
                var _params = new[] {

                    new SqlParameter("@Nombre",objEmpleado.Nombre),
                    new SqlParameter("@Telefono",objEmpleado.Telefono),
                    new SqlParameter("@IdEmpleado",objEmpleado.IdEmpleado),
                     new SqlParameter("@IdRol",objEmpleado.IdRol),

                };

                var result = await _dbContext.ExecuteScalarString("sp_ActualizaEmpleado", _params);
                _objrespuesta.codigo = "1";
                _objrespuesta.mensaje = "OK";
            }

            catch (Exception ex)
            {
                _objrespuesta.mensaje = ex.StackTrace.ToString() + '|' + ex.Message.ToString();
                _objrespuesta.codigo = "1000";
                _objrespuesta.error = "A ocurrido un error reporte al administrador";
            }

            return _objrespuesta;

        }
    }
}
