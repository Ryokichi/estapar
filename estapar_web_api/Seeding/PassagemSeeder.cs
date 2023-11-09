using estapar_web_api;

public static class PassagemSeeder
{
    public static void SeedData(EstaparDbContext context)
    {
        if (!context.Passagem.Any())
        {
            var passagens = new List<Passagem>
            {
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "EVO01").First(),
                    CarroPlaca = "ABC-0O12",
                    CarroMarca = "Honda",
                    CarroModelo = "FIT",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 13:30"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "PIX").First() ,
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "EVO01").First(),
                    CarroPlaca = "DKO-3927",
                    CarroMarca = "Toyota",
                    CarroModelo = "Yaris",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 08:40"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 09:55"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo =="CCR").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "EVO01").First(),
                    CarroPlaca = "SPE-9F42",
                    CarroMarca = "Fiat",
                    CarroModelo = "Argo",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 10:15"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 11:20"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "TAG").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "EVO01").First(),
                    CarroPlaca = "WPS-0385",
                    CarroMarca = "Fiat",
                    CarroModelo = "Fiorino",
                    DataHoraEntrada = DateTime.Parse("06/09/2023 09:10"),
                    DataHoraSaida = DateTime.Parse("10/09/2023 11:30"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "MEN").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "EVO01").First(),
                    CarroPlaca = "FER-E345",
                    CarroMarca = "Volkwagen",
                    CarroModelo = "Gol",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 09:40"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 12:40"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "MEN").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "EVO01").First(),
                    CarroPlaca = "FER-E345",
                    CarroMarca = "Volkwagen",
                    CarroModelo = "Gol",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 15:10"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 19:30"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "MEN").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "PLJK01").First(),
                    CarroPlaca = "FCK-1E44",
                    CarroMarca = "Fiat",
                    CarroModelo = "Palio",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 10:11"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 14:33"),
                    FormaPagamento =  context.FormaPagamento.Where(p => p.Codigo =="DIN").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "PLJK01").First(),
                    CarroPlaca = "HHI-0492",
                    CarroMarca = "BMW",
                    CarroModelo = "320i",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 09:40"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 17:49"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "PIX").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "PLJK01").First(),
                    CarroPlaca = "HRQ-9018",
                    CarroMarca = "Audi",
                    CarroModelo = "A4",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 10:22"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 18:36"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "MEN").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "CSLU01").First(),
                    CarroPlaca = "JZH-6272",
                    CarroMarca = "Volkswagen",
                    CarroModelo = "Tiguan",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 10:12"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 16:30"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "PIX").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "CSLU01").First(),
                    CarroPlaca = "KLV-3182",
                    CarroMarca = "Volkswagen",
                    CarroModelo = "T-Cross",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 11:10"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 20:45"),
                    FormaPagamento =  context.FormaPagamento.Where(p => p.Codigo =="CDE").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "CSLU01").First(),
                    CarroPlaca = "MOR-6228",
                    CarroMarca = "Volkswagen",
                    CarroModelo = "Jetta",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 09:10"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 16:49"),
                    FormaPagamento =  context.FormaPagamento.Where(p => p.Codigo =="CDE").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "CSLU01").First(),
                    CarroPlaca = "JTW-1439",
                    CarroMarca = "Honda",
                    CarroModelo = "Civic",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 11:12"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 12:30"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo =="CCR").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "CSLU01").First(),
                    CarroPlaca = "MYQ-5648",
                    CarroMarca = "Honda",
                    CarroModelo = "H-RV",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 14:55"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 15:59"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "TAG").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "COTO01").First(),
                    CarroPlaca = "LSX-4521",
                    CarroMarca = "Jeep",
                    CarroModelo = "Renegate",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 09:12"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 18:33"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "MEN").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "COTO01").First(),
                    CarroPlaca = "BDI-8423",
                    CarroMarca = "Jeep",
                    CarroModelo = "Commander",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 10:12"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 11:30"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "PIX").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "COTO01").First(),
                    CarroPlaca = "LVX-7196",
                    CarroMarca = "Chevrolet",
                    CarroModelo = "Prisma",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 11:30"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 12:40"),
                    FormaPagamento =  context.FormaPagamento.Where(p => p.Codigo =="CDE").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "COTO01").First(),
                    CarroPlaca = "FDH-4726",
                    CarroMarca = "Chevrolet",
                    CarroModelo = "S10",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 15:30"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 16:50"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo =="CCR").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "COTO01").First(),
                    CarroPlaca = "NCF-5760",
                    CarroMarca = "Chevrolet",
                    CarroModelo = "Onix",
                    DataHoraEntrada = DateTime.Parse("04/09/2023 10:10"),
                    DataHoraSaida = DateTime.Parse("04/09/2023 18:40"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "TAG").First(),
                    PrecoTotal = 0.0
                },
                new Passagem {
                    Garagem = context.Garagem.Where(g => g.Codigo == "COTO01").First(),
                    CarroPlaca = "IND-6562",
                    CarroMarca = "Porsche",
                    CarroModelo = "911",
                    DataHoraEntrada = DateTime.Parse("05/09/2023 10:12"),
                    DataHoraSaida = DateTime.Parse("05/09/2023 14:55"),
                    FormaPagamento = context.FormaPagamento.Where(p => p.Codigo == "TAG").First(),
                    PrecoTotal = 0.0
                }
            };

            context.Passagem.AddRange(passagens);
            context.SaveChanges();
        }
    }
}