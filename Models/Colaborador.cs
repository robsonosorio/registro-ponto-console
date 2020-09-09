using System;
using System.Collections.Generic;
using System.Text;

namespace registroPontoConsole.Models
{
    public abstract class Colaborador
    {
        public Guid Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }

        protected Colaborador(Guid id, int matricula, string nome)
        {
            Id = id;
            Matricula = matricula;
            Nome = nome;
        }
    }
}
