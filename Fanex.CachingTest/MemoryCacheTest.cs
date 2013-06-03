using Fanex.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
namespace Fanex.CachingTest
{


    /// <summary>
    ///This is a test class for MemoryCacheTest and is intended
    ///to contain all MemoryCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MemoryCacheTest
    {


        private TestContext testContextInstance;
        private ICache _target;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            _target = CacheService.CreateInstance(cacheType: CacheType.MemoryCache);
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Clear
        ///</summary>
        [TestMethod()]
        public void ClearTest_Throw_Argument_Null_When_Cache_Null()
        {

            for (int i = 0; i < 10; i++)
            {
                _target.Set<int>("keyClear" + i.ToString(), i);
            }
            _target.Clear();
            Assert.IsTrue(_target.Count == 0);

        }     

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        public void ContainsTest_Throw_Argument_When_Key_Null()
        {
            bool actual;
            _target.Set<int>("keyContains", 1);
            actual = _target.Contains("keyContains");
            Assert.AreEqual(true, actual);


            actual = _target.Contains("keyNotContains");
            Assert.AreEqual(false, actual);

            actual = _target.Contains(string.Empty);
            Assert.Inconclusive(actual.ToString());

        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest()
        {
            for (int i = 0; i < 9; i++)
            {
                _target.Set<int>("keyClear" + i.ToString(), i);
            }
            _target.Dispose();

        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTestHelper_Throw_Argument_When_Key_Null()
        {
            _target.Set<int>("keyGet", 10);
            int actual = _target.Get<int>("keyGet");

            Assert.IsTrue(actual == 10);

            actual = _target.Get<int>(string.Empty);
            Assert.Inconclusive(actual.ToString());
        }



        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest_Throw_Argument_When_Key_Null()
        {
            _target.Set<int>("keyGet", 10);
            int actual = _target.Get<int>("keyGet");

            _target.Remove("keyGet");
            Assert.IsTrue(_target.Contains("keyGet") == false);

            _target.Remove(string.Empty);
            Assert.Inconclusive(actual.ToString());


            _target.Remove("keyNotExist");


        }

        /// <summary>
        ///A test for Set
        ///</summary>
        [TestMethod()]
        public void SetTestNotExpiration_Throw_Argument_When_Key_Null_or_Values_Null()
        {
            _target.Set<int>("keySet", 100);
            _target.Set<int>("keySet", 10);
            int actual = _target.Get<int>("keySet");
            Assert.IsTrue(actual == 10);
        }


        /// <summary>
        ///A test for Set
        ///</summary>      

        [TestMethod()]
        public void SetTestAbsoluteExpiration_Throw_Argument_When_Key_Null_or_Values_Null()
        {
            _target.Set<int>("keySet", 100, DateTime.Now.AddSeconds(1));
            int actual = _target.Get<int>("keySet");
            Assert.IsTrue(actual == 100);

            _target.Set<int>(string.Empty, 100, DateTime.Now.AddSeconds(1));
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        ///
        [TestMethod()]
        public void SetTestSlidingExpiration_Throw_Argument_When_Key_Null_or_Values_Null()
        {
            _target.Set<int>("keySet", 100, new TimeSpan(10));
            int actual = _target.Get<int>("keySet");
            Assert.IsTrue(actual == 100);

            _target.Set<int>(string.Empty, 100, new TimeSpan(10));
        }


        /// <summary>
        ///A test for TryGet
        ///</summary>
        [TestMethod()]
        public void TryGetTestHelper_Throw_Argument_When_Key_Null()
        {
            List<string> listValue = new List<string>();
            List<string> list = new List<string>();
            listValue.Add("A");
            _target.Set<List<string>>("keyTryGet", listValue);
            _target.TryGet<List<string>>("keyTryGet", out list);
            Assert.AreEqual(listValue.Count, list.Count);


            list = new List<string>();
            _target.TryGet<List<string>>("keyNotExist", out list);
            Assert.IsTrue(list == null);


            _target.TryGet<List<string>>(string.Empty, out list);

        }

    }
}
