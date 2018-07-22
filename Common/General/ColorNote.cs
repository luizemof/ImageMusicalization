using Common.Extraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.General
{
    /// <summary>
    /// Estrutura que representa a nota e a cor correspondente
    /// </summary>
    public struct ColorNote
    {
        public static ColorNote Empty
        {
            get
            {
                return new ColorNote()
                {
                    Note = ENote.Unknow,
                    Color = Color.Empty
                };
            }
        }

        public ENote Note { get; set; }
        public Color Color { get; set; }
    }
}
