using System;
using System.Collections.Generic;
using System.Text;

namespace football_from_scratch_client
{
    public partial class Player
    {
        public override string ToString()
        {
            return ($"{PlayerId}, {PlayerName}, {PlayerAge}, {PlayerPosition}, {TeamName}");
        }
    }
}
