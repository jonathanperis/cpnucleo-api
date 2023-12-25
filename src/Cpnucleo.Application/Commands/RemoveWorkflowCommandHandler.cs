namespace Cpnucleo.Application.Commands;

public sealed class RemoveWorkflowCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveWorkflowCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await context.Workflows
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (workflow is null)
        {
            return OperationResult.NotFound;
        }

        workflow = Workflow.Remove(workflow);
        context.Workflows.Update(workflow); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
