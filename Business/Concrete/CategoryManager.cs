using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            var categoryId = _categoryDal.GetById(id);
            _categoryDal.Delete(categoryId);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.List();
        }

        public Category GetById(int id)
        {
            return _categoryDal.Get(x=>x.Id == id);
        }

        public void Update(Category category)
        {
            Category resultCategory = _categoryDal.GetById(x => x.Id == category.Id);
            resultCategory.Name = category.Name;
            _categoryDal.Update(resultCategory);
        }
    }
}
