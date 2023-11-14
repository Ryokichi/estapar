using estapar_web_api;

public class DatabaseSeeder
{
    private EstaparDbContext context;

    public DatabaseSeeder(EstaparDbContext context) {
        this.context = context;
    }
    public void SeedData()
    {
        Console.WriteLine("Seedando");
        FormaPagamentoSeeder.SeedData(context);
        GaragemSeeder.SeedData(context);
        PassagemSeeder.SeedData(context);
    }
}