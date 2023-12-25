namespace Cpnucleo.API.Controllers.V2;

[Authorize]
[ApiController]
[ApiVersion("2")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApontamentoController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Listar apontamentos
    /// </summary>
    /// <remarks>
    /// # Listar apontamentos
    /// 
    /// Lista apontamentos da base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("ListApontamento")]
    public async Task<ActionResult<ListApontamentoViewModel>> ListApontamento([FromBody] ListApontamentoQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Consultar apontamento
    /// </summary>
    /// <remarks>
    /// # Consultar apontamento
    /// 
    /// Consulta um apontamento na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("GetApontamento")]
    public async Task<ActionResult<GetApontamentoViewModel>> GetApontamento([FromBody] GetApontamentoQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Consultar apontamento por recurso
    /// </summary>
    /// <remarks>
    /// # Consultar apontamento por recurso
    /// 
    /// Consulta um apontamento por recurso na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("GetApontamentoByRecurso")]
    public async Task<ActionResult<ListApontamentoByRecursoViewModel>> GetApontamentoByRecurso([FromBody] ListApontamentoByRecursoQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Incluir apontamento
    /// </summary>
    /// <remarks>
    /// # Incluir apontamento
    /// 
    /// Inclui um apontamento na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("CreateApontamento")]
    public async Task<ActionResult<OperationResult>> CreateApontamento([FromBody] CreateApontamentoCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Alterar apontamento
    /// </summary>
    /// <remarks>
    /// # Alterar apontamento
    /// 
    /// Altera um apontamento na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("UpdateApontamento")]
    public async Task<ActionResult<OperationResult>> UpdateApontamento([FromBody] UpdateApontamentoCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Remover apontamento
    /// </summary>
    /// <remarks>
    /// # Remover apontamento
    /// 
    /// Remove um apontamento da base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("RemoveApontamento")]
    public async Task<ActionResult<OperationResult>> RemoveApontamento([FromBody] RemoveApontamentoCommand command)
    {
        return await sender.Send(command);
    }
}
