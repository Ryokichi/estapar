using estapar_web_api;

public static class FormaPagamentoSeeder
{
    public static void SeedData(EstaparDbContext context)
    {
        Console.WriteLine(context.FormaPagamento.Any());
        if (!context.FormaPagamento.Any())
        {
            var formasPagamento = new List<FormaPagamento>
            {
                new FormaPagamento { Codigo = "DIN", Descricao = "Dinheiro" },
                new FormaPagamento { Codigo = "MEN", Descricao = "Mensalista" },
                new FormaPagamento { Codigo = "PIX", Descricao = "PIX" },
                new FormaPagamento { Codigo = "TAG", Descricao = "TAG" },
                new FormaPagamento { Codigo = "CDE", Descricao = "Cartão de Débito" },
                new FormaPagamento { Codigo = "CCR", Descricao = "Cartão de Crédito" },
            };

            context.FormaPagamento.AddRange(formasPagamento);
            context.SaveChanges();
        }
    }
}
