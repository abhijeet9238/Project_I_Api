using AutoMapper;
using Project_I.Api.Models;
using Project_I.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Project_I.Api.Services
{
    public class DynamoDBProjectStorage : IProjectStorage
    {
        private readonly IMapper _mapper;
        public DynamoDBProjectStorage(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> Add(ProjectModel projectModel)
        {
            var dbModel = _mapper.Map<ProjectDBModel>(projectModel);
            dbModel.Id = new Guid().ToString();
            dbModel.CreatedDateTime = DateTime.UtcNow;
            dbModel.Status = ProjectStatus.Pending;
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbModel);
                }
            }
            return dbModel.Id;
        }

        public async Task Confirm(ConfirmProjectModel confirmProjectModel)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    var record = await context.LoadAsync<ProjectDBModel>(confirmProjectModel.Id);
                    if (record == null)
                    {
                        throw new KeyNotFoundException("Not found");
                    }
                    if (confirmProjectModel.Status == ProjectStatus.Active)
                    {
                        record.Status = ProjectStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }
                }
            }
            
        }
    }
}
