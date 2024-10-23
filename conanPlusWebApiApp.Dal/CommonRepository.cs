using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using conanPlusWebApiApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Dal
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly conanPlusWebApiAppDbContext _context;

        public CommonRepository(conanPlusWebApiAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            if (typeof(T) == typeof(Service))
            {
                return await _context.Set<Service>()
                    .Include(s => s.Filters)
                    .Include(s => s.Projects)
                    .ToListAsync() as List<T>;
            }
            else if (typeof(T) == typeof(Filter))
            {
                return await _context.Set<Filter>()
                    .Include(f => f.Service)
                    .Include(f => f.Projects)
                    .ToListAsync() as List<T>;
            }
            else if (typeof(T) == typeof(Project))
            {
                return await _context.Set<Project>()
                    .Include(p => p.Service)
                    .Include(p => p.Filter)
                    .ToListAsync() as List<T>;
            }

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetDetails(int id)
        {
            if (typeof(T) == typeof(Service))
            {
                return await _context.Set<Service>()
                    .Include(s => s.Filters)
                    .Include(s => s.Projects)
                    .FirstOrDefaultAsync(s => s.ServiceId == id) as T;
            }
            else if (typeof(T) == typeof(Filter))
            {
                return await _context.Set<Filter>()
                    .Include(f => f.Service)
                    .Include(f => f.Projects)
                    .FirstOrDefaultAsync(f => f.FilterId == id) as T;
            }
            else if (typeof(T) == typeof(Project))
            {
                return await _context.Set<Project>()
                    .Include(p => p.Service)
                    .Include(p => p.Filter)
                    .FirstOrDefaultAsync(p => p.ProjectId == id) as T;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<T> Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            string keyPropertyName = typeof(T).Name switch
            {
                nameof(Feature) => "FeatureId",
                nameof(User) => "UserId",
                nameof(Partner) => "PartnerId",
                nameof(Service) => "ServiceId",
                nameof(Project) => "ProjectId",
                nameof(Filter) => "FilterId",
                nameof(Employee) => "EmployeeId",
                nameof(Vision) => "VisionId",
                nameof(Goal)=> "GoalId",
                nameof(Package) => "PackageId",
                nameof(FAQ)=> "FAQId",
                nameof(ContactInfo) => "ContactId",
                nameof(ContactForm) => "MessageId",
                nameof(PromoVideo) => "VideoId",
                _ => "Id"
            };

            var trackedEntity = _context.Set<T>().Local.FirstOrDefault(e =>
                _context.Entry(e).Property(keyPropertyName).CurrentValue.Equals(
                _context.Entry(entity).Property(keyPropertyName).CurrentValue));

            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        // Upload file method
        public async Task<string> UploadFile(IFormFile file, string path)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be empty.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(path, fileName);

            Directory.CreateDirectory(path);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fullPath;
        }

        // Delete file method
        public async Task<bool> DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
                return true;
            }
            return false;
        }
    }
}
