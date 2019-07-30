using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Model
{
    public class Graph
    {
        private readonly List<UserNode> _users = new List<UserNode>();

        public IEnumerable<UserNode> Users
        {
            get
            {
                return _users.AsEnumerable();
            }
        }

        public void Add(UserNode user)
        {
            _users.Add(user);
        }

        public void AddRange(IEnumerable<UserNode> users)
        {
            _users.AddRange(users);
        }

        /// <summary>
        /// Gets the distance between two users in the graph using BFS.
        /// </summary>
        /// <param name="user1">Starting user.</param>
        /// <param name="user2">Target user.</param>
        /// <returns>The distance between two users or -1 if they have no connections.</returns>
        public int GetDistance(UserNode user1, UserNode user2)
        {
            if (user1 == user2)
            {
                return 0;
            }

            if (user1.Friends.Contains(user2))
            {
                return 1;
            }

            Queue<FriendDistance> queue = new Queue<FriendDistance>();
            HashSet<UserNode> checkedFriends = new HashSet<UserNode>();

            foreach (var friend in user1.Friends)
            {
                queue.Enqueue(new FriendDistance { Friend = friend, Distance = 1 });
            }

            while (queue.Count > 0)
            {
                var friendDistance = queue.Dequeue();
                foreach (var friend in friendDistance.Friend.Friends)
                {
                    if (friend == user2)
                    {
                        return friendDistance.Distance + 1;
                    }
                    else
                    {
                        if (!checkedFriends.Contains(friend))
                        {
                            queue.Enqueue(new FriendDistance { Friend = friend, Distance = friendDistance.Distance + 1 });
                            checkedFriends.Add(friend);
                        }
                    }
                }
            }

            return -1;
        }

        public List<UserNode> GetFriendsOfFriends(UserNode user, int depth)
        {
            if (depth < 1)
            {
                throw new ArgumentOutOfRangeException("Depth must be at least 1.");
            }

            return null;
        }

        public class FriendDistance
        {
            public UserNode Friend { get; set; }
            public int Distance { get; set; }
        }
    }
}
