namespace Cpnucleo.Application.Commands;

public sealed class UpdateImpedimentoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateImpedimentoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateImpedimentoTarefaCommand request, CancellationToken cancellationToken)
    {
        var impedimentoTarefa = await context.ImpedimentoTarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (impedimentoTarefa is null)
        {
            return OperationResult.NotFound;
        }

        impedimentoTarefa = ImpedimentoTarefa.Update(impedimentoTarefa, request.Descricao, request.IdTarefa, request.IdImpedimento);
        context.ImpedimentoTarefas.Update(impedimentoTarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
