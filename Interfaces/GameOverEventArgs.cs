using System;
using System.Collections.Generic;
using System.Text;

namespace GameMaster
{
    public class GameOverEventArgs : EventArgs
    {
        IEnumerable<Tuple<IPlayer, int>> ScoreTable { get; }

        public GameOverEventArgs(IEnumerable<Tuple<IPlayer, int>> scores)
        {
            ScoreTable = scores;
        }
    }
}
