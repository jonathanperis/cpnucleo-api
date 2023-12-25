namespace Cpnucleo.Application.Commands;

public sealed class CreateRecursoProjetoCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateRecursoProjetoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateRecursoProjetoCommand request, CancellationToken cancellationToken)
    {
        var recursoProjeto = RecursoProjeto.Create(request.IdProjeto, request.IdRecurso);
        context.RecursoProjetos.Add(recursoProjeto);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
