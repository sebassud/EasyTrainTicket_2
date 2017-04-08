using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTrainTickets.Server.Controllers;
using EasyTrainTickets.Domain.Data;
using System.Collections.Generic;
using EasyTrainTickets.Common.DTOs;
using AutoMapper;
using EasyTrainTickets.Domain.Model;
using System.Web.Http.Results;
using EasyTrainTickets.Server.Models;
using EasyTrainTickets.Domain.Services;
using System.Linq;

namespace EasyTrainTickets.Tests
{
    [TestClass]
    public class ServerTests
    {
        private IUnitOfWorkFactory unitOfFactory;
        public IEasyTrainTicketsDbEntities Data;

        [TestInitialize]
        public void Initialize()
        {
            this.unitOfFactory = new AutoRollbackUnitOfWorkFactory();
            AutoMapperConfig.RegisterMappings();
            Data = unitOfFactory.CreateUnitOfWork();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.Data.Dispose();
        }

        [TestMethod]
        public void GetRoutesTest()
        {
            // Arrange
            RoutesController routeController = new RoutesController(unitOfFactory);

            // Act
            IEnumerable<RouteDTO> result1 = routeController.GetRoutes();
            List<RouteDTO> list = (List<RouteDTO>)result1;
            var result2 = routeController.GetRoute(5);
            var result3 = routeController.GetRoute(-2);
            var contentresult = result2.Result as OkNegotiatedContentResult<RouteDTO>;

            // Assert
            Assert.IsTrue(list.Count > 0);
            Assert.AreEqual(5, contentresult.Content.Id);
            Assert.IsInstanceOfType(result3.Result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void GetConnectionsTest()
        {
            // Arrange
            ConnectionsController connectionsController = new ConnectionsController(unitOfFactory);

            // Act
            var result2 = connectionsController.GetConnection(1);
            var contentresult = result2.Result as OkNegotiatedContentResult<ConnectionDTO>;
            var result3 = connectionsController.GetConnection(-5);

            // Assert
            Assert.IsNotNull(contentresult.Content.Parts);
            Assert.IsNotNull(contentresult.Content.Train);
            Assert.AreEqual(1, contentresult.Content.Id);
            Assert.IsInstanceOfType(result3.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateTicketsTest()
        {
            // Arrange
            User user = new User()
            {
                Id = 88,
                Login = "test",
                Password = "test",
                Tickets = "1,2,3:1-14,15,16;1,1,3,3:1-24,25,26,27"
            };
            Data.Users.Add(user);
            TicketsModel ticketsModel = new TicketsModel();
            Data.SaveChanges();

            // Act
            List<TicketDTO> tickets = ticketsModel.GetTicketsUser(user.Login, Data);

            // Assert
            Assert.AreEqual(2, tickets.Count);
            Assert.AreEqual(3, tickets[0].Seats[0].Length);
            Assert.AreEqual(3, tickets[0].Discounts.Count);
        }

        [TestMethod]
        public void BuyTicketTest()
        {
            // Arrange
            var data = unitOfFactory.CreateTransactionalUnitOfWork();
            User user = new User()
            {
                Login = "test",
                Password = "test",
                Tickets = "1,2,3:1-14,15,16;1,1,3,3:1-24,25,26,27"
            };
            data.Users.Add(user);
            data.SaveChanges();
            DiscountsController discountController = new DiscountsController(unitOfFactory);
            var discountResult = discountController.GetDiscount(1);
            var discount = (discountResult.Result as OkNegotiatedContentResult<DiscountDTO>).Content;
            Dictionary<DiscountDTO, int> dict = new Dictionary<DiscountDTO, int>();
            dict.Add(discount, 2);

            ConnectionPathDTO conPath = new ConnectionPathDTO();
            Connection con = data.Connections.Find(1);
            ConnectionDTO conDTO = Mapper.Map<Connection, ConnectionDTO>(con);
            ConnectionPart conPart = new ConnectionPart
            {
                Connection = con,
                Id = 9999999,
                Seats = "1;2;3",
                FreeSeats = 3,
                Route = data.Routes.Find(1),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30)
            };
            data.ConnectionParts.Add(conPart);
            data.SaveChanges();
            conPath.ConnectionsParts = new List<ConnectionPartDTO> { Mapper.Map<ConnectionPart, ConnectionPartDTO>(conPart) };
            UserDTO userDTO = Mapper.Map<User, UserDTO>(data.Users.Where(u => u.Login == user.Login).First());
            TicketDTO ticketDTO = new TicketDTO()
            {
                Discounts = dict,
                User = userDTO,
                ConnectionPath = conPath,
                Seats = new List<int[]> { new int[] { 1, 3 } }
            };
            TicketsModel ticketsModel = new TicketsModel();
            data.SaveChanges();

            // Act
            ticketsModel.BuyTicket(ticketDTO, data);

            // Assert
            Assert.AreEqual("2", conPart.Seats);
            Assert.AreEqual(string.Format("1,2,3:1-14,15,16;1,1,3,3:1-24,25,26,27;1,1:{0}-1,3", conPart.Id), user.Tickets);
        }
        [TestMethod]
        public void DeleteTicketTest()
        {
            // Arrange
            var data = unitOfFactory.CreateTransactionalUnitOfWork();
            User user = new User()
            {
                Login = "test",
                Password = "test",
                Tickets = "1,2,3:1-14,15,16;1,1,3,3:1-24,25,26,27"
            };
            data.Users.Add(user);
            data.SaveChanges();
            DiscountsController discountController = new DiscountsController(unitOfFactory);
            var discountResult = discountController.GetDiscount(1);
            var discount = (discountResult.Result as OkNegotiatedContentResult<DiscountDTO>).Content;
            Dictionary<DiscountDTO, int> dict = new Dictionary<DiscountDTO, int>();
            dict.Add(discount, 2);

            ConnectionPathDTO conPath = new ConnectionPathDTO();
            Connection con = data.Connections.Find(1);
            ConnectionDTO conDTO = Mapper.Map<Connection, ConnectionDTO>(con);
            ConnectionPart conPart = new ConnectionPart
            {
                Connection = con,
                Id = 9999999,
                Seats = "1;2;3",
                FreeSeats = 3,
                Route = data.Routes.Find(1),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30)
            };
            data.ConnectionParts.Add(conPart);
            data.SaveChanges();
            conPath.ConnectionsParts = new List<ConnectionPartDTO> { Mapper.Map<ConnectionPart, ConnectionPartDTO>(conPart) };
            UserDTO userDTO = Mapper.Map<User, UserDTO>(data.Users.Where(u => u.Login == user.Login).First());
            TicketDTO ticketDTO = new TicketDTO()
            {
                Discounts = dict,
                User = userDTO,
                ConnectionPath = conPath,
                Seats = new List<int[]> { new int[] { 1, 3 } }
            };
            TicketsModel ticketsModel = new TicketsModel();
            data.SaveChanges();
            ticketsModel.BuyTicket(ticketDTO, data);

            //Act
            ticketsModel.DeleteTicket(ticketDTO, data);

            // Assert
            Assert.AreEqual("2;1;3", conPart.Seats);
            Assert.AreEqual("1,2,3:1-14,15,16;1,1,3,3:1-24,25,26,27", user.Tickets);
        }

        [TestMethod]
        public void SearchTest()
        {
            // Arrange
            Path path = new Path() { Track = new List<Route>() { Data.Routes.First() } };
            ConnectionPart conPart = new ConnectionPart()
            {
                Route = Data.Routes.First(),
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2)
            };
            conPart.Connection = new Connection()
            {
                Parts = new List<ConnectionPart>() { conPart }
            };
            Data.ConnectionParts.Add(conPart);
            Data.SaveChanges();

            // Act
            List<ConnectionPath> conPath = path.SecondSearch(DateTime.Now, Data);

            // Assert
            Assert.IsTrue(conPath.Where(cp => cp.ConnectionsParts.Contains(conPart)).Count() == 1);
        }

        [TestMethod]
        public void LoginTest()
        {
            // Arrange
            User user = new User()
            {
                Login = "test",
                Password = "test",
                Tickets = ""
            };
            Data.Users.Add(user);
            UserModel userModel = new UserModel();
            Data.SaveChanges();

            // Act
            UserDTO userDTO = userModel.SignIn(Mapper.Map<User, UserDTO>(user), Data);

            // Assert
            Assert.IsNotNull(userDTO);
        }
        [TestMethod]
        public void LoginNotExistTest()
        {
            // Arrange
            UserModel userModel = new UserModel();
            UserDTO userDTO = new UserDTO()
            {
                Login = "",
                Password = "test",
                Tickets = ""
            };

            // Act
            userDTO = userModel.SignIn(userDTO, Data);

            //Assert
            Assert.IsNull(userDTO);
        }

        [TestMethod]
        public void RegistrationTest()
        {
            // Arrange
            UserDTO userDTO = new UserDTO()
            {
                Login = "test",
                Password = "test",
                Tickets = ""
            };
            UserModel userModel = new UserModel();

            // Act
            userDTO = userModel.Registration(userDTO, Data);

            // Assert
            Assert.IsNotNull(userDTO);
        }

        [TestMethod]
        public void RegistrationExistTest()
        {
            // Arrange
            User user = new User()
            {
                Login = "test",
                Password = "test",
                Tickets = ""
            };
            Data.Users.Add(user);
            UserModel userModel = new UserModel();
            Data.SaveChanges();
            UserDTO userDTO = new UserDTO()
            {
                Login = "test",
                Password = "dupa",
                Tickets = ""
            };

            //Act
            userDTO = userModel.Registration(userDTO, Data);

            //Assert
            Assert.IsNull(userDTO);
        }
        [TestMethod]
        public void ChangePasswordTest()
        {
            // Arrange
            User user = new User()
            {
                Login = "test",
                Password = "test",
                Tickets = ""
            };
            Data.Users.Add(user);
            UserModel userModel = new UserModel();
            Data.SaveChanges();
            ChangePasswordDTO changepassDTO = Mapper.Map<User, ChangePasswordDTO>(user);
            changepassDTO.NewPassword = "dupa";

            //Act
            UserDTO userDTO = userModel.ChangePassword(changepassDTO, Data);

            //Assert
            Assert.AreEqual("dupa", user.Password);
        }
        public class AutoRollbackUnitOfWorkFactory : UnitOfWorkFactory
        {
            public override IEasyTrainTicketsDbEntities CreateUnitOfWork()
            {

                return CreateTransactionalUnitOfWork();
            }

            public override ITransactionalData CreateTransactionalUnitOfWork()
            {
                var uow = new EasyTrainTicketsDbEntities(true);
                uow.Reject();
                return uow;
            }
        }
    }
}
