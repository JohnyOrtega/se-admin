using AutoMapper;
using Core.Dtos.HistoricoMapeamentoDto;
using Core.Exceptions;
using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class HistoricoMapeamentoService(IHistoricoMapeamentoRepository historicoMapeamentoRepository, IMapper mapper) : IHistoricoMapeamentoService
{
    private readonly IHistoricoMapeamentoRepository _historicoMapeamentoRepository = historicoMapeamentoRepository;

    public async Task<HistoricoMapeamento> Create(HistoricoMapeamento historicoMapeamento)
    {
        var existsHistoricoMapeamento = await _historicoMapeamentoRepository.ExistsAsync(historicoMapeamento.Id);
        if (existsHistoricoMapeamento)
        {
            throw AlreadyExistsException.For("HistoricoMapeamento", historicoMapeamento.Id);
        }

        var historicoMapeamentoCreated = await _historicoMapeamentoRepository.AddAsync(historicoMapeamento);
        return historicoMapeamentoCreated;
    }

    public async Task DeleteAsync(Guid id)
    {
        var existsHistoricoMapeamento = await _historicoMapeamentoRepository.ExistsAsync(id);
        if (!existsHistoricoMapeamento)
        {
            throw NotFoundException.For("HistoricoMapeador", id);
        }

        await _historicoMapeamentoRepository.DeleteAsync(id);
    }

    public async Task<HistoricoMapeamento> UpdateAsync(HistoricoMapeamentoUpdateDto historicoMapeamentoUpdateDto)
    {
        var historicoMapeamento = await _historicoMapeamentoRepository.GetByIdAsync(historicoMapeamentoUpdateDto.Id) ?? throw NotFoundException.For("HistoricoMapeamento", historicoMapeamentoUpdateDto.Id);
        mapper.Map(historicoMapeamentoUpdateDto, historicoMapeamento);

        var historicoMapeamentoUpdated = await _historicoMapeamentoRepository.UpdateAsync(historicoMapeamento);
        return historicoMapeamentoUpdated;
    }

    public async Task<HistoricoMapeamento> GetById(Guid id)
    {
        return await _historicoMapeamentoRepository.GetByIdAsync(id);
    }
}