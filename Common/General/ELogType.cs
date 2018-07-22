using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.General
{
    /// <summary>
    /// Tipo de log do sistema
    /// </summary>
    public enum ELogType
    {
        [Description("Desconhecido")]
        Unkown = 0,

        /// <summary>
        /// Extração de características
        /// </summary>
        [Description("Extração")]
        Extraction = 1,
        /// <summary>
        /// Geração de estados
        /// </summary>
        [Description("Geração Estados")]
        GenerationStates = 2,
        /// <summary>
        /// Sequencia de notas geradas
        /// </summary>
        [Description("Sequência")]
        Sequence = 3,

        /// <summary>
        /// Cálculo de probabilidade
        /// </summary>
        [Description("Cálculo de Probabilidade")]
        ProbabilityCalculation = 4
    }
}
