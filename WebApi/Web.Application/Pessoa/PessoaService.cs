using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPessoa.Repository;
using WebPessoa.Repository.Models;

namespace WebPessoa.Application.Pessoa
{
    public class PessoaService

    {
        private readonly PessoaContext _context;
        public PessoaService(PessoaContext context)
        {
            _context = context;

        }
        public List<PessoaHistoricoResponse> ObterHistoricoPessoas()
        {
            var pessoasDb = _context.Pessoas.ToList();
            var pessoas = new List<PessoaHistoricoResponse>();

            foreach (var item in pessoasDb)
            {
                pessoas.Add(new PessoaHistoricoResponse()
                {
                    Aliquota = Convert.ToDouble(item.aliquota),
                    Altura = item.altura,
                    Classificacao = item.classificacao,
                    DataNascimento = item.dataNascimento,
                    id = item.id,
                    Idade = item.idade,
                    idUsuario = item.idUsuario,
                    Imc = item.imc,
                    Inss = Convert.ToDouble(item.inss),
                    Nome = item.nome,
                    Peso = item.peso,
                    Salario = Convert.ToDouble(item.salario),
                    SalarioLiquido = Convert.ToDouble(item.salarioLiquido),
                    Saldo = item.saldo,
                    SaldoDolar = item.saldoDolar
                });
                
            }
            return pessoas;
        }
        public PessoaResponse ProcesarInformacoesPessoa(PessoaRequest request, int usuarioId)
        {
            var idade = CalcularIdade(request.DataNascimento);
            var imc = CalcularImc(request.Peso, request.Altura);
            var classificacao = CalcularClassificacao(imc);
            var aliquota = CalcularAliquota(request.Salario);
            var inss = CalcularInss(request.Salario, aliquota);
            var salarioLiquido = request.Salario - inss;
            var saldoEmDolar = CalcularDolar(request.Saldo);

            var resposta = new PessoaResponse();

            resposta.SaldoDolar = saldoEmDolar;
            resposta.SalarioLiquido = salarioLiquido;
            resposta.Aliquota = aliquota;
            resposta.Classificacao = classificacao;
            resposta.Idade = idade;
            resposta.Imc = imc;
            resposta.Inss = inss;
            resposta.Nome = request.Nome;

            var pessoa = new TabPessoa()
            {
                aliquota = Convert.ToDecimal(aliquota),

                altura = request.Altura,
                classificacao = classificacao,
                dataNascimento = request.DataNascimento,
                idade = idade,
                idUsuario = usuarioId,
                imc = imc,
                inss = Convert.ToDecimal(inss),
                nome = request.Nome,
                peso = request.Peso,
                salario = Convert.ToDecimal(request.Salario),
                salarioLiquido = Convert.ToDecimal(salarioLiquido),
                saldo = request.Saldo,
                saldoDolar = saldoEmDolar,
            };
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            return resposta;

        }
        private int CalcularIdade(DateTime dataNascimento)
        {
            var anoAtual = DateTime.Now.Year;
            var idade = anoAtual - dataNascimento.Year;
            var mesAtual = DateTime.Now.Month;

            if (mesAtual < dataNascimento.Month)
            {
                idade -= 1;
            }
            return idade;
        }
        private decimal CalcularImc(decimal peso, decimal altura)
        {
            var imc = Math.Round(peso / (altura * altura), 2);
            return imc;
        }
        private string CalcularClassificacao(decimal imc)
        {
            var classificacao = "";
            if (imc < 18.5M)
            {
                classificacao = "Magreza";
            }
            else if (imc >= 18.5M && imc <= 24.9M)
            {
                classificacao = "Normal";
            }
            else if (imc >= 25.0M && imc <= 29.9M)
            {
                classificacao = "Sobrepeso";
            }
            else if (imc >= 30.0M && imc <= 39.9M)
            {
                classificacao = "Obesidade";
            }
            else
            {
                classificacao = "Obesidade grave";
            }
            return classificacao;
        }
        private double CalcularAliquota(double salario)
        {
            var aliquota = 0.0;

            if (salario <= 1302.00)
            {
                aliquota = 7.5;
            }

            else if (salario >= 1302.01 && salario <= 2571.29)
            {
                aliquota = 9;
            }

            else if (salario >= 2571.30 && salario <= 3856.94)
            {
                aliquota = 12;
            }
            else if (salario >= 3856.95 && salario <= 7507.49)
            {
                aliquota = 14;
            }
            else
            {
                aliquota = 15;
            }
            return aliquota;
        }
        private double CalcularInss(double salario, double aliquota)
        {
            var inss = (salario * aliquota) / 100;
            return inss;
        }
        private decimal CalcularDolar(decimal saldo)
        {
            var dolarCotacao = (decimal)5.14;
            var saldoEmDolar = Math.Round(saldo / dolarCotacao, 2);

            return saldoEmDolar;
        }
    }
}
