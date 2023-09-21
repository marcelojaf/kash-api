using AutoMapper;
using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Business.Models.Validations.ErrorResponse;
using Kash.Api.Business.Models.Validations.Exceptions;
using Kash.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kash.Api.Controllers
{
    /// <summary>
    /// Controller de Tipo de Conta
    /// </summary>
    [ApiVersion("1.0")]
    public class TiposContaController : KashBaseController
    {
        private readonly ITipoContaRepository _tipoContaRepository;
        private readonly ITipoContaService _tipoContaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor padrão de Controller
        /// </summary>
        /// <param name="tipoContaRepository"></param>
        /// <param name="tipoContaService"></param>
        /// <param name="mapper"></param>
        /// <param name="user"></param>
        public TiposContaController(ITipoContaRepository tipoContaRepository,
            ITipoContaService tipoContaService,
            IMapper mapper,
            IUser user) : base(user)
        {
            _tipoContaRepository = tipoContaRepository;
            _tipoContaService = tipoContaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista com todos os Tipos de Conta cadastrados.
        /// </summary>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi obtido do banco de dados e pode ser enviado para o caller.
        /// Lista de objetos do tipo <see cref="TipoContaDTO"/> é retornada.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TipoContaDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tiposConta = _mapper.Map<List<TipoContaDTO>>(await _tipoContaRepository.GetAll());

                return Ok(tiposConta.OrderBy(b => b.Nome).ToList());
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
        /// Insere um Tipo de Conta
        /// </summary>
        /// <param name="tipoContaDTO">Objeto com o Tipo de Conta a ser inserido</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="201">
        /// Quando um código 201 é retornado, significa que o dado foi inserido no banco de dados com sucesso.
        /// Objeto do tipo <see cref="TipoContaDTO"/> é retornado.
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
        [ProducesResponseType(typeof(TipoContaDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Insert([FromBody] TipoContaDTO tipoContaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var tipoConta = _mapper.Map<TipoConta>(tipoContaDTO);
                await _tipoContaService.Insert(tipoConta);

                tipoContaDTO.Id = tipoConta.Id;

                return CreatedAtAction(nameof(GetById), new { Id = tipoContaDTO.Id }, tipoContaDTO);
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
        /// Atualiza um Tipo de Conta
        /// </summary>
        /// <param name="tipoContaDTO">Objeto com o Tipo de Conta a ser atualizado</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi atualizado no banco de dados com sucesso.
        /// Objeto do tipo <see cref="TipoContaDTO"/> é retornado.
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
        [ProducesResponseType(typeof(TipoContaDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] TipoContaDTO tipoContaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var tipoConta = _mapper.Map<TipoConta>(tipoContaDTO);
                await _tipoContaService.Update(tipoConta);

                return Ok(tipoContaDTO);
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
        /// Busca um Tipo de Conta por Id
        /// </summary>
        /// <param name="id">Id do Tipo de Conta a ser buscado</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi atualizado no banco de dados com sucesso.
        /// Objeto do tipo <see cref="TipoContaDTO"/> é retornado.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 
        /// </response>
        /// <response code="404">
        /// Não foi encontrado nenhum Tipo de Conta com o Id informado.
        /// </response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TipoContaDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var tipoConta = await _tipoContaRepository.GetById(id);

                if (tipoConta == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<TipoContaDTO>(tipoConta));
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
        /// Deleta um Tipo de Conta
        /// </summary>
        /// <param name="id">Id do Tipo de Conta a ser deletado</param>
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
        /// Não foi encontrado nenhum Tipo de Conta com o Id informado.
        /// </response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var tipoConta = await _tipoContaRepository.GetById(id);

                if (tipoConta == null)
                {
                    return NotFound();
                }

                await _tipoContaRepository.Delete(id);

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
