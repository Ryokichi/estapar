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

    public RegistroPassagemDTO PostCadastroNovaPassagem(string CodGaragem, PassagemCadastroEntrada DadosEntrada)
    {
        string MsgToUser = "";
        var Garagem = DB.Garagem.Where(g => g.Codigo == CodGaragem).FirstOrDefault();
        var passagem = DB.Passagem.Where(p => p.Garagem.Codigo == CodGaragem
            && p.CarroPlaca == DadosEntrada.CarroPlaca)
            .OrderBy(p => p.DataHoraEntrada)
            .LastOrDefault();
        CarroDTO DadosPassagem = new CarroDTO();

        if (Garagem == null)
        {
            MsgToUser = "Garagem não encontrada";
        }
        else if (passagem != null && passagem.DataHoraSaida == null)
        {
            MsgToUser = "Já existe um registro de entrada sem saida para este carro";
            DadosPassagem = convertePassagemParaDTO(passagem);

        }
        else if( (passagem == null || passagem.DataHoraSaida != null) && Garagem != null)
        {
            DateTime dataHoraEntrada = DateTime.Parse(DateTime.Now.ToString("s"));
            passagem = new Passagem
            {
                Garagem = Garagem,
                CarroPlaca = DadosEntrada.CarroPlaca,
                CarroMarca = DadosEntrada.CarroMarca,
                CarroModelo = DadosEntrada.CarroModelo,
                DataHoraEntrada = dataHoraEntrada
            };
            DB.Passagem.Add(passagem);
            DB.SaveChanges();
            MsgToUser = "Registro Salvo";
            DadosPassagem = convertePassagemParaDTO(passagem);
        }

            return new RegistroPassagemDTO {
            Mensagem = MsgToUser,
            Passagem = DadosPassagem
        };
    }

    public RegistroPassagemDTO PostCadastroSaidaPassagem(string CodGaragem, PassagemCadastroSaida DadosSaida)
    {
        string MsgToUser = "";
        var Garagem = DB.Garagem.Where(g => g.Codigo == CodGaragem).FirstOrDefault();
        var passagem = DB.Passagem.Where(p => p.Garagem.Codigo == CodGaragem
            && p.CarroPlaca == DadosSaida.CarroPlaca)
            .OrderBy(p => p.DataHoraEntrada)
            .LastOrDefault();
        CarroDTO DadosPassagem = new CarroDTO();

        if (Garagem == null)
        {
            MsgToUser = "Garagem não encontrada";
        }
        else if (passagem == null )
        {
            MsgToUser = "Não existe entrada para o carro informado";
        }
        else if (passagem.DataHoraSaida != null) {
            MsgToUser = "Já existe uma saida registrada para esse veículo";
            DadosPassagem = convertePassagemParaDTO(passagem);
        }
        else 
        {
            DateTime DataHoraSaida = DateTime.Parse(DateTime.Now.ToString("s"));
            Double PrecoTotal = CalculaPrecoEstadia(passagem.DataHoraEntrada, DataHoraSaida , Garagem.Preco_1aHora , Garagem.Preco_HorasExtra);

            passagem.DataHoraSaida = DataHoraSaida;
            passagem.PrecoTotal = PrecoTotal;
            DB.Update(passagem);
            DB.SaveChanges();

            DadosPassagem = convertePassagemParaDTO(passagem);

        }
        return new RegistroPassagemDTO {
            Mensagem = MsgToUser,
            Passagem = DadosPassagem
        };
    }

    private CarroDTO convertePassagemParaDTO(Passagem passagem)
    {
        return new CarroDTO
        {
            Garagem = passagem.Garagem.Nome,
            CarroPlaca = passagem.CarroPlaca,
            CarroMarca = passagem.CarroMarca,
            CarroModelo = passagem.CarroModelo,
            DataHoraEntrada = passagem.DataHoraEntrada,
            DataHoraSaida = passagem.DataHoraSaida,
            ValorTotal = passagem.PrecoTotal
        };
    }
}
