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
            _Player = new WindowsMediaPlayer();
            _Player.URL = fileName;
            _Player.PlayStateChange += _Player_PlayStateChange;
            _Finished = false;
        }

        private bool _Finished;
        private WindowsMediaPlayer _Player;
        
        public void Play()
        {
            DateTime start = DateTime.Now;
            _Player.controls.play();
            while (!_Finished && DateTime.Now.Minute - start.Minute <= 3)
            {
            }
        }

        private void _Player_PlayStateChange(int NewState)
        {
            if (((WMPPlayState)NewState) == WMPPlayState.wmppsStopped)
                _Finished = true;
        }

        public void Dispose()
        {
            if (_Player != null)
            {
                _Player.close();
                _Player = null;
            }
        }
    }
}
