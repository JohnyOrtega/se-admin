using Core.Models;
using Core.Repositories.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class HistoricoMapeamentoRepository(AppDbContext context) : Repository<HistoricoMapeamento>(context), IHistoricoMapeamentoRepository
{
}