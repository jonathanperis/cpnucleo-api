namespace Cpnucleo.Application.Commands;

public sealed class CreateTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = Tarefa.Create(request.Nome,
                                                   request.DataInicio,
                                                   request.DataTermino,
                                                   request.QtdHoras,
                                                   request.Detalhe,
                                                   request.IdProjeto,
                                                   request.IdWorkflow,
                                                   request.IdRecurso,
                                                   request.IdTipoTarefa);
        context.Tarefas.Add(tarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
