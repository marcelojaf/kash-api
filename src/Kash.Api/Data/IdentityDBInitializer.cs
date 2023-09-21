using Microsoft.AspNetCore.Identity;

namespace Kash.Api.Data
{
    /// <summary>
    /// Classe inicializadora do IdentityContext
    /// </summary>
    public class IdentityDBInitializer : IIdentityDbInitializer
    {
        private readonly KashIdentityDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Construtor da classe IdentityDBInitializer
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        public IdentityDBInitializer(KashIdentityDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Inicializar o IdentityContext
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Initialize()
        {
            _context.Database.EnsureCreated();

            SeedUser();
        }

        /// <summary>
        /// Inicializa o banco de dados com um Usuário
        /// </summary>
        private void SeedUser()
        {
            if (_context.Users == null)
                return;

            if (!_context.Users.Any())
            {
                var user = new IdentityUser()
                {
                    Email = "marcelo@cklabs.dev",
                    UserName = "marcelo@cklabs.dev",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, "P@ssword1").Wait();
            }
        }
    }
}
