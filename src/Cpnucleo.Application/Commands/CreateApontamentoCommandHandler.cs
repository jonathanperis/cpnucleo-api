namespace Cpnucleo.Application.Commands;

public sealed class CreateApontamentoCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateApontamentoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateApontamentoCommand request, CancellationToken cancellationToken)
    {
        var apontamento = Apontamento.Create(request.Descricao, request.DataApontamento, request.QtdHoras, request.IdTarefa, request.IdRecurso);
        context.Apontamentos.Add(apontamento);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
