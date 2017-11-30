using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using EstudosCache.Redis.Services.Interfaces;
using EstudosCache.Redis.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EstudosCache.Redis.Controllers
{
    [Route("api/[controller]")]
    public class RedisController : Controller
    {
        private ICacheService _cacheService;

        public RedisController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }   

        // GET: api/values
        [HttpGet("{chave}")]
        public IActionResult Get(string chave)
        {

            string valor = _cacheService.SelecionaString(chave);

            if(valor == null)
            {
                //Requisição no banco de dados 
                valor = "Valor de teste !!!!";
                _cacheService.ArmazenarValorCacheExpiraTempo(chave, valor);
            }

            return new JsonResult(new { chave = chave, valor = valor });
        }

        [HttpGet("teste/carga")]
        public IActionResult GetTesteCarga()
        {
            var chave = "carga";
            string valor = _cacheService.SelecionaString(chave);

            if (valor == null)
            {
                List<TipoComplexo> tiposComplexos = new List<TipoComplexo>();
                 
                for(int i = 0; i < 10000; i++)
                {
                    tiposComplexos.Add(
                        new TipoComplexo
                        {
                            Texto = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            ValorInteiro = 123123123,
                            ValorNumerico = 1231231231.123123                            
                        }
                    );
                }

                //Requisição no banco de dados 
                valor = Newtonsoft.Json.JsonConvert.SerializeObject(tiposComplexos);
                _cacheService.ArmazenarValorCacheExpiraTempo(chave, valor);
            }

            return new JsonResult(new { chave = chave, valor = valor });
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
