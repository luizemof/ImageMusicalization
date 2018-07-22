using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.General
{
   /// <summary>
    /// Classe que representa o cubo de musicalização
    /// </summary>
    public class CubeMusicalization
    {
        public CubeMusicalization()
        {
            this.ColorNotes = new List<ColorNote>();
        }

        public List<ColorNote> ColorNotes { get; set; }
    }
}
