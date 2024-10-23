using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Http;

namespace conanPlusWebApiApp.Dal
{
    public interface ICommonRepository<T> where T : class
    {
        Task<List<T>> GetAll(); // Get all records
        Task<T> GetDetails(int id); // Get specific record by ID
        Task<T> Insert(T entity); // Insert a new record
        Task<T> Update(T entity); // Update an existing record
        Task<T> Delete(int id); // Delete a record by ID
        Task<List<T>> Find(Expression<Func<T, bool>> predicate); // Find records based on a condition
        Task<User> GetUserByUsername(string username); // Get User by username
        void Detach(T entity); // Detach an entity to avoid tracking conflicts
        
        // New methods to handle file uploads
        Task<string> UploadFile(IFormFile file, string path); // Method to handle file uploads
        Task<bool> DeleteFile(string filePath); // Method to handle file deletion
    }
}
