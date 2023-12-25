namespace Cpnucleo.API.Controllers.V2;

[ApiController]
[ApiVersion("2")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthUserController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Autenticar Usuário
    /// </summary>
    /// <remarks>
    /// # Autenticar Usuário
    /// 
    /// Autentica o usuário no sistema.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("AuthUser")]
    public async Task<ActionResult<AuthUserViewModel>> AuthUser([FromBody] AuthUserQuery query)
    {
        return await sender.Send(query);
    }
}
