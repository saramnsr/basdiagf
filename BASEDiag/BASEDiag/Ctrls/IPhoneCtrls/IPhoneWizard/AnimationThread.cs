using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard
{
    public class IPhoneWizardAnimationThread
    {
        public int AnimationKey = 0;

        public event IPhoneWizardMustCalculateEventHandler MustCalculateEventHandler;

        UserControl _ctrlToAnime;
        public bool _stopthread = false;

        private int _framrateInMillisecond =1000/25;

        public int FrameRate
        {
            get
            {
                return 1000 / _framrateInMillisecond;
            }
            set
            {
                _framrateInMillisecond = 1000 / value;
            }
        }

        public void StopThread()
        {
            _stopthread = true;
        }

        public IPhoneWizardAnimationThread(UserControl ctrl)
        {
            _ctrlToAnime = ctrl;
        }

        public void ThreadRun()
        {
            int MaxKeys = AnimationKey;
            DateTime tick = DateTime.Now;
            _stopthread = false;
            while (!_stopthread)
            {
                if ((DateTime.Now>tick)&&(AnimationKey>=0))
                {
                    if (MustCalculateEventHandler != null) MustCalculateEventHandler(this, new IPhoneWizardMustCalculateEventArgs(AnimationKey, MaxKeys));
                    _ctrlToAnime.Invalidate();
                    tick = DateTime.Now.AddMilliseconds(_framrateInMillisecond);
                    AnimationKey--;
                    if (AnimationKey < 0) _stopthread = true;
                }
            }
        }
    }

    public delegate void IPhoneWizardMustCalculateEventHandler(object sender, IPhoneWizardMustCalculateEventArgs e);

    public class IPhoneWizardMustCalculateEventArgs : EventArgs
    {
        private int _tickNum;
        public int TickNum
        {
            get
            {
                return _tickNum;
            }
            set
            {
                _tickNum = value;
            }
        }
        private int _tickMax;
        public int TickMax
        {
            get
            {
                return _tickMax;
            }
            set
            {
                _tickMax = value;
            }
        }

        public IPhoneWizardMustCalculateEventArgs(int tickNum, int Maxtick)
        {
            _tickNum = tickNum;
            _tickMax = Maxtick;
        }
    }
}
