using AutoMapper;
using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Business.Models.Validations.ErrorResponse;
using Kash.Api.Business.Models.Validations.Exceptions;
using Kash.Api.DTO;
using Kash.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kash.Api.Controllers
{
    /// <summary>
    /// Controller de Banco
    /// </summary>
    [ApiVersion("1.0")]
    public class BancosController : KashBaseController
    {
        private readonly IBancoRepository _bancoRepository;
        private readonly IBancoService _bancoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor padrão de Controller
        /// </summary>
        /// <param name="bancoRepository"></param>
        /// <param name="bancoService"></param>
        /// <param name="mapper"></param>
        /// <param name="user"></param>
        public BancosController(IBancoRepository bancoRepository,
            IBancoService bancoService,
            IMapper mapper,
            IUser user) : base(user)
        {
            _bancoRepository = bancoRepository;
            _bancoService = bancoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista com todos os Bancos cadastrados.
        /// </summary>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi obtido do banco de dados e pode ser enviado para o caller.
        /// Lista de objetos do tipo <see cref="BancoDTO"/> é retornada.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(List<BancoDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var bancos = _mapper.Map<List<BancoDTO>>(await _bancoRepository.GetAll());

                return Ok(bancos.OrderBy(b => b.Nome).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000001,
                    Exception = ex.Message,
                    MessageList = new string[] { "Erro interno do servidor" }
                });
            }
        }

        /// <summary>
        /// Insere um Banco
        /// </summary>
        /// <param name="bancoDTO">Objeto com o Banco a ser inserido</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="201">
        /// Quando um código 201 é retornado, significa que o dado foi inserido no banco de dados com sucesso.
        /// Objeto do tipo <see cref="BancoDTO"/> é retornado.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 2. CodigoDeErro = 0x00000002 -> Modelo é inválido para ser inserido
        /// 
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BancoDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Insert([FromBody] BancoDTO bancoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var banco = _mapper.Map<Banco>(bancoDTO);
                await _bancoService.Insert(banco);

                bancoDTO.Id = banco.Id;

                return CreatedAtAction(nameof(GetById), new { Id = bancoDTO.Id }, bancoDTO);
            }
            catch (ModelStateException ex)
            {
                var modelStateErrors = GetModelStateErrors(ModelState);
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000002,
                    Exception = ex.Message,
                    MessageList = modelStateErrors.ToArray()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000001,
                    Exception = ex.Message,
                    MessageList = new string[] { "Erro interno do servidor" }
                });
            }
        }

        /// <summary>
        /// Atualiza um Banco
        /// </summary>
        /// <param name="bancoDTO">Objeto com o Banco a ser atualizado</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi atualizado no banco de dados com sucesso.
        /// Objeto do tipo <see cref="BancoDTO"/> é retornado.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 2. CodigoDeErro = 0x00000002 -> Modelo é inválido para ser atualizado
        /// 
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BancoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] BancoDTO bancoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var banco = _mapper.Map<Banco>(bancoDTO);
                await _bancoService.Update(banco);

                return Ok(bancoDTO);
            }
            catch (ModelStateException ex)
            {
                var modelStateErrors = GetModelStateErrors(ModelState);
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000002,
                    Exception = ex.Message,
                    MessageList = modelStateErrors.ToArray()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000001,
                    Exception = ex.Message,
                    MessageList = new string[] { "Erro interno do servidor" }
                });
            }
        }

        /// <summary>
        /// Busca um Banco por Id
        /// </summary>
        /// <param name="id">Id do Banco a ser buscado</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi atualizado no banco de dados com sucesso.
        /// Objeto do tipo <see cref="BancoDTO"/> é retornado.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 
        /// </response>
        /// <response code="404">
        /// Não foi encontrado nenhum Banco com o Id informado.
        /// </response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BancoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var banco = await _bancoRepository.GetById(id);

                if (banco == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<BancoDTO>(banco));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000001,
                    Exception = ex.Message,
                    MessageList = new string[] { "Erro interno do servidor" }
                });
            }
        }

        /// <summary>
        /// Deleta um Banco
        /// </summary>
        /// <param name="id">Id do Banco a ser deletado</param>
        /// <returns>Retorna um codigo 200</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi desativado no banco de dados com sucesso.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 
        /// </response>
        /// <response code="404">
        /// Não foi encontrado nenhum Banco com o Id informado.
        /// </response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var banco = await _bancoRepository.GetById(id);

                if (banco == null)
                {
                    return NotFound();
                }

                await _bancoRepository.Delete(id);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000001,
                    Exception = ex.Message,
                    MessageList = new string[] { "Erro interno do servidor" }
                });
            }
        }
    }
}

