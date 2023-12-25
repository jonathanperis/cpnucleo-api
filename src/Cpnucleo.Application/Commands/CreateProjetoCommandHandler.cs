namespace Cpnucleo.Application.Commands;

public sealed class CreateProjetoCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateProjetoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = Projeto.Create(request.Nome, request.IdSistema);
        context.Projetos.Add(projeto);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
