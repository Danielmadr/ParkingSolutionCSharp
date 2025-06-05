using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioFundamentos.Models.Enums;

namespace DesafioFundamentos.Models.Entities
{
    public class Vehicle
    {
        private static int _nextId = 1;
        public Vehicle(string plate, VehicleType type, DateTime entryDate, DateTime? exitDate = null)
        {
            Id = _nextId++;
            Plate = plate;
            Type = type;
            EntryDate = entryDate;
            ExitDate = exitDate;
        }

        public int Id { get; private set; }
        public string Plate { get; set; }
        public VehicleType Type { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; } 
    
    }
}