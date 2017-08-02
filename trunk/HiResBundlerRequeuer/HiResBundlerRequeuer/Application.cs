using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiResBundlerRequeuer
{
    public class Application
    {
        BundlerQueueHelper _queue = new BundlerQueueHelper();
        RequeuerLogic _logic = new RequeuerLogic();

        public void Run()
        {
            DateTime last = DateTime.Now;
            while (true)
            {
                DateTime now = DateTime.Now;
                TimeSpan diff = now - last;

                if (diff.TotalSeconds > 5)
                {
                    _RunRequeueLogic();
                    last = now;
                }
            }
        }

        void _RunRequeueLogic()
        {
            Dictionary<int, AssetQueueInfo> assetMap = _queue.GetAssetsInQueue(ConnectionConfig.CONN_PROD);

            foreach (KeyValuePair<int, AssetQueueInfo> kvp in assetMap)
            {
                AssetQueueInfo info = kvp.Value;

                bool shouldRequeue = false;
                switch (info.HiResBundlerStatusId)
                {
                    case 5: // Retry
                        shouldRequeue = true;
                        break;

                    case 2: // Stuck in progress
                        shouldRequeue = _logic.CheckIfAssetGotStuckInProgress(info);
                        break;

                    case 3: // Failed but check if should retry
                        shouldRequeue = _logic.CheckIfFailedAssetShouldRetry(info);
                        break;
                }

                if (shouldRequeue)
                {
                    _queue.RequeueAsset(info, ConnectionConfig.CONN_PROD);
                }
            }
        }
    }
}
