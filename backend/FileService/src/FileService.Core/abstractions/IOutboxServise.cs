using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.abstractions
{
    public interface IOutboxServise
    {
        Task PublishAsync<T>(T message) where T : class;

        Task FlushAsync();
    }
}
