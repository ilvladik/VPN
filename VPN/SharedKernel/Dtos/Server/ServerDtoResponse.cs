using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dtos.Server
{
    public class ServerDtoResponse
    {
        public ServerDtoResponse(Guid id, string networkId, int apiPort, string apiPrefix)
        {
            Id = id;
            NetworkId = networkId;
            ApiPort = apiPort;
            ApiPrefix = apiPrefix;
        }

        public Guid Id { get; set; }
        public string NetworkId { get; } = string.Empty;
        public int ApiPort { get; }
        public string ApiPrefix { get; } = string.Empty;
    }
}
