namespace Cpnucleo.Application.Commands;

public sealed class RemoveRecursoCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveRecursoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveRecursoCommand request, CancellationToken cancellationToken)
    {
        var recurso = await context.Recursos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (recurso is null)
        {
            return OperationResult.NotFound;
        }

        recurso = Recurso.Remove(recurso);
        context.Recursos.Update(recurso); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
