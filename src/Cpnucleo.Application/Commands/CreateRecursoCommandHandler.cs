namespace Cpnucleo.Application.Commands;

public sealed class CreateRecursoCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateRecursoCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateRecursoCommand request, CancellationToken cancellationToken)
    {
        var recurso = Recurso.Create(request.Nome, request.Login, request.Senha);
        context.Recursos.Add(recurso);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
