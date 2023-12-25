namespace Cpnucleo.Application.Queries;

public sealed class GetWorkflowQueryHandler(IApplicationDbContext context) : IRequestHandler<GetWorkflowQuery, GetWorkflowViewModel>
{
    public async ValueTask<GetWorkflowViewModel> Handle(GetWorkflowQuery request, CancellationToken cancellationToken)
    {
        var workflow = await context.Workflows
            .AsNoTracking()
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (workflow is null)
        {
            return new GetWorkflowViewModel(OperationResult.NotFound);
        }

        return new GetWorkflowViewModel(OperationResult.Success, workflow);
    }
}