using Project_I.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_I.Api.Services
{
    public interface IProjectStorage
    {
        Task<string> Add(ProjectModel projectModel);
        Task Confirm(ConfirmProjectModel confirmProjectModel);
    }
}
