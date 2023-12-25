namespace Cpnucleo.Application.Commands;

public sealed class UpdateProjetoCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateProjetoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = await context.Projetos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (projeto is null)
        {
            return OperationResult.NotFound;
        }

        projeto = Projeto.Update(projeto, request.Nome, request.IdSistema);
        context.Projetos.Update(projeto);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
