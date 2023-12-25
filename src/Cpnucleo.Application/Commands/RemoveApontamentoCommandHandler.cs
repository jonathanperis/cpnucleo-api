namespace Cpnucleo.Application.Commands;

public sealed class RemoveApontamentoCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveApontamentoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveApontamentoCommand request, CancellationToken cancellationToken)
    {
        var apontamento = await context.Apontamentos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (apontamento is null)
        {
            return OperationResult.NotFound;
        }

        apontamento = Apontamento.Remove(apontamento);
        context.Apontamentos.Update(apontamento); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
