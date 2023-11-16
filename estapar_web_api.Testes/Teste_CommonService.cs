using System;

namespace estapar_web_api.Testes;

public class CommomServiceTeste
{
    private CommomService service;
    private Garagem garagem;
    private Passagem passagem;
    private FormaPagamento formaPagto;

    public CommomServiceTeste() 
    {
        service = new CommomService(new Mock<EstaparDbContext>().Object);
        garagem = new Garagem 
        {
            Codigo = "TST",
            Nome = "TESTE",
            Preco_1aHora = 25,
            Preco_HorasExtra = 15.8,
            Preco_Mensalista = 250
        };

        passagem = new Passagem 
        {
            Id = 1,
            Garagem = this.garagem,
            CarroPlaca = "PXW-1111",
            CarroMarca = "FIAT",
            CarroModelo = "147",
            DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00"),
            DataHoraSaida = DateTime.Parse("2023-01-01 00:00:00"),
            FormaPagamento = null,
            PrecoTotal = 0.0
        };

        formaPagto = new FormaPagamento 
        {
            Codigo = "PIX",
            Descricao = "PIX"
        };
    }

    [Fact]
    public void CalculaPrecoEstadia_PagtoNull()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 02:01:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, null);
        Assert.Equal(0, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_Mensalista()
    {
        this.formaPagto.Codigo = "MEN";
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 02:01:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);
        
        Assert.Equal(0, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_SaidaNull()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = null;
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, null);
        Assert.Equal(0, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_1min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 00:01:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_30min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 00:30:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_31min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 00:31:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_59min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 00:59:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_60min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 01:00:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_61min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 01:01:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(32.9, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_91min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 01:31:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(40.8, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_119min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 01:59:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(40.8, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_120min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 02:00:59");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);

        Assert.Equal(40.8, valor);
    }

    [Fact]
    public void CalculaPrecoEstadia_121min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida = DateTime.Parse("2023-01-01 02:01:00");
        var valor = CommomService.CalculaPrecoEstadia(this.passagem, this.garagem, this.formaPagto);
        Assert.Equal(48.7, valor);
    }


    [Fact]
    public void GetTimeSpanEmMinutos_0sec()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 00:00:00");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(0, valor);
    }

    [Fact]
    public void GetTimeSpanEmMinutos_1sec()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 00:00:01");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(0, valor);
    }

    [Fact]
    public void GetTimeSpanEmMinutos_59sec()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 00:00:59");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(0, valor);
    }

    [Fact]
    public void GetTimeSpanEmMinutos_1min()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 00:01:59");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(1, valor);
    }

    [Fact]
    public void GetTimeSpanEmMinutos_59min()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 00:59:59");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(59, valor);
    }
    [Fact]
    public void GetTimeSpanEmMinutos_1h()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 01:00:59");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(60, valor);
    }
    [Fact]
    public void GetTimeSpanEmMinutos_1h30m()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 01:30:59");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(90, valor);
    }

    [Fact]
    public void GetTimeSpanEmMinutos_1h59m()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-01 01:59:59");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(119, valor);
    }

    [Fact]
    public void GetTimeSpanEmMinutos_1d()
    {
        DateTime ini = DateTime.Parse("2023-01-01 00:00:00");
        DateTime fim = DateTime.Parse("2023-01-02 00:00:59");
        var valor = CommomService.GetTimeSpanEmMinutos(ini, fim);
        Assert.Equal(24*60, valor);
    }

    [Fact]
    public void calculaTempoEstadia_SaidaNull()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida   = null;
        var valor = CommomService.calculaTempoEstadia(this.passagem);
        Assert.Equal("00:00", valor);
    }
    
    [Fact]
    public void calculaTempoEstadia_0min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida   = DateTime.Parse("2023-01-01 00:00:59");
        var valor = CommomService.calculaTempoEstadia(this.passagem);
        Assert.Equal("00:00", valor);
    }

    [Fact]
    public void calculaTempoEstadia_1min()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida   = DateTime.Parse("2023-01-01 00:01:59");
        var valor = CommomService.calculaTempoEstadia(this.passagem);
        Assert.Equal("00:01", valor);
    }
    
    [Fact]
    public void calculaTempoEstadia_1h()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida   = DateTime.Parse("2023-01-01 01:00:59");
        var valor = CommomService.calculaTempoEstadia(this.passagem);
        Assert.Equal("01:00", valor);
    }

    [Fact]
    public void calculaTempoEstadia_1d()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida   = DateTime.Parse("2023-01-02 00:00:59");
        var valor = CommomService.calculaTempoEstadia(this.passagem);
        Assert.Equal("24:00", valor);
    }

    [Fact]
    public void calculaTempoEstadia_3d31m()
    {
        this.passagem.DataHoraEntrada = DateTime.Parse("2023-01-01 00:00:00");
        this.passagem.DataHoraSaida   = DateTime.Parse("2023-01-04 00:31:00");

        Console.WriteLine(this.passagem.DataHoraSaida - this.passagem.DataHoraEntrada);
        var valor = CommomService.calculaTempoEstadia(this.passagem);
        Assert.Equal("72:31", valor);
    }
}
