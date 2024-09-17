using FluentValidation;
using FluentValidation.Results;

namespace NerdStore.Vendas.Domain
{
    public class Voucher
    {
        public Voucher(string codigo, decimal? percentualDesconto, decimal? valorDesconto, int quantidade, TipoDescontoVoucher tipoDescontoVoucher, DateTime dataValidade, bool ativo, bool utilizado)
        {
            Codigo = codigo;
            PercentualDesconto = percentualDesconto;
            ValorDesconto = valorDesconto;
            TipoDescontoVoucher = tipoDescontoVoucher;
            Quantidade = quantidade;
            DataValidade = dataValidade;
            Ativo = ativo;
            Utilizado = utilizado;
        }

        public string Codigo { get; private set; }
        public decimal? PercentualDesconto { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public int Quantidade { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        public ValidationResult ValidarSeAplicavel()
        {
            return new VoucherAplicavelValidation().Validate(this);
        }
    }

    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {
        public static string CodigoErroMsg => "Voucher sem codigo válido.";
        public static string DataValidadeErriMsg => "Este voucher está expirado.";
        public static string AtivoErroMsg => "Este vocuher não é mais valido";
        public static string UtilizadoErroMsg => "Este voucher já foi utilizado";
        public static string QuantidadeErroMsg => "Este voucher não está mais disponivel";
        public static string ValorDescontoErroMsg => "O valor do desconto precisa ser superior a 0";
        public static string PercentualDescontoErroMsg => "O valor da porcentagem de desconto precisa ser superior a 0";

        public VoucherAplicavelValidation()
        {
            RuleFor(v => v.Codigo)
                .NotEmpty()
                .WithMessage(CodigoErroMsg);

            RuleFor(v => v.DataValidade)
                .Must(DateVencimentoSperiorAtual)
                .WithMessage(DataValidadeErriMsg);

            RuleFor(v => v.Ativo)
                .Equal(true)
                .WithMessage(AtivoErroMsg);

            RuleFor(v => v.Utilizado)
                .Equal(false)
                .WithMessage(UtilizadoErroMsg);

            RuleFor(v => v.Quantidade)
                .GreaterThan(0)
                .WithMessage(QuantidadeErroMsg);
            When(v => v.TipoDescontoVoucher == TipoDescontoVoucher.Valor, () =>
            {
                RuleFor(v => v.ValorDesconto)
                .NotNull()
                .WithMessage(ValorDescontoErroMsg)
                .GreaterThan(0)
                .WithMessage(ValorDescontoErroMsg);
            });

            When(v => v.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem, () =>
            {
                RuleFor(v => v.PercentualDesconto)
                .NotNull()
                .WithMessage(PercentualDescontoErroMsg)
                .GreaterThan(0)
                .WithMessage(PercentualDescontoErroMsg);
            });
        }

        protected static bool DateVencimentoSperiorAtual(DateTime dataValidade)
        {
            return dataValidade >= DateTime.Now;
        }
    }
}
