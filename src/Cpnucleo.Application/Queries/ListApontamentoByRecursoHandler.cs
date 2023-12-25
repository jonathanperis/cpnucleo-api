namespace Cpnucleo.Application.Queries;

public sealed class ListApontamentoByRecursoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListApontamentoByRecursoQuery, ListApontamentoByRecursoViewModel>
{
    public async ValueTask<ListApontamentoByRecursoViewModel> Handle(ListApontamentoByRecursoQuery request, CancellationToken cancellationToken)
    {
        var apontamentos = await context.Apontamentos
            .AsNoTracking()
            .Include(x => x.Tarefa)
            .Where(x => x.IdRecurso == request.IdRecurso && x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (apontamentos is null)
        {
            return new ListApontamentoByRecursoViewModel(OperationResult.NotFound);
        }

        return new ListApontamentoByRecursoViewModel(OperationResult.Success, apontamentos);
    }
}