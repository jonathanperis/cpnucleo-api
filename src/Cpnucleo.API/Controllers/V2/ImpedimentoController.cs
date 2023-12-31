﻿namespace Cpnucleo.API.Controllers.V2;

[Authorize]
[ApiController]
[ApiVersion("2")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ImpedimentoController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Listar impedimentos
    /// </summary>
    /// <remarks>
    /// # Listar impedimentos
    /// 
    /// Lista impedimentos da base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("ListImpedimento")]
    public async Task<ActionResult<ListImpedimentoViewModel>> ListImpedimento([FromBody] ListImpedimentoQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Consultar impedimento
    /// </summary>
    /// <remarks>
    /// # Consultar impedimento
    /// 
    /// Consulta um impedimento na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("GetImpedimento")]
    public async Task<ActionResult<GetImpedimentoViewModel>> GetImpedimento([FromBody] GetImpedimentoQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Incluir impedimento
    /// </summary>
    /// <remarks>
    /// # Incluir impedimento
    /// 
    /// Inclui um impedimento na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("CreateImpedimento")]
    public async Task<ActionResult<OperationResult>> CreateImpedimento([FromBody] CreateImpedimentoCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Alterar impedimento
    /// </summary>
    /// <remarks>
    /// # Alterar impedimento
    /// 
    /// Altera um impedimento na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("UpdateImpedimento")]
    public async Task<ActionResult<OperationResult>> UpdateImpedimento([FromBody] UpdateImpedimentoCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Remover impedimento
    /// </summary>
    /// <remarks>
    /// # Remover impedimento
    /// 
    /// Remove um impedimento da base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("RemoveImpedimento")]
    public async Task<ActionResult<OperationResult>> RemoveImpedimento([FromBody] RemoveImpedimentoCommand command)
    {
        return await sender.Send(command);
    }
}
