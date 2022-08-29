using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{

    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpDelete]
        [Route("[controller]/{studentId}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {

            if (await studentRepository.GetStudentByIdAsync(studentId) != null)
            {
                // Delete Student
                var student = await studentRepository.DeleteStudentAsync(studentId);

                return Ok(mapper.Map<Student>(student));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/add-student")]
        public async Task<IActionResult> InsertStudentAsync([FromBody] StudentRequest request)
        {
            var student = await studentRepository.InsertStudentAsync(mapper.Map<DataModels.Student>(request));

            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id }, mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] StudentRequest request)
        {
            var student = await this.studentRepository.GetStudentByIdAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                // Update
                // Convert WebRequest to DataModel
                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));

                if (updatedStudent == null)
                {
                    return NotFound();
                }
                else
                {
                    // Convert DataModel to WebModel
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
        }

        [HttpGet]
        [Route("[controller]/{studentId}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            // Fetch
            var student = await studentRepository.GetStudentByIdAsync(studentId);

            // Validate
            if(student == null)
            {
                return NotFound();
            }
            
            return Ok(mapper.Map<Student>(student));
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();

            // automaps datamodel to webmodel
            var result = mapper.Map<List<Student>>(students);

            return Ok(result);

            // This is the manual mapping but we use automapper instead
            //var domainModelStudents = new List<Student>();

            //foreach(var student in students)
            //{
            //    domainModelStudents.Add(new Student()
            //    {
            //        Id = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName,
            //        DateOfBirth = student.DateOfBirth,
            //        Email = student.Email,
            //        PhoneNumber = student.PhoneNumber,
            //        ProfileImageUrl = student.ProfileImageUrl,
            //        GenderId = student.GenderId,
            //        Gender = new Gender()
            //        {
            //            GenderId = student.Gender.GenderId,
            //            Description = student.Gender.Description,
            //        },
            //        Address = new Address()
            //        {
            //            AddressId = student.Address.AddressId,
            //            PhysicalAddress = student.Address.PhysicalAddress,
            //            PostalAddress = student.Address.PostalAddress,
            //            StudentId = student.Address.StudentId,
            //        }
            //    });
            //}
        }
    }
}
