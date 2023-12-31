using estapar_web_api;


public class CarroService : CommomService
{
    public CarroService(EstaparDbContext ctx): base(ctx)
    {
    }

    public UserResponse BuscaCarrosNaGaragemPorPeriodo(string codGaragem, DateTime dataInicio, DateTime dataFim)
    {
        string usrMensagem;
        List<EstadiaDTO> usrRetorno = new List<EstadiaDTO>();

        if (dataInicio > dataFim) {
            usrMensagem = "Data de inicio da pesquisa não pode ser maior que a de fim";
        }
        else
        {
            usrMensagem = "Ok";
            usrRetorno = DB.Passagem
                .Where(p => p.Garagem.Codigo == codGaragem)
                .Where(p => p.DataHoraEntrada >= dataInicio && p.DataHoraSaida <= dataFim)
                .Select(p => new EstadiaDTO {
                    Garagem = p.Garagem.Nome,
                    CarroPlaca = p.CarroPlaca,
                    CarroMarca = p.CarroMarca,
                    CarroModelo = p.CarroModelo,
                    DataHoraEntrada = p.DataHoraEntrada,
                    DataHoraSaida = p.DataHoraSaida,
                    TempoEstadia = calculaTempoEstadia(p),
                    FormaPagamento = p.FormaPagamento.Descricao,
                    PrecoTotal = CalculaPrecoEstadia(p, p.Garagem, p.FormaPagamento)
                })
                .ToList();
        }

        return new UserResponse {
            Mensagem = usrMensagem,
            Retorno = usrRetorno
        };
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
                    FormaPagamento = p.FormaPagamento.Descricao,
                    DataHoraSaida = p.DataHoraSaida,
                    PrecoTotal = CalculaPrecoEstadia(p, p.Garagem, p.FormaPagamento)
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

    public RegistroPassagemDTO PostCadastroSaidaPassagem(string codGaragem, PassagemCadastroSaida dadosSaida)
    {
        string msgToUser = "";
        var garagem = DB.Garagem.Where(g => g.Codigo == codGaragem).FirstOrDefault();
        var formaPgto = DB.FormaPagamento.Where(f => f.Codigo == dadosSaida.FormaPagamento).FirstOrDefault();
        var passagem = DB.Passagem.Where(p => p.Garagem.Codigo == codGaragem
            && p.CarroPlaca == dadosSaida.CarroPlaca)
            .OrderBy(p => p.DataHoraEntrada)
            .LastOrDefault();
        CarroDTO dadosPassagem = new CarroDTO();

        if (garagem == null)
        {
            msgToUser = "Garagem não encontrada";
        }
        else if (formaPgto == null)
        {
            msgToUser = "Forma de pagamento inválida";
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
            passagem.FormaPagamento = formaPgto;
            passagem.DataHoraSaida = DateTime.Parse(DateTime.Now.ToString("s"));
            passagem.PrecoTotal = CalculaPrecoEstadia(passagem, garagem, formaPgto);
            DB.Update(passagem);
            DB.SaveChanges();

            dadosPassagem = convertePassagemParaDTO(passagem);
            msgToUser = "Registro de saida criado com sucesso.";

        }
        return new RegistroPassagemDTO {
            Mensagem = msgToUser,
            Passagem = dadosPassagem
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

    private static CarroDTO convertePassagemParaDTO(Passagem passagem)
    {
        return new CarroDTO
        {
            Garagem = passagem.Garagem.Nome,
            CarroPlaca = passagem.CarroPlaca,
            CarroMarca = passagem.CarroMarca,
            CarroModelo = passagem.CarroModelo,
            DataHoraEntrada = passagem.DataHoraEntrada,
            DataHoraSaida = passagem.DataHoraSaida,
            FormaPagamento = (passagem.FormaPagamento == null) ? "" : passagem.FormaPagamento.Descricao,
            PrecoTotal = passagem.PrecoTotal
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
                passagem.PrecoTotal = CalculaPrecoEstadia(passagem, passagem.Garagem, passagem.FormaPagamento);

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

}
