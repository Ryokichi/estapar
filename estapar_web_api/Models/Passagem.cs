public class Passagem
{
    public int Id { get; set; }
    public Garagem Garagem { get; set; }
    public string CarroPlaca { get; set; }
    public string CarroMarca { get; set; }
    public string CarroModelo { get; set; }
    public DateTime DataHoraEntrada { get; set; }
    public DateTime? DataHoraSaida { get; set; }
    public FormaPagamento? FormaPagamento { get; set; }
    public double PrecoTotal  { get; set; } = 0.0;
}