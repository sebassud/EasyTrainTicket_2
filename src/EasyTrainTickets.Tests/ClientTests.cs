using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTrainTickets.DesktopClient.Work;
using System.Collections.Generic;
using System.Net.Http;
using EasyTrainTickets.DesktopClient.Models;
using System.Linq;

namespace EasyTrainTickets.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void GraphTest()
        {
            // Arrange
            List<Route> routes = new List<Route>();
            routes.Add(new Route() { From = "One", To = "Two", Distance = 250, BestTime = 200 });
            routes.Add(new Route() { From = "Two", To = "Three", Distance = 250, BestTime = 200 });
            routes.Add(new Route() { From = "Four", To = "Three", Distance = 250, BestTime = 200 });
            routes.Add(new Route() { From = "One", To = "Four", Distance = 300, BestTime = 250 });

            Graph graph = new Graph(new List<string>(){ "One","Two","Three","Four"}, routes);

            //Act
            List<Path> paths = graph.SearchPaths("One", "Three");

            //Assert
            Assert.AreEqual(2, paths.Count);
        }
    }
}
