using Microsoft.OpenApi.Expressions;
using estapar_web_api;

public class CarroService : CommomService
{
    public CarroService(EstaparDbContext ctx): base(ctx)
    {
    }

    public List<CarroDTO> BuscaCarrosNaGaragemPorPeriodo(string CodGaragem, DateTime DataInicio, DateTime DataFim)
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == CodGaragem)
            .Where(p => p.DataHoraEntrada >= DataInicio && p.DataHoraSaida <= DataFim)
            .Select(p => new CarroDTO
            {
                Garagem = p.Garagem.Nome,
                CarroMarca = p.CarroMarca,
                CarroPlaca = p.CarroPlaca,
                CarroModelo = p.CarroModelo,
                DataHoraEntrada = p.DataHoraEntrada,
                DataHoraSaida = p.DataHoraSaida,
            }).ToList();
    }

    public List<CarroDTO>BuscaCarrosEstacionados(string CodGaragem)
    {
        List<CarroDTO> CarrosEstacionados = new List<CarroDTO>();
        var UltimaEntradaCarro = this.GetUltimaEntradaDosCarros(CodGaragem);

        foreach (var Entrada in UltimaEntradaCarro)
        {
            var Estacionado = DB.Passagem
            .Where(p => p.Garagem.Codigo == CodGaragem 
                && p.CarroPlaca == Entrada.CarroPlaca
                && p.DataHoraEntrada == Entrada.DataHoraEntrada
                && p.DataHoraSaida == null)
            .Select(p => new CarroDTO
            {
                Garagem = p.Garagem.Nome,
                CarroMarca = p.CarroMarca,
                CarroPlaca = p.CarroPlaca,
                CarroModelo = p.CarroModelo,
                DataHoraEntrada = p.DataHoraEntrada,
            }).FirstOrDefault();

            if (Estacionado != null)
            {
                CarrosEstacionados.Add(Estacionado);
            }
        }
        return CarrosEstacionados;
    }

    public List<CarroDTO>BuscaCarrosQuePassaram(string CodGaragem)
    {
        List<CarroDTO> CarrosQuePassaram = new List<CarroDTO>();
        var UltimaEntradaCarro = this.GetUltimaEntradaDosCarros(CodGaragem);

        foreach (var entrada in UltimaEntradaCarro)
        {
            var passagem = DB.Passagem
                .Where(p => p.Garagem.Codigo == CodGaragem
                    && p.CarroPlaca == entrada.CarroPlaca 
                    && p.DataHoraEntrada == entrada.DataHoraEntrada
                    && p.DataHoraSaida != null)
                .Select(p => new CarroDTO
                {
                    Garagem = p.Garagem.Nome,
                    CarroMarca = p.CarroMarca,
                    CarroPlaca = p.CarroPlaca,
                    CarroModelo = p.CarroModelo,
                    DataHoraEntrada = p.DataHoraEntrada,
                    DataHoraSaida = p.DataHoraSaida
                })
                .FirstOrDefault();

            if (passagem != null)
            {
                CarrosQuePassaram.Add(passagem);
            }
        }
        return CarrosQuePassaram;
    }

    private List<UltimaEntradaCarroDTO> GetUltimaEntradaDosCarros(string CodGaragem)
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == CodGaragem)
            .GroupBy(p => p.CarroPlaca)
            .Select(g => new UltimaEntradaCarroDTO
            {
                CarroPlaca = g.Key,
                DataHoraEntrada = g.Max(p => p.DataHoraEntrada)
            })
            .ToList();
    }
}
