using AutoMapper;
using Kash.Api.Business.Models;
using Kash.Api.DTO;

namespace Kash.Api.Configuration
{
    /// <summary>
    /// Classe de configuração do AutoMapper
    /// </summary>
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public AutoMapperConfig()
        {
            CreateMap<Banco, BancoDTO>().ReverseMap();
            CreateMap<TipoConta, TipoContaDTO>().ReverseMap();
            CreateMap<Conta, ContaDTO>().ReverseMap();
        }
    }
}

