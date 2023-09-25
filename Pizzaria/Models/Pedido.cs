using System;


namespace Pizzaria.Models;



class Pedido
{
    public int NumeroPedido { get; }
    public string NomeCliente { get; }
    public string TelefoneCliente { get; }
    public List<Pizza> PizzasPedido { get; }
    public decimal TotalPedido { get; private set; }
    public bool Pago { get; private set; }
    public decimal ValorPago { get; private set; }

    public Pedido(int numeroPedido, string nomeCliente, string telefoneCliente, List<Pizza> pizzasPedido, decimal totalPedido)
    {
        NumeroPedido = numeroPedido;
        NomeCliente = nomeCliente;
        TelefoneCliente = telefoneCliente;
        PizzasPedido = pizzasPedido;
        TotalPedido = totalPedido;
        Pago = false;
    }

    public decimal QuantoFaltaPagar()
    {
        return TotalPedido - ValorPago;
    }

    public decimal Pagar(decimal valorPago, FormaPagamento formaPagamento)
    {
        if (!Pago)
        {
            ValorPago = valorPago;
            Pago = true;
            return ValorPago - TotalPedido;
        }
        return 0;
    }
}