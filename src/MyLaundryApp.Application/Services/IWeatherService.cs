using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLaundryApp.Application.Services
{
    public interface IWeatherService
    {
        Task<bool> GetWeatherAsync(CancellationToken cancellationToken = default);
    }
}
