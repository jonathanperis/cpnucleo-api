namespace Cpnucleo.Application.Commands;

public sealed class RemoveRecursoProjetoCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveRecursoProjetoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveRecursoProjetoCommand request, CancellationToken cancellationToken)
    {
        var recursoProjeto = await context.RecursoProjetos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (recursoProjeto is null)
        {
            return OperationResult.NotFound;
        }

        recursoProjeto = RecursoProjeto.Remove(recursoProjeto);
        context.RecursoProjetos.Update(recursoProjeto); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
