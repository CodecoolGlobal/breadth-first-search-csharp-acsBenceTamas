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

            int currentDepth = 1;

            HashSet<UserNode> friends = new HashSet<UserNode>();
            Queue<FriendDistance> friendsToDeepen = new Queue<FriendDistance>();
            HashSet<UserNode> friendsDeepened = new HashSet<UserNode>();

            foreach (var friend in user.Friends)
            {
                friends.Add(friend);
                friendsDeepened.Add(friend);
            }
            if (depth > currentDepth)
            {
                foreach (var friend in user.Friends)
                {
                    friendsDeepened.Add(friend);
                    friendsToDeepen.Enqueue(new FriendDistance { Friend = friend, Distance = currentDepth });
                }
            }

            while (friendsToDeepen.Count > 0)
            {
                var friendDistance = friendsToDeepen.Dequeue();
                friends.Add(friendDistance.Friend);
                if (friendDistance.Distance < depth)
                {
                    foreach (var friend in friendDistance.Friend.Friends)
                    {
                        if (!friendsDeepened.Contains(friend))
                        {
                            friendsToDeepen.Enqueue(new FriendDistance { Friend = friend, Distance = currentDepth });
                            friendsDeepened.Add(friend);
                        }
                    }
                }
            }

            return friends.ToList();
        }

        public List<LinkedList<UserNode>> GetShortestPaths(UserNode user1, UserNode user2)
        {
            var shortestPaths = new List<LinkedList<UserNode>>();

            if (user1 == user2)
            {
                shortestPaths.Add(new LinkedList<UserNode>(new UserNode[] { user1 }));
                return shortestPaths;
            }

            if (user1.Friends.Contains(user2))
            {
                shortestPaths.Add(new LinkedList<UserNode>(new UserNode[] { user1, user2 }));
                return shortestPaths;
            }

            var queue = new Queue<LinkedList<UserNode>>();
            var checkedFriends = new HashSet<UserNode>();
            var shouldQueueMore = true;

            foreach (var friend in user1.Friends)
            {
                var list = new LinkedList<UserNode>(new UserNode[] { user1, friend });
                queue.Enqueue(list);
                checkedFriends.Add(friend);
            }

            while (queue.Count > 0)
            {
                var list = queue.Dequeue();
                foreach (var friend in list.Last.Value.Friends)
                {
                    var newList = new LinkedList<UserNode>(list);
                    newList.AddLast(friend);
                    if (friend == user2)
                    {
                        shortestPaths.Add(newList);
                        shouldQueueMore = false;
                    }
                    if (!checkedFriends.Contains(friend))
                    {
                        checkedFriends.Add(friend);
                    }
                    if (shouldQueueMore)
                    {
                        queue.Enqueue(newList);
                    }
                }
            }

            return shortestPaths;

        }

        public class FriendDistance
        {
            public UserNode Friend { get; set; }
            public int Distance { get; set; }
        }
    }
}
