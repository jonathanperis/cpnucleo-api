namespace Cpnucleo.Application.Commands;

public sealed class UpdateRecursoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateRecursoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateRecursoTarefaCommand request, CancellationToken cancellationToken)
    {
        var recursoTarefa = await context.RecursoTarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (recursoTarefa is null)
        {
            return OperationResult.NotFound;
        }

        recursoTarefa = RecursoTarefa.Update(recursoTarefa, request.IdRecurso, request.IdTarefa);
        context.RecursoTarefas.Update(recursoTarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
