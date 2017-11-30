using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudosCache.Redis.Services.Interfaces
{
    public interface ICacheService
    {
        string SelecionaString(string chave);
        void ArmazenarValorCacheExpiraTempo(string chave, string valor);
        void ArmazenarValorCacheExpiraInatividade(string chave, string valor);
    }
}
