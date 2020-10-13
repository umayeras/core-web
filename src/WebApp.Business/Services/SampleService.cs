using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApp.Business.Abstract.Services;
using WebApp.Core.Constants;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;
using WebApp.Model.Entities;
using WebApp.Model.Results;

namespace WebApp.Business.Services
{
    public class SampleService : ISampleService
    {
        #region ctor

        private readonly IBaseRepository<Sample> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<SampleService> logger;

        public SampleService(IUnitOfWork unitOfWork, ILogger<SampleService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            repository = unitOfWork.GetRepository<Sample>();
        }

        #endregion

        public async Task<IEnumerable<Sample>> GetAll()
        {
            return await repository.GetAllAsync();
        }

        public ServiceResult Add(Sample sample)
        {
            try
            {
                repository.Add(sample);
                unitOfWork.Save();

                return ServiceResult.Success(Messages.GeneralSuccess);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while adding sample, Error: {ex.Message}");
                return ServiceResult.Error(Messages.GeneralError);
            }
        }
    }
}