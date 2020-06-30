using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_I.Api.Models;
using Project_I.Api.Services;
using Project_I.Model;

namespace Project_I.Api.Controllers
{
    [Route("api/v1/Project")]
    [ApiController]
    public class Project : ControllerBase
    {
        private readonly IProjectStorage _projectStorage;
        public Project(IProjectStorage projectStorage)
        {
            _projectStorage = projectStorage;
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200,Type=typeof(CreateProjectResponse))]
        public async Task<IActionResult> Create(ProjectModel projectModel)
        {
            var RecordId = string.Empty;
            try
            {
                RecordId = await _projectStorage.Add(projectModel);
            }
            catch(KeyNotFoundException KE)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
               
            }
            return StatusCode(201, new CreateProjectResponse() { Id= RecordId });
        }
        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmProjectModel confirmProjectModel)
        {
            try
            {
                await _projectStorage.Confirm(confirmProjectModel);
            }
            catch (KeyNotFoundException KE)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);

            }
            return Ok();
        }
    }
}
