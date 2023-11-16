namespace estapar_web_api.Testes;

public class TempoMedioServiceTeste
{
    private Mock<EstaparDbContext> dbContext;
    private TempoMedioService service;
    private Mock mockRepository;
    private List<Garagem> garagens;
    private List<FormaPagamento> formasPagto;
    private List<Passagem> passagens;

    public TempoMedioServiceTeste()
    {
        dbContext = new Mock<EstaparDbContext>();
        service = new TempoMedioService(dbContext.Object);
        mockRepository = new Mock<ITempoMedioService>();

        formasPagto = new List<FormaPagamento>
        {
            new FormaPagamento { Codigo = "DIN", Descricao = "Dinheiro"},
            new FormaPagamento { Codigo = "MEN", Descricao = "Mensalista"},
            new FormaPagamento { Codigo = "PIX", Descricao = "PIX"},
            new FormaPagamento { Codigo = "TAG", Descricao = "TAG"},
            new FormaPagamento { Codigo = "CDE", Descricao = "Cartão de Débito"},
            new FormaPagamento { Codigo = "CCR", Descricao = "Cartão de Crédito"}
        };

        garagens = new List<Garagem>
        {
            new Garagem {Codigo = "ABC01", Nome = "Garagem ABC 1", Preco_1aHora = 20, Preco_HorasExtra = 10, Preco_Mensalista = 300 },
            new Garagem {Codigo = "ABC02", Nome = "Garagem ABC 2", Preco_1aHora = 40, Preco_HorasExtra = 20, Preco_Mensalista = 500 },
            new Garagem {Codigo = "ABC03", Nome = "Garagem ABC 3", Preco_1aHora = 60, Preco_HorasExtra = 30, Preco_Mensalista = 800 },
        };

        passagens = new List<Passagem>
        {
            new Passagem
            {
                Id = 1,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 2,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 3,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 4,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 5,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 6,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 7,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 8,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 9,
                Garagem = garagens[2],
                FormaPagamento = this.formasPagto[1],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
            new Passagem
            {
                Id = 10,
                Garagem = garagens[1],
                FormaPagamento = this.formasPagto[2],
                DataHoraEntrada = DateTime.Parse("2024-01-01 00:00"),
                DataHoraSaida   = DateTime.Parse("2024-01-01 00:00"),
                CarroPlaca = "",
                CarroMarca = "",
                CarroModelo = "",
                PrecoTotal = 0.0
            },
        };


    }

    [Fact]
    public void BuscaCarrosNoPeriodoPorFormaPagto()
    {
        //Arrange
        // dbContext;
        // service = new TempoMedioService(dbContext.Object);

        // string codGaragem = "ABC01";
        // string codPagamento = "DIN";
        // DateTime dataInicio = DateTime.Parse("2024-01-01 00:00");
        // DateTime dataFim    = DateTime.Parse("2024-01-01 00:00");

        // List<string> codigosPagato = new List<string> {"DIN", "PIX"};
        // List<int> idsEsperados = new List<int> {1, 2, 3};
        
        // service.
        // dbContext.Setup(db => db.Passagem).Returns(passagens.AsQueryable());

        // var resultado = service.BuscaCarrosNoPeriodoPorFormaPagto(codGaragem, dataInicio, dataFim, codPagamento);

        // foreach (var passagem in resultado) {
        //     Assert.Contains(passagem.Id, idsEsperados);
        // }
    }
}