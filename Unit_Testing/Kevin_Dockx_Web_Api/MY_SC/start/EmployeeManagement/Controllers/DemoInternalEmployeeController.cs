using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Controllers
{
    [Route("api/demointernalemployees")]
    public class DemoInternalEmployeeController: ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public DemoInternalEmployeeController(IEmployeeService employeeService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<InternalEmployeeDto>> CreateInternalEmployee(
            InternalEmployeeForCreationDto internalEmployeeForCreation)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // create an internal employee entity with default values filled out
            // and the values inputted via the POST request
            var internalEmployee =
                    await _employeeService.CreateInternalEmployeeAsync(
                        internalEmployeeForCreation.FirstName, internalEmployeeForCreation.LastName);

            // persist it
            await _employeeService.AddInternalEmployeeAsync(internalEmployee);

            // return created employee after mapping to a DTO
            return CreatedAtAction("GetInternalEmployee",
                _mapper.Map<InternalEmployeeDto>(internalEmployee),
                new { employeeId = internalEmployee.Id });
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProtectedInternalEmployees()
        {
            if(User.IsInRole("Admin"))
            {
                return RedirectToAction(
                    "GetInternalEmployees", "ProtectedInternalEmployees");
            }

            return RedirectToAction("GetInternalEmployees", "InternalEmployees");

        }
    }
}
