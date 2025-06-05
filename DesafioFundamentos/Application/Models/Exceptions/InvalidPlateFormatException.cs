using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFundamentos.Models.Exceptions
{
    public class InvalidPlateFormatException : Exception
    {
        public InvalidPlateFormatException()
            : base("Formato de placa inválido. Placa modelo antigo: (exemplo: ABC1234). Placa padrão Mercosul: (exemplo: ABC1D23).")
        {
        }

        public InvalidPlateFormatException(string message)
            : base(message)
        {
        }

        public InvalidPlateFormatException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}