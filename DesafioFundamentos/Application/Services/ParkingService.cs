using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Validators;
using DesafioFundamentos.Models.Entities;
using DesafioFundamentos.Models.Enums;
using DotNetEnv;

namespace Application.Services
{
    public class ParkingService
    {
        private List<Vehicle> vehicleList = new List<Vehicle>();
        private decimal INITIAL_PRICE_CAR;
        private decimal INITIAL_PRICE_MOTORCYCLE;
        private decimal PRICE_PER_HOUR_FOR_CAR;
        private decimal PRICE_PER_HOUR_FOR_MOTORCYCLE;

        public ParkingService()
        {
            Env.Load();
            INITIAL_PRICE_CAR = decimal.Parse(Environment.GetEnvironmentVariable("INITIAL_PRICE_CAR"));
            INITIAL_PRICE_MOTORCYCLE = decimal.Parse(Environment.GetEnvironmentVariable("INITIAL_PRICE_MOTORCYCLE"));
            PRICE_PER_HOUR_FOR_CAR = decimal.Parse(Environment.GetEnvironmentVariable("PRICE_PER_HOUR_CAR"));
            PRICE_PER_HOUR_FOR_MOTORCYCLE = decimal.Parse(Environment.GetEnvironmentVariable("PRICE_PER_HOUR_MOTORCYCLE"));
        }

        public void AdicionarVeiculo()
        {
            string placa = "";
            int anwser = 0;
            bool test = false;

            do
            {
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                placa = Console.ReadLine();

                try
                {
                    test = PlateValidator.IsValid(placa.ToUpper());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (test == false);


            while (anwser != 1 && anwser != 2)
            {
                Console.WriteLine("Digite o tipo do veículo:\n  1-Carro\n  2-Moto");
                int.TryParse(Console.ReadLine(), out anwser);
                if (anwser != 1 && anwser != 2)
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }
            }

            Vehicle newVehicle = new Vehicle(
                placa,
                anwser == 1 ? VehicleType.Car : VehicleType.Motorcycle,
                DateTime.Now
            );

            vehicleList.Add(newVehicle);
        }

        public void RemoverVeiculo()
        {
            ListarVeiculos();
            Console.WriteLine("Digite a placa do veículo que deseja fazer checkout:");
            string placa = Console.ReadLine();
            string currentTime = "";

            Vehicle foundVehicle = vehicleList.FirstOrDefault(x => x.Plate.ToUpper() == placa.ToUpper());

            if (foundVehicle != null)
            {
                Console.WriteLine("Digite a hora atual (Exemplo: 14:30):");
                DateTime userExitTime;
                while (!DateTime.TryParse(currentTime = Console.ReadLine(), out userExitTime))
                {
                    Console.WriteLine("Hora inválida. Digite novamente (Exemplo: 14:30):");
                }

                // Calcula o tempo total de estacionamento em horas
                double totalParkingHours = (userExitTime - foundVehicle.EntryDate).TotalHours;
                Console.WriteLine($"O veículo {foundVehicle.Plate} foi estacionado as {foundVehicle.EntryDate.TimeOfDay} e retirado as {userExitTime.TimeOfDay} ficando estacionado por {totalParkingHours} horas.");

                if (totalParkingHours < 0)
                {

                    Console.WriteLine("A hora de saída não pode ser menor que a hora de entrada.");
                    return;
                }

                Decimal totalParkingCost = 0;
                if (totalParkingHours < 1)
                {
                    switch (foundVehicle.Type)
                    {
                        case VehicleType.Car:
                            totalParkingCost = INITIAL_PRICE_CAR;
                            break;
                        case VehicleType.Motorcycle:
                            totalParkingCost = INITIAL_PRICE_MOTORCYCLE;
                            break;
                    }
                }
                else
                {
                    switch (foundVehicle.Type)
                    {
                        case VehicleType.Car:
                            totalParkingCost = INITIAL_PRICE_CAR + decimal.Multiply((decimal)(totalParkingHours - 1), PRICE_PER_HOUR_FOR_CAR);
                            break;
                        case VehicleType.Motorcycle:
                            totalParkingCost = INITIAL_PRICE_MOTORCYCLE + decimal.Multiply((decimal)(totalParkingHours - 1), PRICE_PER_HOUR_FOR_MOTORCYCLE);
                            break;
                    }
                }
                // Remove o veículo da lista
                vehicleList.Remove(foundVehicle);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {totalParkingCost}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (vehicleList.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var vehicle in vehicleList)
                {
                    Console.WriteLine($"Placa: {vehicle.Plate}, Tipo: {vehicle.Type}, Entrada: {vehicle.EntryDate}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
        
        public void ConsultarPreços()
        {
            Console.WriteLine("Preços de estacionamento:");
            Console.WriteLine($"Carro: R$ {INITIAL_PRICE_CAR} para a primeira hora, R$ {PRICE_PER_HOUR_FOR_CAR} por hora adicional.");
            Console.WriteLine($"Moto: R$ {INITIAL_PRICE_MOTORCYCLE} para a primeira hora, R$ {PRICE_PER_HOUR_FOR_MOTORCYCLE} por hora adicional.");
        }
    }
}