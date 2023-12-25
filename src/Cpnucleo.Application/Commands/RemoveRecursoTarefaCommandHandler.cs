namespace Cpnucleo.Application.Commands;

public sealed class RemoveRecursoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveRecursoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveRecursoTarefaCommand request, CancellationToken cancellationToken)
    {
        var recursoTarefa = await context.RecursoTarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (recursoTarefa is null)
        {
            return OperationResult.NotFound;
        }

        recursoTarefa = RecursoTarefa.Remove(recursoTarefa);
        context.RecursoTarefas.Update(recursoTarefa); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
