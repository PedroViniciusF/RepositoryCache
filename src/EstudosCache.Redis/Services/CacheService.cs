using EstudosCache.Redis.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudosCache.Redis.Services
{
    public class CacheService : ICacheService
    {
        private IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public string SelecionaString(string chave)
        {
            return _distributedCache.GetString(chave);
        }

        public void ArmazenarValorCacheExpiraTempo(string chave, string valor)
        {
            DistributedCacheEntryOptions opcoesCache =
                new DistributedCacheEntryOptions();

            //Absolute é para que expire em x tempo(Horario atual + valor x)   
            opcoesCache.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));         

            _distributedCache.SetString(chave, valor,opcoesCache);
        }

        public void ArmazenarValorCacheExpiraInatividade(string chave, string valor)
        {
            DistributedCacheEntryOptions opcoesCache =
                new DistributedCacheEntryOptions();

            //Sliding é para quando ficar inativo por tempo x, o cache expira.
            opcoesCache.SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _distributedCache.SetString(chave, valor, opcoesCache);
        }
    }
}
