using Fanex.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Enyim.Caching.Configuration;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using System.Threading;
using System.Net;
namespace Fanex.CachingTest
{


    /// <summary>
    ///This is a test class for MemCacheTest and is intended
    ///to contain all MemCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MemCacheTest
    {


        private TestContext testContextInstance;
        //private MemcachedClient memCacheClient;
        private ICache target;

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
        public void MemCacheClient()
        {
            target = CacheService.CreateInstance(CacheType.MemCache);
        }
        //[TestMethod()]
        //public void Test()
        //{
        //    memCacheClient.Store(StoreMode.Set, "A", 1);
        //}

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
        public void ClearTest()
        {
            target.Clear();
            Assert.IsTrue(target.Count == 0);

            for (int i = 0; i < 10; i++)
            {
                target.Set<int>(i.ToString(), i);
            }
            target.Clear();
            Assert.IsTrue(target.Count == 0);
        }     

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        public void ContainsTest()
        {
            for (int i = 0; i < 10; i++)
            {
                target.Set<int>(i.ToString(), i);
                int actual = target.Get<int>(i.ToString());
                Assert.IsTrue(actual == i);
            }
            target.Clear();

            target.Contains(string.Empty);
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest()
        {
            target.Dispose();
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTestHelper()
        {
            //for (int i = 1; i < 10; i++)
            //{
            //    target.Set<int>(i.ToString(), i);
            //    int actual = target.Get<int>(i.ToString());
            //    Assert.IsTrue(actual == i);
            //}
            //target.Clear();

            //target.Get<int>(string.Empty);

            target.Set<int>("keySet", 12345);

            int actual = target.Get<int>("keySet");

            Assert.Inconclusive(actual.ToString());
        }


        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            for (int i = 0; i < 10; i++)
            {
                target.Set<int>(i.ToString(), i);
                target.Remove(i.ToString());
            }
            target.Clear();

            target.Remove(string.Empty);
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        /// 
        [TestMethod()]
        public void SetTest_NotExpiration()
        {
            target.Set<int>("keySet", 10);
            target.Set<int>("keySet", 100);
            int actual = target.Get<int>("keySet");
            Assert.IsTrue(actual == 100);
        }




        /// <summary>
        ///A test for Set
        ///</summary>
        ///
        [TestMethod()]
        public void SetTestHelper_AbsoluteExpiration()
        {

        }




        /// <summary>
        ///A test for Set
        ///</summary>
        [TestMethod()]
        public void SetTest_SlidingExpiration()
        {

        }



        /// <summary>
        ///A test for TryGet
        ///</summary>
        [TestMethod()]
        public void TryGetTestHelper()
        {

        }


        /// <summary>
        ///A test for Count
        ///</summary>
        [TestMethod()]
        public void CountTest()
        {
            for (int i = 0; i < 10; i++)
            {
                target.Set<int>(i.ToString(), i);
            }
            int actual = target.Count;
            Assert.IsTrue(actual == 10);
        }
    }
}
