using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.VideoPlayComplete
{
    public class VideoPlayCompleteEventVideoPlayComplete
    {
        public string TrackingId { get; }
        public VideoPlayCompleteEventVideoPlayComplete(string trackingId)
        {
            TrackingId = trackingId;
        }

        internal static VideoPlayCompleteEventVideoPlayComplete Create(dynamic videoPlayComplete)
        {
            return new VideoPlayCompleteEventVideoPlayComplete((string)videoPlayComplete.trackingId);
        }
    }
}
