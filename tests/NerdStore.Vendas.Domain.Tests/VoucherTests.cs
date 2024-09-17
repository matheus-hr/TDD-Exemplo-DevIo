using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validar voucher Tipo Valor Valido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("PROMO-15-REAIS", null, 15, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false );

            //Act
            var result = voucher.ValidarSeAplicavel();

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar voucher Tipo Valor Invalido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, 0, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(-1), false, true);

            //Act
            var result = voucher.ValidarSeAplicavel();

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErriMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.ValorDescontoErroMsg, result.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "Validar voucher Tipo Porcentagem Valido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoPorcemtagem_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("PROMO-10-PORCENTO", 10, null, 1, TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(15), true, false);

            //Act
            var result = voucher.ValidarSeAplicavel();

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar voucher Tipo Porcentagem Invalido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoPorcentagem_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, 0, TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(-1), false, true);

            //Act
            var result = voucher.ValidarSeAplicavel();

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErriMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.PercentualDescontoErroMsg, result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
