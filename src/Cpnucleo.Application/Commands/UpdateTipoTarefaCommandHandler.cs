namespace Cpnucleo.Application.Commands;

public sealed class UpdateTipoTarefaCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTipoTarefaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateTipoTarefaCommand request, CancellationToken cancellationToken)
    {
        var tipoTarefa = await context.TipoTarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tipoTarefa is null)
        {
            return OperationResult.NotFound;
        }

        tipoTarefa = TipoTarefa.Update(tipoTarefa, request.Nome, request.Image);
        context.TipoTarefas.Update(tipoTarefa);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
