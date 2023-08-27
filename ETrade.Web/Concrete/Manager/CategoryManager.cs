//using ETrade.Abstract;
//using ETrade.Entities;

//namespace ETrade.Web.Concrete.Manager
//{
//    public class CategoryManager
//    {

//        private ICategoryRepository _categoryRepository;
//        public CategoryManager(ICategoryRepository categoryRepository)
//        {
//            _categoryRepository = categoryRepository;
//        }

//        public void Create(Category entity)
//        {
//            //ürün oluştur
//            _categoryRepository.Create(entity);
//        }

//        public void Delete(Category entity)
//        {
//            //ürün sil
//            _categoryRepository.Delete(entity);
//        }

//        public List<Category> GetAll()
//        {
//            //tüm ürünleri al
//            return _categoryRepository.GetAll();
//        }

//        public Category GetById(int id)
//        {
//            return _categoryRepository.GetById(id);
//        }

//        public void Update(Category entity)
//        {
//            _categoryRepository.Update(entity);
//        }
//    }
//}

