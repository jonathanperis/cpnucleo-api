namespace Cpnucleo.Application.Commands;

public sealed class RemoveImpedimentoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveImpedimentoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveImpedimentoTarefaCommand request, CancellationToken cancellationToken)
    {
        var impedimentoTarefa = await context.ImpedimentoTarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (impedimentoTarefa is null)
        {
            return OperationResult.NotFound;
        }

        impedimentoTarefa = ImpedimentoTarefa.Remove(impedimentoTarefa);
        context.ImpedimentoTarefas.Update(impedimentoTarefa); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
