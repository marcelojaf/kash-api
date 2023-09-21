using Kash.Api.Business.Models;
using Kash.Api.Data.Context;

namespace Kash.Api.Data
{
    /// <summary>
    /// Classe inicializadora do Context
    /// </summary>
    public class DBInitializer : IDBInitializer
    {
        private readonly KashDbContext _context;

        /// <summary>
        /// Construtor da classe DBInitializer
        /// </summary>
        /// <param name="context">Contexto a ser inicializado</param>
        public DBInitializer(KashDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inicializar o Context
        /// </summary>
        public void Initialize()
        {
            _context.Database.EnsureCreated();

            SeedBanco();
            SeedTipoConta();
            SeedConta();
        }

        /// <summary>
        /// Inicializa o banco de dados com alguns Bancos
        /// </summary>
        private void SeedBanco()
        {
            if (_context.Bancos == null)
                return;

            if (!_context.Bancos.Any())
            {
                var bancos = new Banco[]
                {
                    new Banco
                    {
                        Nome = "Banco Bradesco S.A.",
                        Numero = "237"
                    },
                    new Banco
                    {
                        Nome = "Banco Itaú S.A.",
                        Numero = "341"
                    },
                    new Banco
                    {
                        Nome = "Caixa Econômica Federal",
                        Numero = "104"
                    },
                    new Banco
                    {
                        Nome = "Banco do Brasil S.A.",
                        Numero = "001"
                    },
                    new Banco
                    {
                        Nome = "Nu Pagamentos S.A.",
                        Numero = "260"
                    },
                };

                foreach (Banco b in bancos)
                {
                    _context.Bancos.Add(b);
                }

                _context.SaveChangesAsync().Wait();
            }
        }

        /// <summary>
        /// Inicializa o banco de dados com alguns Tipos de Conta
        /// </summary>
        private void SeedTipoConta()
        {
            if (_context.TiposConta == null)
                return;

            if (!_context.TiposConta.Any())
            {
                var tiposConta = new TipoConta[]
                {
                    new TipoConta
                    {
                        Nome = "Conta Corrente"
                    },
                    new TipoConta
                    {
                        Nome = "Conta Poupança"
                    }
                };

                foreach (TipoConta tc in tiposConta)
                {
                    _context.TiposConta.Add(tc);
                }

                _context.SaveChangesAsync().Wait();
            }
        }

        /// <summary>
        /// Inicializa o banco de dados com algumas Contas
        /// </summary>
        private void SeedConta()
        {
            if (_context.Contas == null)
                return;

            if (!_context.Contas.Any())
            {
                var contas = new Conta[]
                {
                    new Conta
                    {
                        Nome = "Conta Bradesco",
                        Numero = "123445-8",
                        Agencia = "2374",
                        Saldo = 250.55M,
                        TipoConta = _context.TiposConta.Where(tc => tc.Nome.Contains("Corrente")).FirstOrDefault(),
                        Banco = _context.Bancos.Where(b => b.Numero == "237").FirstOrDefault()
                    },
                    new Conta
                    {
                        Nome = "Poupança Banco do Brasil",
                        Numero = "00681237-6",
                        Agencia = "328-x",
                        Saldo = 420,
                        TipoConta = _context.TiposConta.Where(tc => tc.Nome.Contains("Poupança")).FirstOrDefault(),
                        Banco = _context.Bancos.Where(b => b.Numero == "001").FirstOrDefault()
                    }
                };

                foreach (Conta c in contas)
                {
                    _context.Contas.Add(c);
                }

                _context.SaveChangesAsync().Wait();
            }
        }
    }
}