using Microsoft.OpenApi.Expressions;
using estapar_web_api;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CarroService : CommomService
{
    public CarroService(EstaparDbContext ctx): base(ctx)
    {
    }

    public List<CarroDTO> BuscaCarrosNaGaragemPorPeriodo(string codGaragem, DateTime dataInicio, DateTime dataFim)
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == codGaragem)
            .Where(p => p.DataHoraEntrada >= dataInicio && p.DataHoraSaida <= dataFim)
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

    public List<CarroDTO>BuscaCarrosEstacionados(string codGaragem)
    {
        List<CarroDTO> CarrosEstacionados = new List<CarroDTO>();
        var UltimaEntradaCarro = this.GetUltimaEntradaDosCarros(codGaragem);

        foreach (var Entrada in UltimaEntradaCarro)
        {
            var Estacionado = DB.Passagem
            .Where(p => p.Garagem.Codigo == codGaragem 
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

    public List<CarroDTO>BuscaCarrosQuePassaram(string codGaragem)
    {
        List<CarroDTO> CarrosQuePassaram = new List<CarroDTO>();
        var UltimaEntradaCarro = this.GetUltimaEntradaDosCarros(codGaragem);

        foreach (var entrada in UltimaEntradaCarro)
        {
            var passagem = DB.Passagem
                .Where(p => p.Garagem.Codigo == codGaragem
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

    private List<UltimaEntradaCarroDTO> GetUltimaEntradaDosCarros(string codGaragem)
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == codGaragem)
            .GroupBy(p => p.CarroPlaca)
            .Select(g => new UltimaEntradaCarroDTO
            {
                CarroPlaca = g.Key,
                DataHoraEntrada = g.Max(p => p.DataHoraEntrada)
            })
            .ToList();
    }

    public RegistroPassagemDTO PostCadastroNovaPassagem(string codGaragem, PassagemCadastroEntrada dadosEntrada)
    {
        string msgToUser = "";
        CarroDTO dadosPassagem = new CarroDTO();

        var garagem = DB.Garagem.Where(g => g.Codigo == codGaragem).FirstOrDefault();
        var passagem = DB.Passagem.Where(p => p.Garagem.Codigo == codGaragem
            && p.CarroPlaca == dadosEntrada.CarroPlaca)
            .OrderBy(p => p.DataHoraEntrada)
            .LastOrDefault();

        if (garagem == null)
        {
            msgToUser = "Garagem não encontrada";
        }
        else if (passagem != null && passagem.DataHoraSaida == null)
        {
            msgToUser = "Já existe um registro de entrada sem saida para este carro";
            dadosPassagem = convertePassagemParaDTO(passagem);
        }
        else if( (passagem == null || passagem.DataHoraSaida != null) && garagem != null)
        {
            DateTime dataHoraEntrada = DateTime.Parse(DateTime.Now.ToString("s"));
            passagem = new Passagem
            {
                Garagem = garagem ,
                CarroPlaca = dadosEntrada.CarroPlaca,
                CarroMarca = dadosEntrada.CarroMarca,
                CarroModelo = dadosEntrada.CarroModelo,
                DataHoraEntrada = dataHoraEntrada
            };
            DB.Passagem.Add(passagem);
            DB.SaveChanges();
            msgToUser = "Registro Salvo";
            dadosPassagem = convertePassagemParaDTO(passagem);
        }

            return new RegistroPassagemDTO {
            Mensagem = msgToUser,
            Passagem = dadosPassagem
        };
    }

    public RegistroPassagemDTO PostCadastroSaidaPassagem(string codGaragem, PassagemCadastroSaida DadosSaida)
    {
        string msgToUser = "";
        var garagem = DB.Garagem.Where(g => g.Codigo == codGaragem).FirstOrDefault();
        var passagem = DB.Passagem.Where(p => p.Garagem.Codigo == codGaragem
            && p.CarroPlaca == DadosSaida.CarroPlaca)
            .OrderBy(p => p.DataHoraEntrada)
            .LastOrDefault();
        CarroDTO dadosPassagem = new CarroDTO();

        if (garagem == null)
        {
            msgToUser = "Garagem não encontrada";
        }
        else if (passagem == null )
        {
            msgToUser = "Não existe entrada para o carro informado";
        }
        else if (passagem.DataHoraSaida != null) {
            msgToUser = "Já existe uma saida registrada para esse veículo";
            dadosPassagem = convertePassagemParaDTO(passagem);
        }
        else 
        {
            DateTime DataHoraSaida = DateTime.Parse(DateTime.Now.ToString("s"));
            Double PrecoTotal = CalculaPrecoEstadia(garagem, passagem);

            passagem.DataHoraSaida = DataHoraSaida;
            passagem.PrecoTotal = PrecoTotal;
            DB.Update(passagem);
            DB.SaveChanges();

            dadosPassagem = convertePassagemParaDTO(passagem);

        }
        return new RegistroPassagemDTO {
            Mensagem = msgToUser,
            Passagem = dadosPassagem
        };
    }

    public object PostSeedPassagemDB( List<PassagemDTO> seedPassagens)
    {
        int Sucessos = 0;
        int Falhas = 0;

        foreach (var seedPassagem in seedPassagens) 
        {
            var garagem = DB.Garagem.Where(g => g.Codigo == seedPassagem.Garagem ).FirstOrDefault();
            if (garagem != null) {
                
                Passagem passagem = convertPassagemDTOParaPassagem(garagem, seedPassagem);
                passagem.PrecoTotal = CalculaPrecoEstadia(garagem, passagem);

                DB.Add(passagem);
                DB.SaveChanges();
                Sucessos ++;
            }
            else {
                Falhas ++;
            }
        }

        return new {
            Sucessos = Sucessos,
            Falhas = Falhas,
        };
    }

    private Passagem convertPassagemDTOParaPassagem (Garagem garagem, PassagemDTO passagem)
    {
        FormaPagamento formaPagto = DB.FormaPagamento.Where(f => f.Codigo == passagem.FormaPagamento).FirstOrDefault();
        return new Passagem
        {
            Garagem = garagem,
            CarroPlaca = passagem.CarroPlaca,
            CarroMarca = passagem.CarroMarca,
            CarroModelo = passagem.CarroModelo,
            DataHoraEntrada = DateTime.Parse(passagem.DataHoraEntrada),
            DataHoraSaida = DateTime.Parse(passagem.DataHoraSaida),
            FormaPagamento = formaPagto,
            PrecoTotal = passagem.PrecoTotal
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
