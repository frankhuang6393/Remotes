using NSubstitute;
using NUnit.Framework;
using Remotes;
using Remotes.Models;
using Remotes.ViewModel;

namespace RemotesTest
{
    public class ToolsTests
    {
        private APICheckParamterType checkAPIUserNameType;
        private APICheckParamterType checkAPIOrderIDType;

        [SetUp]
        public void Setup()
        {
            checkAPIUserNameType = APICheckParamterType.UserName;
            checkAPIOrderIDType = APICheckParamterType.OrderID;
        }

        #region CheckAPIParamterWithMessage

        [Test]
        public void CheckAPIParamterWithMessage_UserName_ReturnTrue()
        {
            //arrange
            var parm = new object();
            var exceptResp = new APIResponseBaseViewModel<object>();
            
            //act
            var actualResp = new APIResponseBaseViewModel<object>();
            var actual = Tools.CheckAPIParamterWithMessage(checkAPIUserNameType, actualResp, parm);

            //assert
            Assert.IsTrue(actual);
            foreach (var pi in exceptResp.GetType().GetProperties())
            {
                var exceptVal = pi.GetValue(exceptResp);
                var actualVal = actualResp.GetType().GetProperty(pi.Name).GetValue(actualResp);
                Assert.AreEqual(exceptVal, actualVal);
            }
        }

        [Test]
        public void CheckAPIParamterWithMessage_UserName_ReturnFalse()
        {
            //arrange
            object parm = null;
            var exceptResp = new APIResponseBaseViewModel<object>() { Success = false, APIReturnCode = APIReturnCode.UserIsNotExist, Message = checkAPIUserNameType.Description() };

            //act
            var actualResp = new APIResponseBaseViewModel<object>();
            var actual = Tools.CheckAPIParamterWithMessage(checkAPIUserNameType, actualResp, parm);
            
            //assert
            Assert.IsFalse(actual);
            foreach (var pi in exceptResp.GetType().GetProperties())
            {
                var exceptVal = pi.GetValue(exceptResp);
                var actualVal = actualResp.GetType().GetProperty(pi.Name).GetValue(actualResp);
                Assert.AreEqual(exceptVal, actualVal);
            }
        }

        [Test]
        public void CheckAPIParamterWithMessage_OrderID_ReturnTrue()
        {
            //arrange
            var parm = new object();
            var exceptResp = new APIResponseBaseViewModel<object>();

            //act
            var actualResp = new APIResponseBaseViewModel<object>();
            var actual = Tools.CheckAPIParamterWithMessage(checkAPIOrderIDType, actualResp, parm);

            //assert
            Assert.IsTrue(actual);
            foreach (var pi in exceptResp.GetType().GetProperties())
            {
                var exceptVal = pi.GetValue(exceptResp);
                var actualVal = actualResp.GetType().GetProperty(pi.Name).GetValue(actualResp);
                Assert.AreEqual(exceptVal, actualVal);
            }
        }

        [Test]
        public void CheckAPIParamterWithMessage_OrderID_ReturnFalse()
        {
            //arrange
            object parm = null;
            var exceptResp = new APIResponseBaseViewModel<object>() { Success = false, APIReturnCode = APIReturnCode.OrderIsNotExist, Message = checkAPIOrderIDType.Description() };

            //act
            var actualResp = new APIResponseBaseViewModel<object>();
            var actual = Tools.CheckAPIParamterWithMessage(checkAPIOrderIDType, actualResp, parm);

            //assert
            Assert.IsFalse(actual);
            foreach (var pi in exceptResp.GetType().GetProperties())
            {
                var exceptVal = pi.GetValue(exceptResp);
                var actualVal = actualResp.GetType().GetProperty(pi.Name).GetValue(actualResp);
                Assert.AreEqual(exceptVal, actualVal);
            }
        }
        
        #endregion

        #region GetNewSN

        [Test]
        public void GetNewSN_Length_ReturnTrue()
        {
            //arrange
            var except = 32;
            
            //act
            var actual = Tools.GetNewSN().Length;

            //assert
            Assert.AreEqual(except, actual);
        }

        [Test]
        public void GetNewSN_Length_ReturnFalse()
        {
            //arrange
            var except = 36;

            //act
            var actual = Tools.GetNewSN().Length;

            //assert
            Assert.AreNotEqual(except, actual);
        }
        
        #endregion
    }
}