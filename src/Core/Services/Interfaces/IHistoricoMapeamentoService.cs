using Core.Dtos.HistoricoMapeamentoDto;
using Core.Models;

namespace Core.Services.Interfaces;

public interface IHistoricoMapeamentoService
{
    Task<HistoricoMapeamento> GetById(Guid id);
    Task<HistoricoMapeamento> Create(HistoricoMapeamento historicoMapeamento);
    Task<HistoricoMapeamento> UpdateAsync(HistoricoMapeamentoUpdateDto mapeador);
    Task DeleteAsync(Guid id);
}