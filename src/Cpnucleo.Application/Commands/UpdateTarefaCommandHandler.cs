namespace Cpnucleo.Application.Commands;

public sealed class UpdateTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tarefa is null)
        {
            return OperationResult.NotFound;
        }

        tarefa = Tarefa.Update(tarefa,
                                                   request.Nome,
                                                   request.DataInicio,
                                                   request.DataTermino,
                                                   request.QtdHoras,
                                                   request.Detalhe,
                                                   request.IdProjeto,
                                                   request.IdWorkflow,
                                                   request.IdRecurso,
                                                   request.IdTipoTarefa);
        context.Tarefas.Update(tarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
