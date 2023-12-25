namespace Cpnucleo.Application.Queries;

public sealed class AuthUserQueryHandler(IApplicationDbContext context, IConfiguration configuration) : IRequestHandler<AuthUserQuery, AuthUserViewModel>
{
    public async ValueTask<AuthUserViewModel> Handle(AuthUserQuery request, CancellationToken cancellationToken)
    {
        var recurso = await context.Recursos
            .AsNoTracking()
            .Where(x => x.Login == request.Usuario && x.Ativo)
            .FirstOrDefaultAsync(cancellationToken);

        if (recurso is null)
        {
            return new AuthUserViewModel(OperationResult.NotFound);
        }

        var success = Recurso.VerifyPassword(request.Senha, recurso.Senha!, recurso.Salt!);

        if (!success)
        {
            return new AuthUserViewModel(OperationResult.NotFound);
        }

        _ = int.TryParse(configuration["Jwt:Expires"], out var jwtExpires);
        string token = TokenService.GenerateToken(recurso.Id.ToString(), configuration["Jwt:Key"]!, configuration["Jwt:Issuer"]!, jwtExpires);

        return new AuthUserViewModel(OperationResult.Success, token, recurso.MapToDto());
    }
}
