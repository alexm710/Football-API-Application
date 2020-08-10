using System;
using System.Collections.Generic;
using System.Text;

namespace football_from_scratch_client
{
    public partial class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamStadium { get; set; }
        public string TeamLocation { get; set; }
        public int? PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
