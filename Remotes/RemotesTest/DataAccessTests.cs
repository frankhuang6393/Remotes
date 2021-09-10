using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Remotes;
using Remotes.Models;
using Remotes.Services;
using Remotes.ViewModel;
using System.Collections.Generic;

namespace RemotesTest
{
    public class DataAccessTests
    {
        private IDaoService<APILogModel> fakeApiDaoService;
        private IDaoService<OrderModel> fakeOrderDaoService;
        private IDaoService<UserModel> fakeUserDaoService;

        [SetUp]
        public void Setup()
        {
            fakeApiDaoService = Substitute.For<IDaoService<APILogModel>>();
            fakeOrderDaoService = Substitute.For<IDaoService<OrderModel>>();
            fakeUserDaoService = Substitute.For<IDaoService<UserModel>>();
        }

        #region CreateAPIReqLog
        [Test]
        public void CreateAPIReqLog_ReturnTrue()
        {
            //arrange
            var expect = 0L;
            ILogService apiLogService = new APILogDAO(fakeApiDaoService);
            var tmpModel = new APILogModel();

            //act
            fakeApiDaoService.Excute(Arg.Any<string>(), Arg.Any<APILogModel>()).Returns(0L);
            var acutal = apiLogService.CreateAPIReqLog(tmpModel);
            fakeApiDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<APILogModel>());

            //assert
            Assert.AreEqual(expect, acutal);
        }

        [Test]
        public void CreateAPIReqLog_ReturnFalse()
        {
            //arrange
            var expect = 0;
            ILogService apiLogService = new APILogDAO(fakeApiDaoService);
            var tmpModel = new APILogModel();

            //act
            fakeApiDaoService.Excute(Arg.Any<string>(), Arg.Any<APILogModel>());
            var actual = apiLogService.CreateAPIReqLog(tmpModel);
            fakeApiDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<APILogModel>());

