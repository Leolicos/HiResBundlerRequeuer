using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiResBundlerRequeuer
{
    public class BundlerQueueHelper
    {
        public Dictionary<int, AssetQueueInfo> GetAssetsInQueue(string connectionURL)
        {
            Dictionary<int, AssetQueueInfo> assetMap = new Dictionary<int, AssetQueueInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionURL))
                {
                    connection.Open();
                    string strCommand = string.Format("{0} {1} {2}",
                        "SELECT *",
                        "FROM HiResBundlerQueue q",
                        "inner join HiResBundlerLog l on q.HiResBundlerQueueId = l.HiResBundlerQueueId and (q.HiResBundlerStatusId = 2 or q.HiResBundlerStatusId = 3 or q.HiResBundlerStatusId = 5)");

                    Console.Write("run command " + strCommand);
                    using (SqlCommand command = new SqlCommand(strCommand, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int assetId = int.Parse(reader["ProductAssetId"].ToString());
                            if ( ! assetMap.ContainsKey(assetId) )
                            {
                                AssetQueueInfo info = new AssetQueueInfo()
                                {
                                    HiResBundlerQueueId = int.Parse(reader["HiResBundlerQueueId"].ToString()),
                                    ProductAssetId = assetId,
                                    HiResBundlerStatusId = int.Parse(reader["HiResBundlerStatusId"].ToString()),
                                    HiResBundlerErrorCodeId = int.Parse(reader["HiResBundlerErrorCodeId"].ToString()),
                                    InsertDate = reader["InsertDate"].ToString(),
                                    UpdateDate = reader["UpdateDate"].ToString(),
                                    UpdateUserId = int.Parse(reader["UpdateUserId"].ToString()),
                                    Priority = int.Parse(reader["Priority"].ToString()),
                                    QueueName = reader["QueueName"].ToString(),
                                    LogSubject = reader["LogSubject"].ToString(),
                                    LogMessage = reader["LogMessage"].ToString()
                                };

                                Console.WriteLine("Adding assetid " + info.ProductAssetId);
                                assetMap.Add(assetId, info);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("GetAssetsInQueue Exception: {0}", ex.Message));
            }

            return assetMap;
        }
        public void RequeueAsset(AssetQueueInfo assetInfo, string connectionURL)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionURL))
                {
                    connection.Open();
                    string strCommand = string.Format("{0} {1}",
                        "UPDATE HiResBundlerQueue",
                        string.Format("SET HiResBundlerStatusId = 1 where HiResBundlerQueueId = {0}", assetInfo.HiResBundlerQueueId));

                    using (SqlCommand command = new SqlCommand(strCommand, connection))
                    {
                        Console.Write("running command\n" + strCommand);
                        //command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("RequeueAsset Exception: {0}", ex.Message));
            }
        }
    }
}
