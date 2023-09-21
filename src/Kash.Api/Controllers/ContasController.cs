using AutoMapper;
using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Business.Models.Validations.ErrorResponse;
using Kash.Api.Business.Models.Validations.Exceptions;
using Kash.Api.Business.Services;
using Kash.Api.Data.Repository;
using Kash.Api.DTO;
using Kash.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kash.Api.Controllers
{
    /// <summary>
    /// Controller de Conta
    /// </summary>
    [ApiVersion("1.0")]
    public class ContasController : KashBaseController
    {
        private readonly IContaRepository _contaRepository;
        private readonly IContaService _contaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor padrão de Controller
        /// </summary>
        /// <param name="contaRepository"></param>
        /// <param name="contaService"></param>
        /// <param name="mapper"></param>
        /// <param name="user"></param>
        public ContasController(IContaRepository contaRepository, 
            IContaService contaService, 
            IMapper mapper,
            IUser user) : base(user)
        {
            _contaRepository = contaRepository;
            _contaService = contaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista com todos os Bancos cadastrados.
        /// </summary>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi obtido do banco de dados e pode ser enviado para o caller.
        /// Lista de objetos do tipo <see cref="ContaDTO"/> é retornada.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ContaDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contas = _mapper.Map<List<ContaDTO>>(await _contaRepository.GetTodasContas());

                return Ok(contas);
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
        /// Insere uma Conta
        /// </summary>
        /// <param name="contaDTO">Objeto com a Conta a ser inserida</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="201">
        /// Quando um código 201 é retornado, significa que o dado foi inserido no banco de dados com sucesso.
        /// Objeto do tipo <see cref="ContaDTO"/> é retornado.
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
        [ProducesResponseType(typeof(ContaDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Insert([FromBody] ContaDTO contaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var conta = _mapper.Map<Conta>(contaDTO);
                await _contaService.Insert(conta);

                contaDTO.Id = conta.Id;

                return CreatedAtAction(nameof(GetById), new { Id = contaDTO.Id }, contaDTO);
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
        /// Atualiza uma Conta
        /// </summary>
        /// <param name="contaDTO">Objeto com a Conta a ser atualizada</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi atualizado no banco de dados com sucesso.
        /// Objeto do tipo <see cref="ContaDTO"/> é retornado.
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
        [ProducesResponseType(typeof(ContaDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] ContaDTO contaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var conta = _mapper.Map<Conta>(contaDTO);
                await _contaService.Update(conta);

                return Ok(contaDTO);
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
        /// Busca uma Conta por Id
        /// </summary>
        /// <param name="id">Id da Conta a ser buscada</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi atualizado no banco de dados com sucesso.
        /// Objeto do tipo <see cref="ContaDTO"/> é retornado.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 
        /// </response>
        /// <response code="404">
        /// Não foi encontrado nenhuma Conta com o Id informado.
        /// </response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ContaDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var conta = await _contaRepository.GetById(id);

                if (conta == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ContaDTO>(conta));
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
        /// Deleta uma Conta
        /// </summary>
        /// <param name="id">Id da Conta a ser deletada</param>
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
                var conta = await _contaRepository.GetById(id);

                if (conta == null)
                {
                    return NotFound();
                }

                await _contaRepository.Delete(id);

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
