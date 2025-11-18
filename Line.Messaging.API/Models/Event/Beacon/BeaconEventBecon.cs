using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Webhook;

namespace Line.Messaging.API.Models.Event.Beacon
{
    public class BeaconEventBecon
    {
        public string Hwid { get; }
        public string Type { get; }
        public string? DM { get; }
        public BeaconEventBecon(string hwid, string type, string? dm = null)
        {
            Hwid = hwid;
            Type = type;
            DM = dm;
        }
        internal static BeaconEventBecon Create(dynamic beacon)
        {
            return new BeaconEventBecon((string)beacon.hwid, (string)beacon.type, (string?)beacon.dm);
        }
    }
}
