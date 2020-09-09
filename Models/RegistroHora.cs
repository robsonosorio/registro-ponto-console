using System;
using System.Collections.Generic;
using System.Text;

namespace registroPontoConsole.Models
{
    public class RegistroHora
    {
        public DateTimeOffset Date { get; set; }
        public char Indicador { get; set; } 

        public RegistroHora()
        {
        }

        public RegistroHora(DateTimeOffset date, char indicador)
        {
            Date = date;
            Indicador = indicador;
        }

        public override string ToString()
        {
            return "Data: " + Date.ToString("dd/MM/yyyy -- HH:mm")
                + " | Indicador: " + Indicador;
        }
    }
}
