using Kash.Api.Business.Models;
using System.Linq.Expressions;

namespace Kash.Api.Business.Interfaces
{
    /// <summary>
    /// Interface padrão para um Repositório
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        /// <summary>
        /// Inserir uma entidade
        /// </summary>
        /// <param name="entity">Entidade com dados a serem inseridos</param>
        /// <returns></returns>
        Task Insert(TEntity entity);

        /// <summary>
        /// Retorna uma entidade filtrada por Id
        /// </summary>
        /// <param name="id">Id da entidade a ser retornada</param>
        /// <returns></returns>
        Task<TEntity> GetById(Guid id);

        /// <summary>
        /// Retorna uma lista com todos os registros de uma entidade
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAll();

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade com dados a serem atualizados</param>
        /// <returns></returns>
        Task Update(TEntity entity);

        /// <summary>
        /// Remove uma entidade
        /// </summary>
        /// <param name="id">Id da entidade a ser removida</param>
        /// <returns></returns>
        Task Delete(Guid id);

        /// <summary>
        /// Retorna uma entidade filtrada por uma expressão customizada
        /// </summary>
        /// <param name="predicate">Filtro customizado</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Salva as alterações feitas no context
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChanges();
    }
}

