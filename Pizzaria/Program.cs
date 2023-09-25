using System;
using System.Collections.Generic;

namespace Pizzaria.Models;


class Program
{
    static List<Pizza> pizzas = new List<Pizza>();
    static List<Pedido> pedidos = new List<Pedido>();
    static int numeroPedido = 1;

    static void Main(string[] args)
    {
        int escolha;

        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1- Adicionar Pizza");
            Console.WriteLine("2- Listar Pizzas");
            Console.WriteLine("3- Criar novo Pedido");
            Console.WriteLine("4- Listar Pedidos");
            Console.WriteLine("5- Pagar Pedido");
            Console.WriteLine("0- Sair");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                switch (escolha)
                {
                    case 1:
                        AdicionarPizza();
                        break;
                    case 2:
                        ListarPizzas();
                        break;
                    case 3:
                        CriarPedido();
                        break;
                    case 4:
                        ListarPedidos();
                        break;
                    case 5:
                        PagarPedido();
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }

        } while (escolha != 0);
    }

    static void AdicionarPizza()
    {
        Console.Write("Nome da Pizza: ");
        string nome = Console.ReadLine();
        Console.Write("Sabores (separados por vírgula): ");
        string[] sabores = Console.ReadLine().Split(',');
        Console.Write("Preço (00,00): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal preco))
        {
            pizzas.Add(new Pizza(nome, sabores, preco));
            Console.WriteLine("Pizza criada com sucesso.");
        }
        else
        {
            Console.WriteLine("Formato de preço inválido.");
        }
    }

    static void ListarPizzas()
    {
        Console.WriteLine("Listagem de Pizzas:");
        foreach (var pizza in pizzas)
        {
            Console.WriteLine($"Nome: {pizza.Nome}");
            Console.WriteLine($"Sabores: {string.Join(", ", pizza.Sabores)}");
            Console.WriteLine($"Preço: R${pizza.Preco:F2}\n");
        }
    }

    static void CriarPedido()
    {
        Console.Write("Quem é o Cliente? ");
        string nomeCliente = Console.ReadLine();
        Console.Write("Qual é o telefone do Cliente? ");
        string telefoneCliente = Console.ReadLine();

        List<Pizza> pizzasPedido = new List<Pizza>();
        do
        {
            Console.Write("Escolha uma pizza para Adicionar (digite o nome): ");
            string nomePizza = Console.ReadLine();
            Pizza pizzaSelecionada = pizzas.Find(p => p.Nome == nomePizza);
            if (pizzaSelecionada != null)
            {
                pizzasPedido.Add(pizzaSelecionada);
                Console.Write("Deseja acrescentar mais uma pizza? (1-Sim | 2- Não): ");
                int escolha;
                if (int.TryParse(Console.ReadLine(), out escolha) && escolha == 2)
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Pizza não encontrada.");
            }
        } while (true);

        decimal totalPedido = pizzasPedido.Sum(p => p.Preco);
        Pedido pedido = new Pedido(numeroPedido++, nomeCliente, telefoneCliente, pizzasPedido, totalPedido);

        pedidos.Add(pedido);
        Console.WriteLine("Pedido Criado");
        Console.WriteLine($"Total: R${totalPedido:F2}");
    }

    static void ListarPedidos()
    {
        Console.WriteLine("Listagem de Pedidos:");
        foreach (var pedido in pedidos)
        {
            Console.WriteLine($"Pedido {pedido.NumeroPedido}:");
            Console.WriteLine($"Cliente: {pedido.NomeCliente} - {pedido.TelefoneCliente}");
            Console.WriteLine("Pizzas do pedido:");
            foreach (var pizza in pedido.PizzasPedido)
            {
                Console.WriteLine($"   {pizza.Nome} - R${pizza.Preco:F2}");
            }
            Console.WriteLine($"Total: R${pedido.TotalPedido:F2}");
            Console.WriteLine($"Quanto Falta para Pagar: R${pedido.QuantoFaltaPagar():F2}");
            Console.WriteLine($"Pago: {(pedido.Pago ? "Sim" : "Não")}\n");
        }
    }

    static void PagarPedido()
    {
        Console.Write("Digite o número do pedido: ");
        if (int.TryParse(Console.ReadLine(), out int numeroPedido))
        {
            Pedido pedido = pedidos.Find(p => p.NumeroPedido == numeroPedido);
            if (pedido != null && !pedido.Pago)
            {
                Console.Write("Forma de pagamento (1-Dinheiro | 2-Cartão de Débito | 3-Vale Refeição): ");
                if (int.TryParse(Console.ReadLine(), out int formaPagamento))
                {
                    decimal valorPago = 0;
                    if (formaPagamento == 1)
                    {
                        Console.Write("Digite o valor em dinheiro: ");
                        decimal.TryParse(Console.ReadLine(), out valorPago);
                    }
                    else if (formaPagamento == 2 || formaPagamento == 3)
                    {
                        Console.Write("Digite o valor: ");
                        decimal.TryParse(Console.ReadLine(), out valorPago);
                    }
                    else
                    {
                        Console.WriteLine("Forma de pagamento inválida.");
                        return;
                    }

                    decimal troco = pedido.Pagar(valorPago, (FormaPagamento)formaPagamento);
                    Console.WriteLine($"Valor pago: R${valorPago:F2}");
                    if (troco > 0)
                    {
                        Console.WriteLine($"Troco: R${troco:F2}");
                    }
                }
                else
                {
                    Console.WriteLine("Forma de pagamento inválida.");
                }
            }
            else
            {
                Console.WriteLine("Pedido não encontrado ou já foi pago.");
            }
        }
        else
        {
            Console.WriteLine("Número de pedido inválido.");
        }
    }
}