using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models.Interfaces;

namespace Web.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly CategoryContext _context;
        public BaseRepository(CategoryContext context) { 
            _context = context;
        }
        async Task<IEnumerable<T>> IBaseRepository<T>. GetAllAsync()
        {
            var content= await _context.Set<T>().ToListAsync();
            return (content);
        }
        async Task<T> IBaseRepository<T>.GetByIdAsync(int id)
        {
            var item=await _context.Set<T>().FindAsync(id);
            return (item);
        }

        async Task IBaseRepository<T>.CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        async Task IBaseRepository<T>.UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        async Task IBaseRepository<T>.DeleteAsync(int id)
        {
            var item=await _context.Set<T>().FindAsync(id);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
                await _context.SaveChangesAsync();
            }
          
        }
    }
}
