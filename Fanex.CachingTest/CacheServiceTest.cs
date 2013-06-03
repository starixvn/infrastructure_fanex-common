using Fanex.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Fanex.CachingTest
{
    
    
    /// <summary>
    ///This is a test class for CacheServiceTest and is intended
    ///to contain all CacheServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CacheServiceTest
    {


        private TestContext testContextInstance;

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
        ///A test for CreateInstance
        ///</summary>
        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_MemoryCache_Instance_As_Default()
        {            
            ICache cache = CacheService.CreateInstance();
            Assert.IsTrue(cache.GetType().Name.Equals("MemoryCache"));             
        }

        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_MemoryCache_Instance_If_TypeName_Is_Null()
        {
            string typeName = null;
            ICache cache = CacheService.CreateInstance(typeName);
            Assert.IsTrue(cache.GetType().Name.Equals("MemoryCache"));

            //typeName = string.Empty;
            //cache = CacheService.CreateInstance(typeName);
            //Assert.IsTrue(cache.GetType().Name.Equals("MemoryCache"));
        }

        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_MemoryCache_Instance_If_TypeName_Is_Empty()
        {
            string typeName = string.Empty;
            ICache cache = CacheService.CreateInstance(typeName);
            Assert.IsTrue(cache.GetType().Name.Equals("MemoryCache"));
        }

        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_MemoryCache_Instance_If_TypeName_Is_Invalid()
        {
            string typeName = "heheheh";
            ICache cache = CacheService.CreateInstance(typeName);
            Assert.IsTrue(cache.GetType().Name.Equals("MemoryCache"));
        }

        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_EntlibCache_Instance_When_CacheType_Is_EntlibCache()
        {
            CreateCacheInstanceByType(CacheType.EntlibCache);
        }

        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_MemCache_Instance_When_CacheType_Is_MemCache()
        {
            CreateCacheInstanceByType(CacheType.MemCache);
        }

        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_SharedCache_Instance_When_CacheType_Is_SharedCache()
        {            
            CreateCacheInstanceByType(CacheType.SharedCache);
        }

      

        [TestMethod()]
        [TestCategory("CacheService")]
        public void CacheService_Return_WeakRefCache_Instance_When_CacheType_Is_WeakRefCache()
        {
            CreateCacheInstanceByType(CacheType.WeakRefCache);
        }

        private static void CreateCacheInstanceByType(CacheType cacheType)
        {
            ICache cache = CacheService.CreateInstance(cacheType);
            Assert.IsTrue(cache.GetType().Name == cacheType.ToString());
        }
    }
}
