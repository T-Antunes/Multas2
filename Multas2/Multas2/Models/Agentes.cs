using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public int ID { get; set; }
        
        [Required (ErrorMessage = "Este campo é de preenchimento obrigatório")]
        [StringLength(40)]
        [RegularExpression("[A-ZÁÉÍÓÚÂ][a-záéíóúàèìòùäëïöüãõâêîôûçñ]+(( | e | de | da | das | do | dos |-|'|d')[A-ZÁÉÍÓÚÂ][a-záéíóúàèìòùäëïöüãõâêîôûçñ]*){1,3}",
                         ErrorMessage = "só são aceites palavras, começadas por Maiúscula, " +
                                       "separadas por um espeço em branco.")]

        public string Nome { get; set; }

        [Required (ErrorMessage = "Este campo é de preenchimento obrigatório")]
        [StringLength(30)]
      //  [RegularExpression("Torres Novas|Tomar|Entroncamento",
         //                ErrorMessage = " só é aceite Torres Novas ou Tomar ou Entroncamento")]
        public string Esquadra { get; set; }

        [StringLength(40)]
        public string Fotografia { get; set; }


        // lista de multas associadas ao Agente

        public ICollection<Multas> ListaDeMultas { get; set; }


    }
}