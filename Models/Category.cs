using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    [Display(Name = "Category")]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The length must be at least 1 simbol and maximum 50")]
        public string Title { get; set; }
        public byte[] Image { get; set; }

        [Display(Name = "Category level")]
        public int CategoryLevel { get; set; }
        public int Order { get; set; }
        public bool IsTop { get; set; }
        //public bool isContainSubCategory { get; set; }

        //Navigation
        [ForeignKey("ParentCategory")]
        public int? ParentCategoryId { get; set; }
        
        [Display(Name = " Parent category")]
        public Category ParentCategory { get; set; }
        public ICollection<Category> Categories { get; set; }
        
        public ICollection<CategoryProduct> CategoryProducts { get; set; }

        //public ICollection<Template> Templates { get; set; }
        public int? TemplateId { get; set; }
        public Template Template { get; set; }



        //////////////////////////////////////////////////
        //public int? ProductId { get; set; }
        //public ICollection<Product> Products { get; set; }
        /// //////////////////////////////////////////////

        // Evaluation properties 
        public bool IsContainSubCategories
        {
            get
            {
                if (Categories != null) // смысл в том, что если я где-нибудь подгружу Categories, то они будут не null, но Count может быть 0
                    if (Categories.Count() != 0)
                        return true;

                return false;
            }
        }

        public bool IsContainProducts
        {
            get
            {
                if (CategoryProducts != null) // ... аналогично как выше
                    if (CategoryProducts.Count() != 0)
                        return true;
          
                    return false;

                //if (CategoryProducts == null) 
                //    return true;

                //else if (CategoryProducts.Count() != 0) 
                //    return true;

                //else 
                //    return false; 
            }
        }
    }
}
