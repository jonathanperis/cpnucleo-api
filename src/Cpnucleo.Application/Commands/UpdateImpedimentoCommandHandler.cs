namespace Cpnucleo.Application.Commands;

public sealed class UpdateImpedimentoCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateImpedimentoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateImpedimentoCommand request, CancellationToken cancellationToken)
    {
        var impedimento = await context.Impedimentos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (impedimento is null)
        {
            return OperationResult.NotFound;
        }

        impedimento = Impedimento.Update(impedimento, request.Nome);
        context.Impedimentos.Update(impedimento);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
