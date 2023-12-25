namespace Cpnucleo.Application.Commands;

public sealed class UpdateWorkflowCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateWorkflowCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await context.Workflows
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (workflow is null)
        {
            return OperationResult.NotFound;
        }

        workflow = Workflow.Update(workflow, request.Nome, request.Ordem);
        context.Workflows.Update(workflow);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
