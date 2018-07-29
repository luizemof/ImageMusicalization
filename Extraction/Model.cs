using Common;
using Common.Extraction;
using Common.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Extraction
{
	/// <summary>
	/// Classe abstrata que representa o Modelo que será executado para a extração de característica da imagem
	/// </summary>
	public abstract class Model : IDisposable
	{
		public Model(string imageFile, Log log, bool parallel)
		{
			if (string.IsNullOrWhiteSpace(imageFile))
				throw new ArgumentException(nameof(imageFile));

			_ImageFile = imageFile;
			_Log = log;
			_MyImage = new Bitmap(imageFile);
			_ExecuteParallel = parallel;
		}

		~Model()
		{
			Dispose(false);
		}

		public abstract EModelType Type { get; }
		public abstract List<IResult> Result { get; }

		protected string _ImageFile;
		protected Bitmap _MyImage;
		protected bool _ExecuteParallel;

		private readonly Log _Log;

		public string ImageFile { get { return _ImageFile; } }
		public Bitmap MyImage { get { return _MyImage; } }
		public bool ExecuteParallel { get { return _ExecuteParallel; } }

		public abstract void Execute();

		/// <summary>
		/// Salva a o resultado da imagem pintando os pontos encontrados
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="points"></param>
		public void SaveImageResult(string fileName, List<Point> points)
		{
			Bitmap newImage = _MyImage.Clone() as Bitmap;
			points.ForEach(r => _Draw(newImage, r));

			if (File.Exists(fileName))
				File.Delete(fileName);

			newImage.Save(fileName);
		}

		/// <summary>
		/// Desenha a imagem com os pontos encontrados na execução dos modelos
		/// </summary>
		/// <param name="newImage">Nova imagem que terá uma dada região pintada</param>
		/// <param name="r">ponto a ser pintado</param>
		private void _Draw(Bitmap newImage, Point r)
		{
			int x, y;
			for (int i = -6; i < 6; i++)
				for (int j = -6; j < 6; j++)
				{
					x = r.X + i;
					y = r.Y + j;
					if (x >= 0 && x < newImage.Width && y >= 0 && y < newImage.Height)
						newImage.SetPixel(x, y, Color.White);
				}
		}

		protected void WriteLog(string message, int level = 0, bool sameLine = false)
		{
			if (_Log != null)
				lock (_Log)
					_Log.WriteLog(message, level, sameLine);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if(_MyImage != null)
					{
						_MyImage.Dispose();
						_MyImage = null;
					}
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}
		#endregion
	}
}
