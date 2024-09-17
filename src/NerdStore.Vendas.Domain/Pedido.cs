using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using System.Collections.ObjectModel;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : Entity, //Informações em comum de uma entidade
                         IAggregateRoot //Deixa explicito que ele é uma rais de agregação
    {
        protected Pedido()
        {
            _pedidoItems = new Collection<PedidoItem>();
        }

        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;


        public Guid Clienteid { get; set; }
        public decimal ValorTotal { get; private set; }
        public decimal Desconto { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public Voucher Voucher { get; private set; }
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        private readonly Collection<PedidoItem> _pedidoItems;


        public void AdicionarItem(PedidoItem pedidoItem)
        {

            ValidarQuantidadeItemPermitida(pedidoItem);


            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);

                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);

                pedidoItem = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            _pedidoItems.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);
            ValidarQuantidadeItemPermitida(pedidoItem);

            var itemExistente = _pedidoItems.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId);

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);

            _pedidoItems.Remove(pedidoItem);

            CalcularValorPedido();
        }

        public bool PedidoItemExistente(PedidoItem pedidoItem)
        {
            return _pedidoItems.Any(p => p.ProdutoId == pedidoItem.ProdutoId);
        }

        public void ValidarPedidoItemInexistente(PedidoItem pedidoItem)
        {
            if (!PedidoItemExistente(pedidoItem))
                throw new DomainException("O item não existe no pedido");
        }

        public void ValidarQuantidadeItemPermitida(PedidoItem pedidoItem)
        {
            var quantidadeItens = pedidoItem.Quantidade;
            
            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);
                quantidadeItens += itemExistente.Quantidade;
            }

            if (quantidadeItens > MAX_UNIDADES_ITEM)
                throw new DomainException($"Maximo de {MAX_UNIDADES_ITEM} unidades por produto!");
        }

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var result = voucher.ValidarSeAplicavel();

            if(!result.IsValid) 
                return result;

            Voucher = voucher;
            VoucherUtilizado = true;

            CalcularValorTotalDesconto();

            return result;
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public void CalcularValorTotalDesconto()
        {
            if(!VoucherUtilizado) return;

            decimal desconto = 0;
            decimal valor = ValorTotal;

            switch (Voucher.TipoDescontoVoucher)
            {
                case TipoDescontoVoucher.Valor:
                    if (Voucher.ValorDesconto.HasValue)
                    {
                        desconto = Voucher.ValorDesconto.Value;
                        valor -= desconto;
                    }
                    break;
                case TipoDescontoVoucher.Porcentagem:
                    if (Voucher.PercentualDesconto.HasValue)
                    {
                        desconto = (ValorTotal * Voucher.PercentualDesconto.Value) / 100;
                        valor -= desconto;
                    }
                    break;
                default:
                    break;
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory //Classe Alinhada 
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido { Clienteid = clienteId };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
