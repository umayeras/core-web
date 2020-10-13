using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Business.Abstract.Services;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;
using WebApp.Model.Entities;

namespace WebApp.Business.Services
{
    public class SampleService :  ISampleService
    {
        #region ctor

        private readonly IUnitOfWork unitOfWork;
        private readonly IBaseRepository<Sample> repository;
        
        public SampleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.GetRepository<Sample>();
        }

        #endregion

        public async Task<IEnumerable<Sample>> GetAll()
        {
            return await repository.GetAllAsync();
        }
    }
}