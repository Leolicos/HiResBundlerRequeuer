using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiResBundlerRequeuer
{
    public static class ConnectionConfig
    {
        public const string CONN_PROD   = @"Data Source = qzl8e681ue.database.windows.net; Initial Catalog = Platform_50; Persist Security Info=True;User Id = bundler; Password=xJ2B35&7m0WW&#HB;MultipleActiveResultSets=True";
        public const string CONN_QA     = @"svtjbqh4b6.database.windows.net";
        public const string CONN_DEV    = @"icssql02\wheaties";
    }
}
