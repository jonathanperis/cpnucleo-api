namespace Cpnucleo.Application.Commands;

public sealed class CreateImpedimentoCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateImpedimentoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateImpedimentoCommand request, CancellationToken cancellationToken)
    {
        var impedimento = Impedimento.Create(request.Nome);
        context.Impedimentos.Add(impedimento);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
