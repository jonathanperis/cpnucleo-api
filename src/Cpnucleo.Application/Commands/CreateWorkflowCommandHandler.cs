namespace Cpnucleo.Application.Commands;

public sealed class CreateWorkflowCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateWorkflowCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = Workflow.Create(request.Nome, request.Ordem);
        context.Workflows.Add(workflow);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
