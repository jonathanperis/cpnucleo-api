namespace Cpnucleo.Application.Commands;

public sealed class CreateSistemaCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSistemaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(CreateSistemaCommand request, CancellationToken cancellationToken)
    {
        var sistema = Sistema.Create(request.Nome, request.Descricao, request.Id);
        context.Sistemas.Add(sistema);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
