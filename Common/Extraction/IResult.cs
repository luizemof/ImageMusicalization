using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extraction
{
    /// <summary>
    /// Interface que representa um resultado
    /// </summary>
    public interface IResult
    {
        Point Coordinator { get; set; }
        Color Pixel { get; set; }
        EModelType ModelType { get; }
        ENote Note { get; }
        string SoundNote { get; }
		int NumberOfElements { get; set; }

		/// <summary>
		/// Gera a nota
		/// </summary>
		void GenerateNote();
    }
}
