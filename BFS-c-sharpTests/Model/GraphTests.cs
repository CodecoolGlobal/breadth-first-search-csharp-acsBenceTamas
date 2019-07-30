using Microsoft.VisualStudio.TestTools.UnitTesting;
using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Model.Tests
{
    [TestClass()]
    public class GraphTests
    {
        [TestMethod()]
        public void GetDistance_GivenSameUser_Returns0()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");

            graph.Add(user1);

            int distance = graph.GetDistance(user1, user1);
            int expected = 0;

            Assert.AreEqual(expected, distance);
        }

        [TestMethod()]
        public void GetDistance_GivenNoConnections_ReturnsMinus1()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "B");

            graph.Add(user1);
            graph.Add(user2);

            int distance = graph.GetDistance(user1, user2);
            int expected = -1;

            Assert.AreEqual(expected, distance);
        }

        [TestMethod()]
        public void GetDistance_GivenDirectFriends_Returns1()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "C");

            user1.AddFriend(user2);

            graph.Add(user1);
            graph.Add(user2);

            int distance = graph.GetDistance(user1, user2);
            int expected = 1;

            Assert.AreEqual(expected, distance);
        }

        [TestMethod()]
        public void GetDistance_GivenSingleCommonFriend_Returns2()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "B");
            UserNode user3 = new UserNode("A", "C");

            user1.AddFriend(user2);
            user3.AddFriend(user2);

            graph.Add(user1);
            graph.Add(user2);
            graph.Add(user3);

            int distance = graph.GetDistance(user1, user3);
            int expected = 2;

            Assert.AreEqual(expected, distance);
        }

        [TestMethod()]
        public void GetDistance_GivenMultipleCommonFriends_Returns2()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "B");
            UserNode user2b = new UserNode("A", "C");
            UserNode user3 = new UserNode("A", "D");

            user1.AddFriend(user2);
            user3.AddFriend(user2);
            user1.AddFriend(user2b);
            user3.AddFriend(user2b);

            graph.Add(user1);
            graph.Add(user2);
            graph.Add(user2b);
            graph.Add(user3);

            int distance = graph.GetDistance(user1, user3);
            int expected = 2;

            Assert.AreEqual(expected, distance);
        }

        [TestMethod()]
        public void GetDistance_GivenNoCommonFriendsAtWithDistanceOf3WithMultipleRoutes_Returns3()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "B");
            UserNode user2b = new UserNode("A", "C");
            UserNode user3 = new UserNode("A", "D");
            UserNode user3b = new UserNode("A", "E");
            UserNode user3c = new UserNode("A", "F");
            UserNode user4 = new UserNode("A", "G");

            user1.AddFriend(user2);
            user1.AddFriend(user2b);
            user2.AddFriend(user3b);
            user3.AddFriend(user2);
            user3.AddFriend(user2b);
            user3.AddFriend(user3b);
            user3.AddFriend(user3c);
            user4.AddFriend(user3);

            graph.Add(user1);
            graph.Add(user2);
            graph.Add(user2b);
            graph.Add(user3);
            graph.Add(user3b);
            graph.Add(user3c);
            graph.Add(user4);

            int distance = graph.GetDistance(user1, user4);
            int expected = 3;

            Assert.AreEqual(expected, distance);
        }

        [TestMethod()]
        public void GetFriendsOfFriends_GivenADepthOf0_ThrowsArgumentOutOfRangeException()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");

            graph.Add(user1);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => graph.GetFriendsOfFriends(user1, 0));
        }

        [TestMethod()]
        public void GetFriendsOfFriends_GivenADepthOf1WithNoFriends_ReturnsEmptyList()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");

            graph.Add(user1);

            List<UserNode> friends = graph.GetFriendsOfFriends(user1, 1);

            int result = friends.Count;
            int expected = 0;

            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void GetFriendsOfFriends_GivenADepthOf1_ReturnsImmediateFriends()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "B");
            UserNode user2b = new UserNode("A", "C");
            UserNode user3 = new UserNode("A", "D");

            user1.AddFriend(user2);
            user1.AddFriend(user2b);
            user2.AddFriend(user3);

            graph.Add(user1);
            graph.Add(user2);
            graph.Add(user2b);
            graph.Add(user3);

            List<UserNode> friends = graph.GetFriendsOfFriends(user1, 1);

            Assert.IsTrue(friends.Contains(user2) && friends.Contains(user2b) && !friends.Contains(user3));
        }

        [TestMethod()]
        public void GetFriendsOfFriends_GivenADepthOf1_ContainsFriendsOnlyOnce()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "B");
            UserNode user2b = new UserNode("A", "C");

            user1.AddFriend(user2);
            user1.AddFriend(user2b);
            user2.AddFriend(user2b);

            graph.Add(user1);
            graph.Add(user2);
            graph.Add(user2b);

            List<UserNode> friends = graph.GetFriendsOfFriends(user1, 1);

            int result = friends.Count;
            int expected = 2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetFriendsOfFriends_GivenADepthOf2_ReturnsFriendsOfFriends()
        {
            Graph graph = new Graph();

            UserNode user1 = new UserNode("A", "A");
            UserNode user2 = new UserNode("A", "B");
            UserNode user2b = new UserNode("A", "C");
            UserNode user3 = new UserNode("A", "D");

            user1.AddFriend(user2);
            user1.AddFriend(user2b);
            user2.AddFriend(user3);

            graph.Add(user1);
            graph.Add(user2);
            graph.Add(user2b);
            graph.Add(user3);

            List<UserNode> friends = graph.GetFriendsOfFriends(user1, 2);

            Assert.IsTrue(friends.Contains(user2) && friends.Contains(user2b) && friends.Contains(user3));
        }
    }
}