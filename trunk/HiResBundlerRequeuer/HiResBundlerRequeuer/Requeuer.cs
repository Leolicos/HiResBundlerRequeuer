using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiResBundlerRequeuer
{
    public class RequeuerLogic
    {
        public bool CheckIfAssetGotStuckInProgress(AssetQueueInfo assetInfo)
        {
            bool gotStuck = false;

            DateTime insertedTime = DateTime.Parse(assetInfo.UpdateDate);
            TimeSpan processingTime = DateTime.Now - insertedTime;

            if ( processingTime.TotalMinutes > 60 )
            {
                gotStuck = true;
            }

            return gotStuck;
        }
        public bool CheckIfFailedAssetShouldRetry(AssetQueueInfo assetInfo)
        {
            bool retry = false;

            if ( assetInfo.HiResBundlerErrorCodeId == 9 )
            {
                if (assetInfo.LogMessage.Contains("The network path was not found"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("The specified network name is no longer available"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("Unable to connect to the Perforce Server"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("Processing of asset Unknown failed with message") &&
                    assetInfo.LogMessage.Contains("Access to the path"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("The file") && assetInfo.LogMessage.Contains("already exists"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("Access to the path") && assetInfo.LogMessage.Contains("is denied"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("AccessViolation exception"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("The process cannot access the file because it is being used by another process"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("The directory is not empty"))
                    retry = true;
                else if (assetInfo.LogMessage.Contains("Unkown unity exception"))
                    retry = true;
            }

            return retry;
        }
    }
}
