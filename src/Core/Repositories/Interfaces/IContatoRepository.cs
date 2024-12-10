using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IContatoRepository : IRepository<Contato>
{
    IQueryable<Contato> GetWithFilters(ContatoFilterParams filters);
}