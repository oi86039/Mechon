using System;
using DisablerAi.Interfaces;
using DisablerAi_Implemented;

namespace DisablerAi
{
    public class PlayerLocation
    {
        public DateTime Time { get; }
        public Location Location { get; }

        public bool Seen { get; set; }

        public bool Heard { get; set; }

        public PlayerLocation(DateTime time, Location location, bool seen, bool heard)
        {
            this.Time = time;
            this.Location = location;
            this.Seen = seen;
            this.Heard = heard;
        }
    }
}