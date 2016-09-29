using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System.Threading;
using System.Fabric.Description;
using System.Net;
using System.Fabric;
using Microsoft.ServiceFabric.Data.Collections;

namespace Mall.Common.Client
{
    //public class StateFulService: ICounter
    //{
        
    //    public async Task<long> GetCountAsync()
    //    {
    //        var myDictionary =
    //          await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

    //        using (var tx = this.StateManager.CreateTransaction())
    //        {
    //            var result = await myDictionary.TryGetValueAsync(tx, "Counter");
    //            return result.HasValue ? result.Value : 0;
    //        }
    //    }
    //}
}
