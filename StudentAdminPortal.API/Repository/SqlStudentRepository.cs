using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }
                
        public async Task<Student> GetStudentByIdAsync(Guid studentId)
        {
            var result = await context.Student
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == studentId);

            return result;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            var result = await context.Student.Include(nameof(Gender)).Include(nameof(Address))
                .ToListAsync();

            return result;
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            var dbStudent = await GetStudentByIdAsync(studentId);

            if (dbStudent != null)
            {
                dbStudent.FirstName = request.FirstName;
                dbStudent.LastName = request.LastName;
                dbStudent.DateOfBirth = request.DateOfBirth;
                dbStudent.Email = request.Email;
                dbStudent.PhoneNumber = request.PhoneNumber;
                dbStudent.GenderId = request.GenderId;
                dbStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                dbStudent.Address.PostalAddress = request.Address.PostalAddress;

                await context.SaveChangesAsync();

                return dbStudent;
            }

            return null;
        }

        public async Task<Student> DeleteStudentAsync(Guid studentId)
        {
            var existingStudent = await GetStudentByIdAsync(studentId);

            if (existingStudent != null)
            {
                context.Student.Remove(existingStudent);
                await context.SaveChangesAsync();

                return existingStudent;
            }

            return null;
        }

        public async Task<Student> InsertStudentAsync(Student request)
        {
            var student = context.Student.Add(request);
            await context.SaveChangesAsync();

            return student.Entity;
        }
    }
}
