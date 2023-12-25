namespace Cpnucleo.Application.Commands;

public sealed class UpdateRecursoProjetoCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateRecursoProjetoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateRecursoProjetoCommand request, CancellationToken cancellationToken)
    {
        var recursoProjeto = await context.RecursoProjetos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (recursoProjeto is null)
        {
            return OperationResult.NotFound;
        }

        recursoProjeto = RecursoProjeto.Update(recursoProjeto, request.IdRecurso, request.IdProjeto);
        context.RecursoProjetos.Update(recursoProjeto);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
