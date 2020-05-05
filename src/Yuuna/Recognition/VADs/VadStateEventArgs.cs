// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Recognition.VADs
{
    using System;

    public sealed class VadStateEventArgs : EventArgs
    {
        public double Decibel { get; }

        public VadState State { get; }

        internal VadStateEventArgs(VadState vadState, double decibel)
        {
            this.State = vadState;
            this.Decibel = decibel;
        }
    }
}