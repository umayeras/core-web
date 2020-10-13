using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Model.Entities;
using WebApp.Model.Results;

namespace WebApp.Business.Abstract.Services
{
    public interface ISampleService
    {
        Task<IEnumerable<Sample>> GetAll();
        ServiceResult Add(Sample sample);
    }
}