using System;
using System.Collections.Generic;
using System.Text;

namespace GameMaster
{
    public class TrickTakenEventArgs : EventArgs
    {
        public IPlayer Player { get; }

        public TrickTakenEventArgs(IPlayer player)
        {
            Player = player;
        }
    }
}
