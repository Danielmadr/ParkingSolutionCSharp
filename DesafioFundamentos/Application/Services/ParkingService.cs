using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Validators;
using DesafioFundamentos.Models.Entities;
using DesafioFundamentos.Models.Enums;

namespace Application.Services
{
    public class ParkingService
    {
        private List<Vehicle> vehicleList = new List<Vehicle>();

        public void AdicionarVeiculo()
        {
            string placa = "";
            int anwser = 0;

            do
            {
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                placa = Console.ReadLine();

                //Verifica se a placa segue o padrão 
            } while (PlateValidator.IsValid(placa.ToUpper()) == false);


            Console.WriteLine("Digite o tipo do veículo:\n  1-Carro\n  2-Moto");
            while (anwser != 1 && anwser != 2)
            {
                Console.WriteLine("Opção inválida. Digite 1 para Carro ou 2 para Moto.");
                int.TryParse(Console.ReadLine(), out anwser);
                Console.WriteLine(anwser);
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
                if (totalParkingHours  < 1)
                {  
                    switch (foundVehicle.Type)
                    {
                        case VehicleType.Car:
                            totalParkingCost = 5;
                            break;
                        case VehicleType.Motorcycle:
                            totalParkingCost = 3;
                            break;
                    }
                }else
                {
                    switch (foundVehicle.Type)
                    {
                        case VehicleType.Car:
                            totalParkingCost = 5 + (decimal)((totalParkingHours - 1) * 2);
                            break;
                        case VehicleType.Motorcycle:
                            totalParkingCost = 3 + (decimal)((totalParkingHours - 1) * 1.5);
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
    }
}