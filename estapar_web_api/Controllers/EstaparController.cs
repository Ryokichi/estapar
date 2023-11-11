using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class EstaparAdminController : ControllerBase
{
    [HttpGet("{CodGaragem}/carros-no-periodo")]
    public IActionResult getCarrosPeriodo(string CodGaragem, [FromQuery] DateTime DataInicio, [FromQuery] DateTime DataFim, [FromServices] CarroService Service)
    {
        List<CarroDTO> CarrosDto = Service.BuscaCarrosNaGaragemPorPeriodo(CodGaragem, DataInicio, DataFim);
        return Ok(CarrosDto);
    }

    [HttpGet("{CodGaragem}/carros-estacionados")]
    public IActionResult getCarrosEstacionados(string CodGaragem, [FromServices] CarroService Service)
    {
        List<CarroDTO> CarrosDto = Service.BuscaCarrosEstacionados(CodGaragem);
        return Ok(CarrosDto);
    }

    [HttpGet("{CodGaragem}/carros-que-passaram")]
    public IActionResult getCarrosQuePassaram(string CodGaragem, [FromServices] CarroService Service)
    {
        List<CarroDTO> CarrosDto = Service.BuscaCarrosQuePassaram(CodGaragem);
        return Ok(CarrosDto);
    }

    [HttpGet("{CodGaragem}/fechamento-periodo")]
    public IActionResult getFechamentoPeriodo(string CodGaragem, [FromQuery] DateTime DataInicio, [FromQuery] DateTime DataFim, [FromServices] FechamentoService Service)
    {
        List<FechamentoDTO> FechamentoDto = Service.ExecutaFecahamentoDoPeriodo(CodGaragem, DataInicio, DataFim);
        return Ok(FechamentoDto);
    }

    [HttpGet("{CodGaragem}/tempo-medio")]
    public IActionResult getTempoMedio(string CodGaragem, [FromQuery] DateTime DataInicio, [FromQuery] DateTime DataFim, [FromServices] TempoMedioService Service)
    {
        List<TempoMedioDTO> TempoMedioDto = Service.ExecutaCalculoDeTempoMedioNoPeriodo(CodGaragem, DataInicio, DataFim);
        return Ok(TempoMedioDto);
    }

}