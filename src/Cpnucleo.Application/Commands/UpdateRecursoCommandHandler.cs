namespace Cpnucleo.Application.Commands;

public sealed class UpdateRecursoCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateRecursoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateRecursoCommand request, CancellationToken cancellationToken)
    {
        var recurso = await context.Recursos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (recurso is null)
        {
            return OperationResult.NotFound;
        }

        recurso = Recurso.Update(recurso, request.Nome, request.Senha);
        context.Recursos.Update(recurso);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
