using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktSoftware.BusinessLayer.Abstract;
using MarktSoftware.Entity;

namespace MarktSoftware.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        public override int Delete(Category category)
        {
            ProductManager productManager = new ProductManager();
            LikedManager likedManager = new LikedManager();
            CommentManager commentManager = new CommentManager();

            // Kategori ile ilişkili notların silinmesi gerekiyor.
            foreach (Product product in category.Products.ToList())
            {

                // Note ile ilişikili like'ların silinmesi.
                foreach (Liked like in product.Likes.ToList())
                {
                    likedManager.Delete(like);
                }

                // Note ile ilişkili comment'lerin silinmesi
                foreach (Comment comment in product.Comments.ToList())
                {
                    commentManager.Delete(comment);
                }

                productManager.Delete(product);
            }

            return base.Delete(category);
        }
    }

}
