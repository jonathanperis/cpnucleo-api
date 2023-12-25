namespace Cpnucleo.Application.Commands;

public sealed class RemoveTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tarefa is null)
        {
            return OperationResult.NotFound;
        }

        tarefa = Tarefa.Remove(tarefa);
        context.Tarefas.Update(tarefa); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
