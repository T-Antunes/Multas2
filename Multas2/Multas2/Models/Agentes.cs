using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multas2.Models
{
    public class Agentes
    {
        /// atributos:
        /// - nº agente
        /// - nome
        /// - esquadra
        /// - foto

        public int ID { get; set; }

        public string Nome { get; set; }

        public string Esquadra { get; set; }

        public string Fotografia { get; set; }


        // lista de multas associadas ao Agente

        public ICollection<Multas> ListaDeMultas { get; set; }


    }
}