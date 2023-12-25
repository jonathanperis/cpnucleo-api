namespace Cpnucleo.Application.Queries;

public sealed class ListWorkflowQueryHandler(IApplicationDbContext context) : IRequestHandler<ListWorkflowQuery, ListWorkflowViewModel>
{
    public async ValueTask<ListWorkflowViewModel> Handle(ListWorkflowQuery request, CancellationToken cancellationToken)
    {
        var workflows = await context.Workflows
            .AsNoTracking()
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (workflows is null)
        {
            return new ListWorkflowViewModel(OperationResult.NotFound);
        }

        var colunas = context.Workflows.Where(x => x.Ativo).Count();

        workflows.ForEach(x => x.TamanhoColuna = Workflow.GetTamanhoColuna(colunas));

        return new ListWorkflowViewModel(OperationResult.Success, workflows);
    }
}