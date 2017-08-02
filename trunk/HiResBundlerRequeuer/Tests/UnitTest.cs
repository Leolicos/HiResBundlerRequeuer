using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                var list = queue.GetAssetsInQueue(HiResBundlerRequeuer.ConnectionConfig.CONN_PROD);

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
                var list = queue.GetAssetsInQueue(HiResBundlerRequeuer.ConnectionConfig.CONN_PROD);

                if ( list.Count > 0 )
                    queue.RequeueAsset(list[0], HiResBundlerRequeuer.ConnectionConfig.CONN_PROD);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed with exception: " + ex.Message);
            }
        }
    }
}
