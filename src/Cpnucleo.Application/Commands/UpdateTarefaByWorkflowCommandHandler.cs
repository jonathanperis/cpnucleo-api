namespace Cpnucleo.Application.Commands;

public sealed class UpdateTarefaByWorkflowCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTarefaByWorkflowCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateTarefaByWorkflowCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tarefa is null)
        {
            return OperationResult.NotFound;
        }

        tarefa = Tarefa.Update(tarefa, request.IdWorkflow);
        context.Tarefas.Update(tarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
