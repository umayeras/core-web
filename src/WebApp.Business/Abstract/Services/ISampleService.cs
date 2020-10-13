using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Model.Entities;

namespace WebApp.Business.Abstract.Services
{
    public interface ISampleService
    {
        Task<IEnumerable<Sample>> GetAll();
    }
}