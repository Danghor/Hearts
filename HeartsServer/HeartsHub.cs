using System;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeartsServer
{
    public class HeartsHub : Hub
    {
        readonly ICollection<Guid> clientIds = new List<Guid>();

        public async Task RequestId()
        {
            if (clientIds.Count < 2)
            {
                Guid id = Guid.NewGuid();
                clientIds.Add(id);
                Console.WriteLine($"Assigning Id {id} to client.");

                await Clients.Caller.SendAsync("AssignId", id).ConfigureAwait(false);
            }
            else
            {
                await Clients.Caller.SendAsync("AssignId", Guid.Empty);
            }
        }
    }
}
