namespace Cpnucleo.Application.Commands;

public sealed class UpdateApontamentoCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateApontamentoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateApontamentoCommand request, CancellationToken cancellationToken)
    {
        var apontamento = await context.Apontamentos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (apontamento is null)
        {
            return OperationResult.NotFound;
        }

        apontamento = Apontamento.Update(apontamento, request.Descricao, request.DataApontamento, request.QtdHoras, request.IdTarefa, request.IdRecurso);
        context.Apontamentos.Update(apontamento);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
