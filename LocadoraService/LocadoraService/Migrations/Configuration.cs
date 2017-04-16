namespace LocadoraService.Migrations
{
    using LocadoraService.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LocadoraService.Models.LocadoraServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LocadoraService.Models.LocadoraServiceContext context)
        {
            context.Clientes.AddOrUpdate(x => x.Id,
                new Cliente() { Id = 1, Nome = "Gilson Austen", CPF = "11111111111", FilmeId = 1 },
                new Cliente() { Id = 2, Nome = "Guilherme Dickens", CPF = "22222222222", FilmeId = 2 },
                new Cliente() { Id = 3, Nome = "Vinicius de Cervantes", CPF = "33333333333", FilmeId = 3 }
                );

            context.Filmes.AddOrUpdate(x => x.Id,
                new Filme() { Id = 1, Nome = "A Noiva de Chucky", Categoria = "Terror", FaixaEtaria = 18, Preco = 20 },
                new Filme() { Id = 2, Nome = "Minority Report", Categoria = "Ação", FaixaEtaria = 12, Preco = 25 },
                new Filme() { Id = 3, Nome = "Jogos Mortais 4", Categoria = "Terror", FaixaEtaria = 18, Preco = 32 }
                );

            context.Locadoras.AddOrUpdate(x => x.Id,
                new Locadora() { Id = 1, Nome = "Fantasy Video", Endereco = "Avenida rio branco, 156" },
                new Locadora() { Id = 2, Nome = "LocaTudo", Endereco = "Rua Augusta, 200" },
                new Locadora() { Id = 3, Nome = "Point dos Filmes", Endereco = "Avenida Vieira Souto, 459" }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
