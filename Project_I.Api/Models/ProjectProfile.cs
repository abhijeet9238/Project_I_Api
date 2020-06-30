using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project_I.Model;

namespace Project_I.Api.Models
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectModel, ProjectDBModel>();
        }
    }
}
