using Fanex.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Fanex.CachingUnitTest
{
    
    
    /// <summary>
    ///This is a test class for MemoryCacheTest and is intended
    ///to contain all MemoryCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MemoryCacheTest
    {
        private ICache cache;
        [TestInitialize()]
        public void Initialize()
        {
            cache = CacheService.CreateInstance(CacheType.MemoryCache);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            cache.Clear();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Set_EmptyKey_ThrowException()
        {
            cache.Set(string.Empty, string.Empty);
        }

         [TestMethod()]
        public void Set_EmptyKey_Not_Increase_Number_Of_Keys()
        {
            cache.Set(string.Empty, string.Empty);
        }

        [TestMethod()]
        public void Count_Return_The_Number_Of_Current_Item_In_Cache_Instance()
        {
            cache.Clear();
            cache.Set<int>("1", 1);
            cache.Set<int>("2", 2);

            int expected = 2;
            int actual = cache.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Clear_Should_Empty_Key_Items()
        {
            cache.Clear();
            cache.Set<int>("1", 1);
            cache.Set<int>("2", 2);

            int expected = 0;
            cache.Clear();

            Assert.AreEqual(expected, cache.Count);
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        public void ContainsTest()
        {
            MemoryCache target = new MemoryCache(); // TODO: Initialize to an appropriate value
            string key = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Contains(key);
            Assert.AreEqual(expected, actual);
            
        }
    }
}
