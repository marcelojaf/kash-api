using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kash.Api.Data.Repository
{
    /// <summary>
    /// Classe base de um repositório
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Contexto do sistema
        /// </summary>
        protected readonly KashDbContext _context;
        /// <summary>
        /// DbSet da entidade
        /// </summary>
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Construtor do repositório com Context
        /// </summary>
        /// <param name="context"></param>
        public Repository(KashDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Retorna uma entidade filtrada por uma expressão customizada
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Retorna uma entidade filtrada por Id
        /// </summary>
        /// <param name="id">Id da entidade a ser retornada</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.Where(e => e.Ativo && e.Id == id).FirstAsync();
        }

        /// <summary>
        /// Retorna uma lista com todos os registros de uma entidade
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.Where(e => e.Ativo).ToListAsync();
        }

        /// <summary>
        /// Insere uma entidade
        /// </summary>
        /// <param name="entity">Entidade com dados a serem inseridos</param>
        /// <returns></returns>
        public async Task Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade com dados a serem atualizados</param>
        /// <returns></returns>
        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        /// <summary>
        /// Remove uma entidade
        /// </summary>
        /// <param name="id">Id da entidade a ser removida</param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            var entity = await _dbSet.Where(e => e.Id == id).FirstAsync();
            entity.Ativo = false;
            _dbSet.Update(entity);
            await SaveChanges();
        }

        /// <summary>
        /// Salva as alterações feitas no context
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Executa o Dispose no Context
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

