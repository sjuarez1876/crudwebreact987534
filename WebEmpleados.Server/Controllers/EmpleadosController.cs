using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebEmpleados.Server.Models;
using WebEmpleados.Server.Repositories;

namespace WebEmpleados.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadosRepository _empleados;
        public EmpleadosController(EmpleadosRepository empleados)
        {
            _empleados = empleados;
        }


        [HttpGet]
        public async Task<List<EmpleadosModel>> ConsultaEmpleadosGrid()
        {
            List<EmpleadosModel> lscatEmpleados = await _empleados.ConsultaEmpleadosGrid();

            return  lscatEmpleados;
        }

        [HttpPost]
        public async Task<Respuesta> agregarRegistro([FromBody] EmpleadosModel objEmpleados)
        {
            Respuesta objRespuesta = new Respuesta();   
             objRespuesta = await _empleados.AgregaEmpleado(objEmpleados);

            return objRespuesta;
        }

        [HttpPost("ActualizarEmpleado")]
        public async Task<Respuesta> actualizaRegistro([FromBody] EmpleadosModel objEmpleados)
        {
            Respuesta objRespuesta = new Respuesta();
            objRespuesta = await _empleados.ActualizaEmpleado(objEmpleados);

            return objRespuesta;
        }

        [HttpDelete("{IdEmpleado}")]
        public async Task<Respuesta> elminaRegistro(int IdEmpleado)
        {
            Respuesta objRespuesta = new Respuesta();
            objRespuesta = await _empleados.EliminaEmpleado(IdEmpleado);

            return objRespuesta;
        }
    }
}
