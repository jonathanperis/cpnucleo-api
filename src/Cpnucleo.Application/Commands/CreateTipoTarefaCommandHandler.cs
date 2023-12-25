namespace Cpnucleo.Application.Commands;

public sealed class CreateTipoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTipoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateTipoTarefaCommand request, CancellationToken cancellationToken)
    {
        var tipoTarefa = TipoTarefa.Create(request.Nome, request.Image);
        context.TipoTarefas.Add(tipoTarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
