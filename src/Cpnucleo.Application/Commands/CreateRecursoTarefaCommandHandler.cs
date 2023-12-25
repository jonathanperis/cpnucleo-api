namespace Cpnucleo.Application.Commands;

public sealed class CreateRecursoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateRecursoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateRecursoTarefaCommand request, CancellationToken cancellationToken)
    {
        var recursoTarefa = RecursoTarefa.Create(request.IdTarefa, request.IdRecurso);
        context.RecursoTarefas.Add(recursoTarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
