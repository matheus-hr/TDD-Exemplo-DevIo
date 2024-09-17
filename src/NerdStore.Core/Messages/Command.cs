using FluentValidation.Results;
using MediatR;

namespace NerdStore.Core.Messages
{
    public abstract class Command : Message, //Command e querie pode ser considerado um message
                                    IRequest<bool> //Para funcionar com o MediatR. Retorna um bool
    {
        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public abstract bool EhValido();
    }
}
