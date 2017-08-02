using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiResBundlerRequeuer
{
    public class AssetQueueInfo
    {
        public int HiResBundlerQueueId;
        public int ProductAssetId;
        public int HiResBundlerStatusId;
        public int HiResBundlerErrorCodeId;
        public string InsertDate;
        public string UpdateDate;
        public int UpdateUserId;
        public int Priority;
        public string QueueName;
        public string LogSubject;
        public string LogMessage;
    }
}
