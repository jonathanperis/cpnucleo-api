namespace Cpnucleo.Application.Commands;

public sealed class RemoveImpedimentoCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveImpedimentoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveImpedimentoCommand request, CancellationToken cancellationToken)
    {
        var impedimento = await context.Impedimentos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (impedimento is null)
        {
            return OperationResult.NotFound;
        }

        impedimento = Impedimento.Remove(impedimento);
        context.Impedimentos.Update(impedimento); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
