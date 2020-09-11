using System;
using System.Collections.Generic;

namespace registroPontoConsole.Models
{
    public class RegistroPonto : Colaborador
    {

        public List<RegistroHora> RegistroDeHoras { get; set; } = new List<RegistroHora>();
        public List<DateTimeOffset> RegistroDeEntradas { get; set; } = new List<DateTimeOffset>();
        public List<DateTimeOffset> RegistroDeSaidas { get; set; } = new List<DateTimeOffset>();

        public RegistroPonto(Guid id, int matricula, string nome) : base(id, matricula, nome)
        {
            Id = id;
            Matricula = matricula;
            Nome = nome;
        }

        public void AddRegistro(RegistroHora registroHora)
        {
            RegistroDeHoras.Add(registroHora);
        }
        public void AddEntrada(DateTimeOffset entrada)
        {
            RegistroDeEntradas.Add(entrada);
        }
        public void AddSaida(DateTimeOffset saida)
        {
            RegistroDeSaidas.Add(saida);
        }

        public Boolean VerificarStatus()
        {
            if (RegistroDeEntradas.Count == RegistroDeSaidas.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal HorasTrabalhadas()
        {
            decimal horasTrabalhadas = 0;

            foreach (var entrada in RegistroDeEntradas)
            {
                foreach (var saida in RegistroDeSaidas)
                {
                    if (RegistroDeEntradas.IndexOf(entrada) == RegistroDeSaidas.IndexOf(saida))
                    {
                        TimeSpan time = saida.Subtract(entrada);
                        horasTrabalhadas += time.Hours;
                    }
                }
            }
            return horasTrabalhadas;
        }

        public override string ToString()
        {
            return "Id: " + Id
                + " | Matricula: " + Matricula
                + " | Nome: " + Nome;
        }
    }
}
