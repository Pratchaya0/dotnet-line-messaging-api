using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Follow
{
    public class FollowEventFollow
    {
        public bool IsUnblocked { get; }

        public FollowEventFollow(bool isUnblocked)
        {
            IsUnblocked = isUnblocked;
        }

        internal static FollowEventFollow Create(dynamic follow)
        {
            return new FollowEventFollow((bool)follow.isUnblocked);
        }
    }
}
