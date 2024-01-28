using Microsoft.AspNetCore.Mvc;
using System;

public class LekaController : Controller
{
    [HttpGet]
    public IActionResult LekaDispesas()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LekaDispesas(string mes, int diasmes, int plantoes, int faltas, int faltasplantoes, string atipico, string hextra, decimal hextradia, bool gerarexcel)
    {
        try
        {
            // Cálculos
            var qtdHoraNormal = (diasmes - plantoes) * 7.2m;
            var qtdHoraPlantao = plantoes * 9.0m;
            var qtdHorasFaltasSemPlantao = (faltas - faltasplantoes) * 7.2m;
            var qtdHorasFaltasComPlantao = faltasplantoes * 9.0m;
            var faltasTotais = qtdHorasFaltasComPlantao + qtdHorasFaltasSemPlantao;
            var totalHorasExtras = hextra == "Sim" ? hextradia : 0; // Assumindo que hextra é uma string "Sim" ou "Não"
            var total = (qtdHoraNormal + qtdHoraPlantao + totalHorasExtras) - faltasTotais;
            var valorTotalAReceber = total * 28;

            // Entrada dos dias faltados
            Console.WriteLine("Quais dias você faltou? (Informe apenas os dias, separados por vírgula (Exemplo: 01, 02, 12)): ");
            string[] diasFaltados = Console.ReadLine()!.Split(',');

            // Entrada dos dias faltados em plantão
            Console.WriteLine("Dos dias faltados, quais dias foram plantões? (Informe apenas os dias, separados por vírgula (Exemplo: 01, 12)): ");
            string[] diasFaltadosEmPlantao = Console.ReadLine()!.Split(',');

            // Saída
            Console.Clear();
            Console.WriteLine("Informações úteis!\n");
            Console.WriteLine($"Mês: {mes};\n");
            Console.WriteLine($"Quantos plantões realizados: {plantoes};\n");
            Console.WriteLine($"Quantidade de faltas: {faltas}; Em horas: {faltasTotais};\n");
            Console.WriteLine($"Quantidade de horas extras: {totalHorasExtras};\n");
            Console.WriteLine($"Quantidade de horas trabalhadas: {total};\n");
            Console.WriteLine($"Quantidade a receber: R${valorTotalAReceber};\n");

            if (faltas >= 1)
            {
                Console.WriteLine($"Dias que faltou: {string.Join(", ", diasFaltados)};");
            }
            else
            {
                Console.WriteLine("Dias que faltou: Nenhum.");
            }
            if (faltasplantoes == 0)
            {
                Console.WriteLine("Faltas em plantão: Nenhuma.");
            }
            else
            {
                Console.WriteLine($"Faltas em plantão: {string.Join(", ", diasFaltadosEmPlantao)};");
            }

            return View("LekaResultado", valorTotalAReceber); // Substitua "LekaResultado" pelo nome da sua view de resultados
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Erro: {ex.Message}";
            return View("Error! Return");
        }
    }
}
