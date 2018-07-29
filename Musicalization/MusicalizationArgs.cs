using Common;
using Common.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalization
{
	/// <summary>
	/// Classe que representa os argumentos necessários para a execução dos modelos para musicalização
	/// </summary>
	public class MusicalizationArgs
	{
		public List<Point> Centers { get; set; }
		public string ModelLog { get; set; }
		public string StateLog { get; set; }
		public string SequenceLog { get; set; }
		public string LogPath { get; set; }
		public string ImageFile { get; set; }
		public bool Parallel { get; set; }
		public int MaxNotes { get; set; }
		public EModelType TargetType { get; set; }
		public bool AnalizeImage { get; set; }
		public string ProbabilityCalculation { get; set; }

		public void Validade()
		{
			if (this.ImageFile == string.Empty)
				throw new Exception("Não foi possível encontrar a imagem nos argumentos");

			if (this.LogPath == null || this.LogPath == string.Empty)
				this.LogPath = string.Concat(Environment.CurrentDirectory, "\\Logs");

			if (this.MaxNotes < 1)
				this.MaxNotes = 30;

			if (this.TargetType == EModelType.KMeans)
				_ValidadeKMeansArguments();
			else if (this.TargetType == EModelType.SURF)
				_ValidadeSURFArguments();
			else
				throw new ArgumentException("Não foi possível achar o modelo");

			_ValidateLog(TargetType);
		}

		private void _ValidateLog(EModelType type)
		{
			string model = type.ToString();
			if (this.ModelLog == string.Empty || this.ModelLog == null)
				this.ModelLog = string.Format("{0}.log", model);

			if (this.StateLog == string.Empty || this.StateLog == null)
				this.StateLog = string.Format("{0}State.log", model);

			if (this.SequenceLog == string.Empty || this.SequenceLog == null)
				this.SequenceLog = string.Format("{0}Sequence.log", model);

			if (this.ProbabilityCalculation == string.Empty || this.ProbabilityCalculation == null)
				this.ProbabilityCalculation = string.Format("{0}Probability.log", model);
		}

		private void _ValidadeSURFArguments()
		{

		}

		private void _ValidadeKMeansArguments()
		{

		}

		public List<Point> ConvertStringToCenter(string p)
		{
			List<Point> value = new List<Point>();
			string[] firstSplit = p.Split(' ');
			string[] secondSplit;
			foreach (string item in firstSplit)
			{
				secondSplit = item.Split(';');

				if (secondSplit.Length != 2)
					throw new Exception("Não foi possível encontrar um centro, valores incorretos. Valor esperado CoordX;CoordY");

				value.Add(new Point(int.Parse(secondSplit[0]), int.Parse(secondSplit[1])));
			}
			return value;
		}
	}
}
