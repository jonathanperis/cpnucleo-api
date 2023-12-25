namespace Cpnucleo.Application.Commands;

public sealed class RemoveTipoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveTipoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveTipoTarefaCommand request, CancellationToken cancellationToken)
    {
        var tipoTarefa = await context.TipoTarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tipoTarefa is null)
        {
            return OperationResult.NotFound;
        }

        tipoTarefa = TipoTarefa.Remove(tipoTarefa);
        context.TipoTarefas.Update(tipoTarefa); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
