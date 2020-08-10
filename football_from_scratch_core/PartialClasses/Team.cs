using System;
using System.Collections.Generic;
using System.Text;

namespace football_from_scratch_client
{
    public partial class Team
    {
        public override string ToString()
        {
            return ($"{TeamId}, {TeamName}, {TeamStadium}, {TeamLocation}");
        }
    }
}