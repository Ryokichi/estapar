using estapar_web_api;

public class FechamentoService : CommomService
{
    public FechamentoService(EstaparDbContext ctx): base(ctx)
    {
    }

    public List<FechamentoDTO>ExecutaFecahamentoDoPeriodo(string codGaragem, DateTime dataInicio, DateTime dataFim )
    {
        List<FechamentoDTO> fechamento = new List<FechamentoDTO>();
        List<FormaPagamento> formasPagamento = DB.FormaPagamento.ToList();
        Garagem garagem = DB.Garagem.Where(g => g.Codigo == codGaragem).First();

        double TotalDoFechamento = 0;

        foreach (var formaPagamento in formasPagamento) 
        {
            double TotalFormaPagto = 0;

            ////Considera todos os mensalistas 
            if (formaPagamento.Codigo == "MEN") {
                int veiculosDiferentes = DB.Passagem.Where(
                    p => p.FormaPagamento.Codigo == "MEN"
                    && p.Garagem.Codigo == garagem.Codigo)
                    .GroupBy(p => p.CarroPlaca).Count();
                TotalFormaPagto = veiculosDiferentes * garagem.Preco_Mensalista;
            }
            else {
                List<Passagem> passagemDosCarros = BuscaCarrosNoPeriodoPorFormaPagto(codGaragem, dataInicio, dataFim, formaPagamento.Codigo);
                foreach(var passagem in passagemDosCarros) {
                    if (passagem.DataHoraSaida == null)
                        continue;
                    if (passagem.PrecoTotal == 0)
                        fazFechamentoEstadia(garagem, passagem);

                    TotalFormaPagto += passagem.PrecoTotal;
                }
            }

            fechamento.Add(GetFechamentoDTO(dataInicio, dataFim, formaPagamento.Descricao, TotalFormaPagto));
            TotalDoFechamento += TotalFormaPagto;
        }
        fechamento.Add(GetFechamentoDTO(dataInicio, dataFim, "Total Consolidado", TotalDoFechamento));
        return fechamento;
    }

    private FechamentoDTO GetFechamentoDTO(DateTime dataInicio, DateTime dataFim, string descricao, double totalFormaPagto)
    {
        return new FechamentoDTO {
            DataHoraInicio = dataInicio,
            DataHoraFim = dataFim,
            FormaPagamento = descricao,
            PrecoTotal = Math.Truncate(totalFormaPagto * 100) / 100
        };
    }

    private void fazFechamentoEstadia (Garagem garagem, Passagem passagem)
    {
        double valorEstadia = CalculaPrecoEstadia(passagem, passagem.Garagem, passagem.FormaPagamento);
        passagem.PrecoTotal = valorEstadia;
        DB.Update(passagem);
        DB.SaveChanges();
    }
}