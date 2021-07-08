using KdTraining.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdTraining.API.Controllers
{
    [ApiController]
    [Route("api/autos")]
    public class AutoController : ControllerBase
    {
        private static List<AutoModel> _datos;
        public AutoController()
        {
            if (_datos == null)
            {
                _datos = new List<AutoModel> {
                    new AutoModel { Patente = "AAA111", Titular = "LBJ", Marca = "Benz", Modelo = "Clase C", Tipo = "Auto" },
                    new AutoModel { Patente = "BBB222", Titular = "TM", Marca = "Ford", Modelo = "Focus", Tipo = "Auto" }
                };
            }
        }

        [HttpGet] //api/autos?marca=...&titular=...     1 o los 2
        public IActionResult Get(string marca, string titular)
        {
            var autos = _datos
                .Where(a => string.IsNullOrEmpty(titular) || a.Titular == titular)
                .Where(a => string.IsNullOrWhiteSpace(marca) || a.Marca == marca)
                .ToList();

            return Ok(autos);
        }
        [HttpGet("{patente}")]
        public IActionResult Get(string patente)
        {
            var auto = _datos.FirstOrDefault(a => a.Patente == patente);
            if (auto == null)
            {
                return NotFound();
            }
            return Ok(auto);
        }
        [HttpPost]
        public IActionResult Post(AutoModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Patente) ||
                string.IsNullOrWhiteSpace(model.Marca) ||
                string.IsNullOrWhiteSpace(model.Titular) ||
                string.IsNullOrWhiteSpace(model.Modelo) ||
                string.IsNullOrWhiteSpace(model.Tipo))
            {
                return BadRequest("Todos los datos son requeridos");
            }

            var patenterepetida = _datos.Any(a => a.Patente == model.Patente);
            if (patenterepetida)
            {
                return BadRequest("La patente ya se encuentra cargada");
            }

            _datos.Add(model);
            return Ok(model);
        }
        [HttpPut("{patente}")]
        public IActionResult Put([FromRoute] string patente, [FromBody] AutoModel model)
        {
            if (string.IsNullOrWhiteSpace(patente))
            {
                return BadRequest("No se ha especificado una patente");
            }

            var auto = _datos.FirstOrDefault(a => a.Patente == patente);
            if (auto == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(model.Patente) ||
                string.IsNullOrWhiteSpace(model.Marca) ||
                string.IsNullOrWhiteSpace(model.Titular) ||
                string.IsNullOrWhiteSpace(model.Modelo) ||
                string.IsNullOrWhiteSpace(model.Tipo))
            {
                return BadRequest("Todos los datos son requeridos");
            }

            if (!string.IsNullOrWhiteSpace(patente) || patente != model.Patente)
            {
                var patenterepetida = _datos.Any(a => a.Patente == model.Patente);
                if (patenterepetida)
                {
                    return BadRequest("La patente ya se encuentra cargada");
                }

                auto.Patente = model.Patente;
            }

            auto.Titular = model.Titular;
            auto.Marca = model.Marca;
            auto.Modelo = model.Modelo;
            auto.Tipo = model.Tipo;

            return Ok(auto);
        }
        [HttpDelete("{patente}")]
        public IActionResult Delete(string patente)
        {
            if (string.IsNullOrWhiteSpace(patente))
            {
                return BadRequest("No se ha especificado una patente");
            }

            var auto = _datos.FirstOrDefault(a => a.Patente == patente);

            if (auto == null)
            {
                return NotFound();
            }

            _datos.Remove(auto);

            return Ok(auto);
        }
    }
}




