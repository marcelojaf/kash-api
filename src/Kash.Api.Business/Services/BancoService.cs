using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Business.Models.Validations;
using Newtonsoft.Json;

namespace Kash.Api.Business.Services
{
    /// <summary>
    /// Classe com serviços da entidade Banco
    /// </summary>
    public class BancoService : BaseService, IBancoService
    {
        private readonly IBancoRepository _bancoRepository;

        /// <summary>
        /// Construtor com repositório
        /// </summary>
        /// <param name="bancoRepository">Repositório da entidade Banco</param>
        public BancoService(IBancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }

        /// <summary>
        /// Insere um Banco
        /// </summary>
        /// <param name="banco">Objeto do tipo Banco a ser inserido</param>
        /// <returns></returns>
        public async Task Insert(Banco banco)
        {
            if (!ExecutarValidacao(new BancoValidation(), banco))
                return;

            VerificarSeNumeroDoBancoExiste(banco.Numero);

            await _bancoRepository.Insert(banco);
        }

        /// <summary>
        /// Atualiza um Banco
        /// </summary>
        /// <param name="banco">Objeto do tipo Banco a ser atualizado</param>
        /// <returns></returns>
        public async Task Update(Banco banco)
        {
            if (!ExecutarValidacao(new BancoValidation(), banco))
                return;

            await _bancoRepository.Update(banco);
        }

        /// <summary>
        /// Remove um Banco
        /// </summary>
        /// <param name="id">Id do Banco a ser removido</param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            await _bancoRepository.Delete(id);
        }

        /// <summary>
        /// Realiza o Dispose do repositório
        /// </summary>
        public void Dispose()
        {
            _bancoRepository.Dispose();
        }

        /// <summary>
        /// Verifica se o número do Banco a ser inserido já existe no sistema
        /// </summary>
        /// <param name="numeroDoBanco">Número a ser inserido</param>
        /// <exception cref="Exception"></exception>
        public void VerificarSeNumeroDoBancoExiste(string numeroDoBanco)
        {
            var bancoExistenteComEsseNumero = _bancoRepository.Get(b => b.Numero == numeroDoBanco).Result.FirstOrDefault();

            if (!string.IsNullOrEmpty(numeroDoBanco) && bancoExistenteComEsseNumero is not null)
            {
                throw new Exception($"Já existe um Banco com esse número: {JsonConvert.SerializeObject(bancoExistenteComEsseNumero)}");
            }
        }
    }
}

