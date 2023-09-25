using System;
namespace Pizzaria.Models;

class Pizza
{
    public string Nome { get; }
    public string[] Sabores { get; }
    public decimal Preco { get; }

    public Pizza(string nome, string[] sabores, decimal preco)
    {
        Nome = nome;
        Sabores = sabores;
        Preco = preco;
    }
}