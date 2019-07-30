using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Model
{
    class Graph
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

        public int GetDistance(UserNode user1, UserNode user2)
        {
            return 0;
        }
    }
}
