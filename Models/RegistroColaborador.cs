using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace registroPontoConsole.Models
{
    public class RegistroColaborador
    {
        public Guid Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public List<RegistroHoras> RegistrosDeHoras { get; set; } = new List<RegistroHoras>();

        public RegistroColaborador()
        {
        }

        public RegistroColaborador(Guid id, int matricula, string nome)
        {
            Id = id;
            Matricula = matricula;
            Nome = nome;
        }

        public void AddRegistro(RegistroHoras registroHora)
        {
            RegistrosDeHoras.Add(registroHora);
        }

        public void RemoveRegistro(RegistroHoras registroHora)
        {
            RegistrosDeHoras.Remove(registroHora);
        }

        public override string ToString()
        {
            return "Id: " + Id
                + " | Matricula: " + Matricula
                + " | Nome: " + Nome;
        }
    }
}
