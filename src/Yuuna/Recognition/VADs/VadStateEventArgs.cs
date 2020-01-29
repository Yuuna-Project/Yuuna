
namespace Yuuna.Recognition.VADs
{

    using System;

    public sealed class VadStateEventArgs : EventArgs
    {
        internal VadStateEventArgs(VadState vadState, double decibel)
        {
            this.State = vadState;
            this.Decibel = decibel;
        }

        public VadState State { get; }
        public double Decibel { get; }
    }

}
