using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models.Validations.ErrorResponse;
using Kash.Api.Business.Models.Validations.Exceptions;
using Kash.Api.DTO;
using Kash.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Kash.Api.Controllers
{
    /// <summary>
    /// Controller de Autenticação
    /// </summary>
    [ApiVersion("1.0")]
    public class AuthController : KashBaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Lista de Erros do Identity
        /// </summary>
        public IEnumerable<IdentityError> IdentityErrors { get; set; }

        /// <summary>
        /// Construtor com Identity
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="appSettings"></param>
        /// <param name="user"></param>
        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings,
            IUser user) : base(user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Registra um novo Usuário no sistema
        /// </summary>
        /// <param name="userDTO">Objeto com o Usuário a ser inserido</param>
        /// <returns>Retorna um IActionResult</returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi inserido no banco de dados com sucesso.
        /// Objeto do tipo <see cref="UserDTO"/> é retornado.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 2. CodigoDeErro = 0x00000002 -> Modelo é inválido para ser inserido
        /// 
        /// </response>
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var user = new IdentityUser()
                {
                    UserName = userDTO.Email,
                    Email = userDTO.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok(await GerarJwt(userDTO.Email));
                }

                if (result.Errors.Any())
                {
                    IdentityErrors = result.Errors;
                    throw new InvalidUserDataException();
                }

                throw new Exception();
            }
            catch (InvalidUserDataException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = 0x00000002,
                    Exception = ex.Message,
                    MessageList = IdentityErrors.Select(i => i.Description).ToArray()
                });
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
        /// Realiza o Login no sistema
        /// </summary>
        /// <param name="loginUserDTO"></param>
        /// <returns></returns>
        /// <response code="200">
        /// Quando um código 200 é retornado, significa que o dado foi obtido do banco de dados e pode ser enviado para o caller.
        /// </response>
        /// <response code="400">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 1. CodigoDeErro = 0x00000001 -> Internal Server Error
        /// 2. CodigoDeErro = 0x00000002 -> Modelo é inválido para ser inserido
        /// 
        /// </response>
        /// <response code="401">
        /// ### Possíveis erros de API
        /// Ocorreu um erro no request. Possíveis `CodigoDeErro`
        ///
        /// 3. CodigoDeErro = 0x00000003 -> Usuário temporariamente bloqueado por tentativas inválidas
        /// 4. CodigoDeErro = 0x00000004 -> Usuário ou senha inválidos
        /// 
        /// </response>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ModelStateException();

                var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Email, loginUserDTO.Password, false, true);

                if (result.Succeeded)
                {
                    return Ok(await GerarJwt(loginUserDTO.Email));
                }

                if (result.IsLockedOut)
                {
                    throw new UserLockedOutException();
                }

                throw new InvalidCredentialsException();
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    Code = 0x00000004,
                    Exception = ex.Message,
                    MessageList = new string[] { "Usuário ou senha inválidas" }
                });
            }
            catch (UserLockedOutException ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    Code = 0x00000003,
                    Exception = ex.Message,
                    MessageList = new string[] { "Usuário temporariamente bloqueado por tentativas inválidas" }
                });
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

        private async Task<LoginResponseDTO> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoEmHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            string encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseDTO
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoEmHoras).TotalSeconds,
                UserToken = new UserTokenDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDTO { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
