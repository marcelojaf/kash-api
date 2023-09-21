using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Business.Models.Validations;

namespace Kash.Api.Business.Services
{
    /// <summary>
    /// Classe com serviços da entidade TipoConta
    /// </summary>
    public class TipoContaService : BaseService, ITipoContaService
    {
        private readonly ITipoContaRepository _tipoContaRepository;

        /// <summary>
        /// Construtor com repositório
        /// </summary>
        /// <param name="tipoContaRepository">Repositório da entidade TipoConta</param>
        public TipoContaService(ITipoContaRepository tipoContaRepository)
        {
            _tipoContaRepository = tipoContaRepository;
        }

        /// <summary>
        /// Insere um Tipo de Conta
        /// </summary>
        /// <param name="tipoConta">Objeto do tipo TipoConta a ser inserido</param>
        /// <returns></returns>
        public async Task Insert(TipoConta tipoConta)
        {
            if (!ExecutarValidacao(new TipoContaValidation(), tipoConta))
                return;

            await _tipoContaRepository.Insert(tipoConta);
        }

        /// <summary>
        /// Atualiza um Tipo de Conta
        /// </summary>
        /// <param name="tipoConta">Objeto do tipo TipoConta a ser atualizado</param>
        /// <returns></returns>
        public async Task Update(TipoConta tipoConta)
        {
            if (!ExecutarValidacao(new TipoContaValidation(), tipoConta))
                return;

            await _tipoContaRepository.Update(tipoConta);
        }

        /// <summary>
        /// Remove um Tipo de Conta
        /// </summary>
        /// <param name="id">Id do Tipo de Conta a ser removido</param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            await _tipoContaRepository.Delete(id);
        }

        /// <summary>
        /// Realiza o Dispose do repositório
        /// </summary>
        public void Dispose()
        {
            _tipoContaRepository.Dispose();
        }
    }
}
