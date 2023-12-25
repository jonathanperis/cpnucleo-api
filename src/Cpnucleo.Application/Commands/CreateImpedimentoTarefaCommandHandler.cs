namespace Cpnucleo.Application.Commands;

public sealed class CreateImpedimentoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateImpedimentoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateImpedimentoTarefaCommand request, CancellationToken cancellationToken)
    {
        var impedimentoTarefa = ImpedimentoTarefa.Create(request.Descricao, request.IdTarefa, request.IdImpedimento);
        context.ImpedimentoTarefas.Add(impedimentoTarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
