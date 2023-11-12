using System;

namespace estapar_web_api.Testes;

public class TempoMedioServiceTeste
{
    private TempoMedioService tms;

    public TempoMedioServiceTeste() 
    {
        tms = new TempoMedioService(new Mock<EstaparDbContext>().Object);
    }

    [Fact]
    public void PrecoEstadia_1min()
    {
        DateTime dataHoraEntrada    = DateTime.Parse("2023-01-01 00:00:00");
        DateTime dataHoraSaida      = DateTime.Parse("2023-01-01 00:01:00");
        var valor = tms.CalculaPrecoEstadia(dataHoraEntrada, dataHoraSaida, 25, 15.8);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void PrecoEstadia_30min()
    {
        DateTime dataHoraEntrada    = DateTime.Parse("2023-01-01 00:00:00");
        DateTime dataHoraSaida      = DateTime.Parse("2023-01-01 00:30:00");
        var valor = tms.CalculaPrecoEstadia(dataHoraEntrada, dataHoraSaida, 25, 15.8);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void PrecoEstadia_31min()
    {
        DateTime dataHoraEntrada    = DateTime.Parse("2023-01-01 00:00:00");
        DateTime dataHoraSaida      = DateTime.Parse("2023-01-01 00:31:00");
        var valor = tms.CalculaPrecoEstadia(dataHoraEntrada, dataHoraSaida, 25, 15.8);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void PrecoEstadia_59min()
    {
        DateTime dataHoraEntrada    = DateTime.Parse("2023-01-01 00:00:00");
        DateTime dataHoraSaida      = DateTime.Parse("2023-01-01 00:59:00");
        var valor = tms.CalculaPrecoEstadia(dataHoraEntrada, dataHoraSaida, 25, 15.8);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void PrecoEstadia_60min()
    {
        DateTime dataHoraEntrada    = DateTime.Parse("2023-01-01 00:00:00");
        DateTime dataHoraSaida      = DateTime.Parse("2023-01-01 01:00:00");
        var valor = tms.CalculaPrecoEstadia(dataHoraEntrada, dataHoraSaida, 25, 15.8);

        Assert.Equal(25, valor);
    }

    [Fact]
    public void PrecoEstadia_61min()
    {
        DateTime dataHoraEntrada    = DateTime.Parse("2023-01-01 00:00:00");
        DateTime dataHoraSaida      = DateTime.Parse("2023-01-01 01:01:00");
        var valor = tms.CalculaPrecoEstadia(dataHoraEntrada, dataHoraSaida, 25, 15.8);

        Assert.Equal(32.9, valor);
    }

    [Fact]
    public void PrecoEstadia_91min()
    {
        DateTime dataHoraEntrada    = DateTime.Parse("2023-01-01 00:00:00");
        DateTime dataHoraSaida      = DateTime.Parse("2023-01-01 01:31:00");
        var valor = tms.CalculaPrecoEstadia(dataHoraEntrada, dataHoraSaida, 25, 15.8);

        Assert.Equal(40.8, valor);
    }
}