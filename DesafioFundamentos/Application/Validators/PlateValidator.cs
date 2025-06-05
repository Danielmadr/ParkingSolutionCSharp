using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DesafioFundamentos.Models.Exceptions;

namespace Application.Validators
{
    public class PlateValidator
    {
        public static bool IsValid(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
            {
                throw new InvalidPlateFormatException("A placa não pode ser nula ou vazia.");
            }

            if (plate.Length != 7)
            {
                throw new InvalidPlateFormatException("A placa deve ter exatamente 7 caracteres.");
            }

            var oldPlatePattern = new Regex(@"^[A-Za-z]{3}[0-9]{4}$");
            var mercosulPlatePattern = new Regex(@"^[A-Za-z]{3}[0-9][A-Za-z][0-9]{2}$");

            if (!oldPlatePattern.IsMatch(plate) && !mercosulPlatePattern.IsMatch(plate))
            {
                throw new InvalidPlateFormatException("A placa deve seguir o padrão antigo (ABC1234) ou Mercosul (ABC1D23).");
            }

            return true;
        }
    }
}