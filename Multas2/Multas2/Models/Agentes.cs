﻿using System;
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

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Esquadra { get; set; }

        public string Foto { get; set; }


    }
}