using estapar_web_api;

public static class GaragemSeeder
{
    public static void SeedData(EstaparDbContext context)
    {
        if (!context.Garagem.Any())
        {
            var garagens = new List<Garagem>
            {
                new Garagem {
                    Codigo = "EVO01",
                    Nome =  "Estamplaza Vila Olimpia",
                    Preco_1aHora =  40,
                    Preco_HorasExtra =  10,
                    Preco_Mensalista =  550
                },
                new Garagem {
                    Codigo = "PLJK01",
                    Nome =  "Plaza JK",
                    Preco_1aHora =  35,
                    Preco_HorasExtra =  12,
                    Preco_Mensalista =  380
                },
                new Garagem {
                    Codigo = "SZJK01",
                    Nome =  "Spazio JK",
                    Preco_1aHora =  30,
                    Preco_HorasExtra =  15,
                    Preco_Mensalista =  350
                },
                new Garagem {
                    Codigo = "CSLU01",
                    Nome =  "Condominio São Luiz",
                    Preco_1aHora =  50,
                    Preco_HorasExtra =  12,
                    Preco_Mensalista =  550
                },
                new Garagem {
                    Codigo = "COTO01",
                    Nome =  "Corporate Tower Itaim",
                    Preco_1aHora =  30,
                    Preco_HorasExtra =  12,
                    Preco_Mensalista =  360
                }
            };

            context.Garagem.AddRange(garagens);
            context.SaveChanges();
        }
    }
}