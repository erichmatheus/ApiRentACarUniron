using ApiRentACarUniron.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRentACarUniron.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : Controller
    {
        /// <summary>
        /// criar um veículo. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns> Um objeto do tipo veículo salvo no banco </returns>
        /// <response code="200">Retorna um objeto veiculo cadastrado </response>
        /// <response code="400">Retorna caso falte algum critério</response>
        [HttpPost]
        public async Task<ActionResult<Veiculo>> Post([FromServices] Data.DadosContexto context, [FromBody] Veiculo model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    model.Marca = model.Marca.ToLower();
                    model.Modelo = model.Modelo.ToLower();
                    context.Veiculos.Add(model);
                    await context.SaveChangesAsync();
                    return Ok(model);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// Atualiza um específico veículo.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id">Informar o ID do veículo</param>
        /// <param name="model"></param>
        /// <returns>Um objeto do tipo veículo salvo no banco </returns>
        /// <response code="200">Retorna um objeto veiculo cadastrado </response>
        /// <response code="400">Retorna caso falte algum critério</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Veiculo>> Update([FromServices] Data.DadosContexto context, int id, [FromBody] Veiculo model)
        {
            var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == id);
            if (veiculo != null)
            {
                veiculo.Marca = model.Marca != null ? model.Marca : veiculo.Marca;
                veiculo.Modelo = model.Modelo != null ? model.Modelo : veiculo.Modelo;
                veiculo.Quilometragem = model.Quilometragem != 0 ? model.Quilometragem : veiculo.Quilometragem;
                veiculo.Ano = model.Ano != 0 ? model.Ano : veiculo.Ano;
                context.Update(veiculo);
                await context.SaveChangesAsync();
                return Ok(veiculo);
            }

            return BadRequest();

        }
        /// <summary>
        /// lista todos os veículos.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Lista de todos os veículos</returns>
        /// <response code="200">Retorna uma lista veiculo </response>
        /// <response code="404">Retorna Nenhum Dado</response>
        [HttpGet]
        public async Task<ActionResult<List<Veiculo>>> Get([FromServices] Data.DadosContexto context)
        {

            if (ModelState.IsValid)
            {
                var veiculos = await context.Veiculos.ToListAsync();
                if (veiculos != null)
                {
                    return Ok(veiculos);
                }
            }

            return NotFound();

        }
        /// <summary>
        /// lista os veículos da To-do list por filtro marca.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="marca">'Fiat'</param>
        /// <remarks>
        /// Exemplo: "Fiat"
        /// </remarks>
        /// <returns>Lista veículos por marca</returns>
        /// <response code="200">Retorna uma lista veiculo </response>
        /// <response code="404">Retorna Nenhum Dado</response>
       
        [HttpPost("marca")]
        public async Task<ActionResult<List<Veiculo>>> GetByMarca([FromServices] Data.DadosContexto context, [FromBody] string marca)
        {

            if (ModelState.IsValid)
            {
                var veiculos = await context.Veiculos.Where(x => x.Marca == marca.ToLower()).ToListAsync();
                if (veiculos != null)
                {
                    return Ok(veiculos);
                }
            }

            return NotFound();

        }
        /// <summary>
        /// lista os veículos da To-do list por filtro modelo.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelo">'Fiat Touro'</param>
        /// <remarks>
        /// Exemplo: "Fiat Touro"
        /// </remarks>
        /// <returns> Lista veículo por modelo</returns>
        /// <response code="200">Retorna uma lista veiculo </response>
        /// <response code="404">Retorna Nenhum Dado</response>
       
        [HttpPost("modelo")]
        public async Task<ActionResult<List<Veiculo>>> GetByModelo([FromServices] Data.DadosContexto context, [FromBody] string modelo)
        {

            if (ModelState.IsValid)
            {
                var veiculos = await context.Veiculos.Where(x => x.Modelo == modelo.ToLower()).ToListAsync();
                if (veiculos != null)
                {
                    return Ok(veiculos);
                }
            }

            return NotFound();

        }

        /// <summary>
        ///  Remove um Veiculo específico. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns>Retorna um objeto veiculo excluido</returns>
        /// <response code="200">Retorna um objeto veiculo que estava cadastrado </response>
        /// <response code="400">Retorna null caso não tenha parametro</response>

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Veiculo>> Delete([FromServices] Data.DadosContexto context, int id)
        {

            if (ModelState.IsValid)
            {
                var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == id);
                context.Remove(veiculo);
                await context.SaveChangesAsync();
                return Ok(veiculo);
            }
            return BadRequest();

        }

    }
}
