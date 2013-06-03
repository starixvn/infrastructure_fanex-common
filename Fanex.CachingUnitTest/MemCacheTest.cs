using Fanex.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Enyim.Caching;

namespace Fanex.CachingUnitTest
{
    
    
    /// <summary>
    ///This is a test class for MemCacheTest and is intended
    ///to contain all MemCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MemCacheTest
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
        ///A test for MemCache Constructor
        ///</summary>
        [TestMethod()]
        public void MemCacheConstructorTest()
        {
            MemCache target = new MemCache();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        [TestMethod()]
        public void ClearTest()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            target.Clear();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        public void ContainsTest()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Contains(key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            target.Dispose();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        public void GetTestHelper<T>()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            T expected = default(T); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.Get<T>(key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetTest()
        {
            GetTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            target.Remove(key);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        public void SetTestHelper<T>()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            T value = default(T); // TODO: Initialize to an appropriate value
            TimeSpan slidingExpiration = new TimeSpan(); // TODO: Initialize to an appropriate value
            target.Set<T>(key, value, slidingExpiration);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void SetTest()
        {
            SetTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        public void SetTest1Helper<T>()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            T value = default(T); // TODO: Initialize to an appropriate value
            int timeOut = 0; // TODO: Initialize to an appropriate value
            target.Set<T>(key, value, timeOut);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void SetTest1()
        {
            SetTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        public void SetTest2Helper<T>()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            T value = default(T); // TODO: Initialize to an appropriate value
            DateTime absoluteExpiration = new DateTime(); // TODO: Initialize to an appropriate value
            target.Set<T>(key, value, absoluteExpiration);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void SetTest2()
        {
            SetTest2Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for TryGet
        ///</summary>
        public void TryGetTestHelper<T>()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            T value = default(T); // TODO: Initialize to an appropriate value
            T valueExpected = default(T); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.TryGet<T>(key, out value);
            Assert.AreEqual(valueExpected, value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void TryGetTest()
        {
            TryGetTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for UseBucket
        ///</summary>
        [TestMethod()]
        public void UseBucketTest()
        {
            string bucketName = string.Empty; // TODO: Initialize to an appropriate value
            MemCache.UseBucket(bucketName);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        [TestMethod()]
        public void CountTest()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            int actual;
            actual = target.Count;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Keys
        ///</summary>
        [TestMethod()]
        public void KeysTest()
        {
            MemCache target = new MemCache(); // TODO: Initialize to an appropriate value
            HashSet<string> actual;
            actual = target.Keys;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MemCacheClient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fanex.Caching.dll")]
        public void MemCacheClientTest()
        {
            MemcachedClient actual;
            actual = MemCache_Accessor.MemCacheClient;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
