﻿using Application.Services;
using DesafioFundamentos.Models;
using DotNetEnv;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\n");

// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
ParkingService es = new ParkingService();

string opcao = string.Empty;
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Entrada de veículo");
    Console.WriteLine("2 - Saída de veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Consultar preço por veículo");
    Console.WriteLine("5 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            es.AdicionarVeiculo();
            break;

        case "2":
            es.RemoverVeiculo();
            break;

        case "3":
            es.ListarVeiculos();
            break;

        case "4":
            es.ConsultarPreços();
            break;   

        case "5":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");
