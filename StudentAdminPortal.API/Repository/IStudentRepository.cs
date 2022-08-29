using StudentAdminPortal.API.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId, Student request);
        Task<List<Gender>> GetGendersAsync();
        Task<Student> DeleteStudentAsync(Guid studentId);
        Task<Student> InsertStudentAsync(Student request);
    }
}
