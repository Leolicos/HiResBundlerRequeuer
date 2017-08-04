using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiResBundlerRequeuer;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void GetAssetInfoTest()
        {
            try
            {
                HiResBundlerRequeuer.BundlerQueueHelper queue = new HiResBundlerRequeuer.BundlerQueueHelper();
                var list = queue.GetAssetsInQueue(ConnectionConfig.GetConnectionString());

                Assert.AreNotEqual(list, null);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed with exception: " + ex.Message);
            }
        }

        [TestMethod]
        public void RequeueAsset()
        {
            try
            {
                HiResBundlerRequeuer.BundlerQueueHelper queue = new HiResBundlerRequeuer.BundlerQueueHelper();
                var list = queue.GetAssetsInQueue(ConnectionConfig.GetConnectionString());

                if ( list.Count > 0 )
                    queue.RequeueAsset(list[0], ConnectionConfig.GetConnectionString());
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed with exception: " + ex.Message);
            }
        }
    }
}
