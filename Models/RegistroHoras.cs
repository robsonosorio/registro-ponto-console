using System;
using System.Collections.Generic;
using System.Text;

namespace registroPontoConsole.Models
{
    public class RegistroHoras
    {
        public DateTimeOffset Date { get; set; }
        public char Indicador { get; set; }

        public RegistroHoras()
        {
        }

        public RegistroHoras(DateTimeOffset date, char indicador)
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