            //assert
            Assert.AreNotEqual(expect, actual);
        }
        #endregion

        #region CreateAPIRespLog
        [Test]
        public void CreateAPIRespLog_ReturnTrue()
        {
            //arrange
            ILogService apiLogService = new APILogDAO(fakeApiDaoService);
            var tmpModel = new APILogModel();

            //act
            fakeApiDaoService.Excute(Arg.Any<string>(), Arg.Any<APILogModel>());
            apiLogService.CreateAPIRespLog(tmpModel);
            
            //assert
            fakeApiDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<APILogModel>());
        }

        [Test]
        public void CreateAPIRespLog_ReturnFalse()
        {
            //arrange
            ILogService apiLogService = new APILogDAO(fakeApiDaoService);
            
            //act
            fakeApiDaoService.Excute(Arg.Any<string>(), Arg.Any<APILogModel>());
            apiLogService.CreateAPIRespLog(Arg.Any<APILogModel>());

            //assert
            fakeApiDaoService.Received(0).Excute(Arg.Any<string>(), Arg.Any<APILogModel>());
        }
        #endregion

        #region GetAllAPILog
        [Test]
        public void GetAllAPILog_ReturnTrue()
        {
            //arrange
            IEnumerable<APILogModel> expect = new List<APILogModel>();
            ILogService apiLogService = new APILogDAO(fakeApiDaoService);

            //act
            fakeApiDaoService.QueryItems(Arg.Any<string>()).ReturnsForAnyArgs(new List<APILogModel>());
            var actual = apiLogService.GetAllAPILog();
            fakeApiDaoService.Received(1).QueryItems(Arg.Any<string>());

            //assert
            Assert.AreEqual(expect, actual);
        }

        [Test]
        public void GetAllAPILog_ReturnFalse()
        {
            //arrange
            IEnumerable<APILogModel> expect = null;
            ILogService apiLogService = new APILogDAO(fakeApiDaoService);

            //act
            fakeApiDaoService.QueryItems(Arg.Any<string>()).ReturnsForAnyArgs(new List<APILogModel>());
            var actual = apiLogService.GetAllAPILog();
            fakeApiDaoService.Received(1).QueryItems(Arg.Any<string>());

            //assert
            Assert.AreNotEqual(expect, actual);
        }
        #endregion

        #region CreateOrder
        [Test]
        public void CreateOrder_ReturnTrue()
        {
            //arrange
            var expect = 0L;
            IOrderService orderService = new OrderDAO(fakeOrderDaoService);
            var tmpModel = new OrderModel();

            //act
            fakeOrderDaoService.Excute(Arg.Any<string>(), Arg.Any<OrderModel>()).Returns(0L);
            var acutal = orderService.CreateOrder(tmpModel);
            fakeOrderDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<OrderModel>());

            //assert
            Assert.AreEqual(expect, acutal);
        }

        [Test]
        public void CreateOrder_ReturnFalse()
        {
            //arrange
            var expect = 0L;
            IOrderService orderService = new OrderDAO(fakeOrderDaoService);
            var tmpModel = new OrderModel();

            //act
            fakeOrderDaoService.Excute(Arg.Any<string>(), Arg.Any<OrderModel>());
            var actual = orderService.CreateOrder(tmpModel);
            fakeOrderDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<OrderModel>());

            //assert
            Assert.AreNotEqual(expect, actual);
        }
        #endregion

        #region GetOrder
        [Test]
        public void GetOrder_ReturnTrue()
        {
            //arrange
            var expect = new OrderModel() { ID = 1 };
            IOrderService orderService = new OrderDAO(fakeOrderDaoService);
            var tmpOrderId = string.Empty;

            //act
            fakeOrderDaoService.Query(Arg.Any<string>(), Arg.Any<OrderModel>()).ReturnsForAnyArgs(new OrderModel() { ID = 1 });
            var actual = orderService.GetOrder(tmpOrderId);
            fakeOrderDaoService.Received(1).Query(Arg.Any<string>(), Arg.Any<OrderModel>());

            //assert
            Assert.AreEqual(expect.ID, actual.ID);
        }

        [Test]
        public void GetOrder_ReturnFalse()
        {
            //arrange
            var expect = new OrderModel() { ID = 1 };
            IOrderService orderService = new OrderDAO(fakeOrderDaoService);
            var tmpOrderId = string.Empty;

            //act
            fakeOrderDaoService.Query(Arg.Any<string>(), Arg.Any<OrderModel>()).ReturnsForAnyArgs(new OrderModel());
            var actual = orderService.GetOrder(tmpOrderId);
            fakeOrderDaoService.Received(1).Query(Arg.Any<string>(), Arg.Any<OrderModel>());

            //assert
            Assert.AreNotEqual(expect.ID, actual.ID);
        }
        #endregion

        #region UpdateOrderState
        [Test]
        public void UpdateOrderState_ReturnTrue()
        {
            //arrange
            IOrderService orderService = new OrderDAO(fakeOrderDaoService);
            var tmpModel = new OrderModel();

            //act
            fakeOrderDaoService.Excute(Arg.Any<string>(), Arg.Any<OrderModel>());
            orderService.UpdateOrderState(tmpModel);
            
            //assert
            fakeOrderDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<OrderModel>());
        }

        [Test]
        public void UpdateOrderState_ReturnFalse()
        {
            //arrange
            IOrderService orderService = new OrderDAO(fakeOrderDaoService);
            
            //act
            fakeOrderDaoService.Excute(Arg.Any<string>(), Arg.Any<OrderModel>());
            orderService.UpdateOrderState(Arg.Any<OrderModel>());

            //assert
            fakeOrderDaoService.Received(0).Excute(Arg.Any<string>(), Arg.Any<OrderModel>());
        }
        #endregion

        #region CreateUser
        [Test]
        public void CreateUser_ReturnTrue()
        {
            //arrange
            var expect = 0;
            IUserService userService = new UserDAO(fakeUserDaoService);
            var tmpModel = new UserModel();

            //act
            fakeUserDaoService.Excute(Arg.Any<string>(), Arg.Any<UserModel>()).ReturnsForAnyArgs(0L);
            var actual = userService.CreateUser(tmpModel);
            fakeUserDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<UserModel>());

            //assert
            Assert.AreEqual(expect, actual);
        }

        [Test]
        public void CreateUser_ReturnFalse()
        {
            //arrange
            var expect = 0;
            IUserService userService = new UserDAO(fakeUserDaoService);
            var tmpModel = new UserModel();

            //act
            fakeUserDaoService.Excute(Arg.Any<string>(), Arg.Any<UserModel>());
            var actual = userService.CreateUser(tmpModel);
            fakeUserDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<UserModel>());

            //assert
            Assert.AreNotEqual(expect, actual);
        }
        #endregion

        #region GetUser
        [Test]
        public void GetUser_ReturnTrue()
        {
            //arrange
            var expect = new UserModel() { ID = 1 };
            IUserService userService = new UserDAO(fakeUserDaoService);
            var tmpUserName = string.Empty;

            //act
            fakeUserDaoService.Query(Arg.Any<string>(), Arg.Any<UserModel>()).ReturnsForAnyArgs(new UserModel() { ID = 1 });
            var actual = userService.GetUser(tmpUserName);
            fakeUserDaoService.Received(1).Query(Arg.Any<string>(), Arg.Any<UserModel>());

            //assert
            Assert.AreEqual(expect.ID, actual.ID);
        }

        [Test]
        public void GetUser_ReturnFalse()
        {
            //arrange
            var expect = new UserModel() { ID = 1 };
            IUserService userService = new UserDAO(fakeUserDaoService);
            var tmpUserName = string.Empty;

            //act
            fakeUserDaoService.Query(Arg.Any<string>(), Arg.Any<UserModel>()).ReturnsForAnyArgs(new UserModel());
            var actual = userService.GetUser(tmpUserName);
            fakeUserDaoService.Received(1).Query(Arg.Any<string>(), Arg.Any<UserModel>());

            //assert
            Assert.AreNotEqual(expect.ID, actual.ID);
        }
        #endregion

        #region UpdateBalance
        [Test]
        public void UpdateBalance_ReturnTrue()
        {
            //arrange
            IUserService userService = new UserDAO(fakeUserDaoService);
            var tmpModel = new UserModel();

            //act
            fakeUserDaoService.Excute(Arg.Any<string>(), Arg.Any<UserModel>());
            userService.UpdateBalance(tmpModel);

            //assert
            fakeUserDaoService.Received(1).Excute(Arg.Any<string>(), Arg.Any<UserModel>());
        }

        [Test]
        public void UpdateBalance_ReturnFalse()
        {
            //arrange
            IUserService userService = new UserDAO(fakeUserDaoService);
            
            //act
            fakeUserDaoService.Excute(Arg.Any<string>(), Arg.Any<UserModel>());
            userService.UpdateBalance(Arg.Any<UserModel>());

            //assert
            fakeUserDaoService.Received(0).Excute(Arg.Any<string>(), Arg.Any<UserModel>());
        }
        #endregion
    }
}
