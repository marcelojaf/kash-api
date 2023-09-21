using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Business.Models.Validations;
using Newtonsoft.Json;

namespace Kash.Api.Business.Services
{
    /// <summary>
    /// Classe com serviços da entidade Conta
    /// </summary>
    public class ContaService : BaseService, IContaService
    {
        private readonly IContaRepository _contaRepository;

        /// <summary>
        /// Construtor com repositório
        /// </summary>
        /// <param name="contaRepository">Repositório da entidade Conta</param>
        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        /// <summary>
        /// Insere uma Conta
        /// </summary>
        /// <param name="conta">Objeto do tipo Conta a ser inserido</param>
        /// <returns></returns>
        public async Task Insert(Conta conta)
        {
            if (!ExecutarValidacao(new ContaValidation(), conta))
                return;

            VerificarSeNumeroDaContaExiste(conta.Numero);

            await _contaRepository.Insert(conta);
        }

        /// <summary>
        /// Atualiza uma Conta
        /// </summary>
        /// <param name="conta">Objeto do tipo Conta a ser atualizado</param>
        /// <returns></returns>
        public async Task Update(Conta conta)
        {
            if (!ExecutarValidacao(new ContaValidation(), conta))
                return;

            await _contaRepository.Insert(conta);
        }

        /// <summary>
        /// Remove uma Conta
        /// </summary>
        /// <param name="id">Id do Conta a ser removida</param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            await _contaRepository.Delete(id);
        }

        /// <summary>
        /// Realiza o Dispose do repositório
        /// </summary>
        public void Dispose()
        {
            _contaRepository.Dispose();
        }

        /// <summary>
        /// Verifica se o número da Conta a ser inserida já existe no sistema
        /// </summary>
        /// <param name="numeroDaConta">Número a ser inserido</param>
        /// <exception cref="Exception"></exception>
        public void VerificarSeNumeroDaContaExiste(string numeroDaConta)
        {
            var contaExistenteComEsseNumero = _contaRepository.Get(c => c.Numero == numeroDaConta).Result.FirstOrDefault();

            if (!string.IsNullOrEmpty(numeroDaConta) && contaExistenteComEsseNumero is not null)
            {
                throw new Exception($"Já existe uma Conta com esse número: {JsonConvert.SerializeObject(contaExistenteComEsseNumero)}");
            }
        }
    }
}
