using System;
using System.Collections.Generic;
using System.Text;

namespace WebPessoa.Application.Pessoa
{
    public class PessoaHistoricoResponse: PessoaRequest
    {
        public int id { get; set; }
        public int Idade { get; set; }
        public decimal Imc { get; set; }
        public string Classificacao { get; set; }
        public double Inss { get; set; }
        public double Aliquota { get; set; }

        public double SalarioLiquido { get; set; }
        public decimal SaldoDolar { get; set; }

        public int idUsuario { get; set; }
    }
}
