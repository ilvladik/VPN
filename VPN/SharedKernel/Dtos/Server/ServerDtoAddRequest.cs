using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dtos.Server
{
    public class ServerDtoAddRequest
    {
        public ServerDtoAddRequest(string networkId, int apiPort, string apiPrefix) 
        {
            NetworkId = networkId;
            ApiPort = apiPort;
            ApiPrefix = apiPrefix;
        }
        public string NetworkId { get; } = string.Empty;
        public int ApiPort { get; }
        public string ApiPrefix { get; } = string.Empty;
    }
}
