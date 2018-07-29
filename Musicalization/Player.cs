using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Musicalization
{
	public class Player : IDisposable
	{
		public Player(string fileName)
		{
			_Player = new WindowsMediaPlayer
			{
				URL = fileName
			};
			_Player.PlayStateChange += _Player_PlayStateChange;
			_Finished = false;
		}

		private bool _Finished;
		private WindowsMediaPlayer _Player;

		public void Play()
		{
			DateTime start = DateTime.Now;
			_Player.controls.play();
			while (!_Finished && DateTime.Now.Minute - start.Minute <= 3) { }
		}

		private void _Player_PlayStateChange(int NewState)
		{
			if (((WMPPlayState)NewState) == WMPPlayState.wmppsStopped)
				_Finished = true;
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (_Player != null)
					{
						_Player.close();
						_Player = null;
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
