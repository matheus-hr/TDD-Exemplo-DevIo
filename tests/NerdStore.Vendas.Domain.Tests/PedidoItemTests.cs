using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    //Esse comportamento/funcionalidade existe dentro da Item Pedido, por isso ele foi movido pra uma classe de PedidoItem
    public class PedidoItemTests
    {
        [Fact(DisplayName = "Novo Item Pedido com Unidades Abaixo do Permitido")]
        [Trait("Categoria", "Vendas - Pedido Item")]
        public void AdicionarItemPedido_UnidadesItemAbaixoDoPermitido_DeveRetornarException()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Produto Teste", Pedido.MIN_UNIDADES_ITEM - 1, 100));
        }
    }
}
