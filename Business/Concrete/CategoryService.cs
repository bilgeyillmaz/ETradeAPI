using Business.Abstract;
using Core.DataAccess;
using Core.UnitOfWork;
using Core.Utilities.Exceptions;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IEntityRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<Category> GetByNameAsync(string name)
        {
            var category = GetAllAsync().Result.FirstOrDefault(x => x.Name != null && x.Name.ToLower().Contains(name.ToLower()));
            if (category == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({name}) not found");
            }
            return Task.FromResult(category);
        }

    }
}
