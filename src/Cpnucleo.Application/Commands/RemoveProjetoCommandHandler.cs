namespace Cpnucleo.Application.Commands;

public sealed class RemoveProjetoCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveProjetoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = await context.Projetos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (projeto is null)
        {
            return OperationResult.NotFound;
        }

        projeto = Projeto.Remove(projeto);
        context.Projetos.Update(projeto); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
