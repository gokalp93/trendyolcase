using System;
namespace TrendyolCaseStudyNetCore.Core.Entity
{
    public class Category
    {
        public Category(string title)
        {
            Title = title;
        }

        public Category(string title, Category parentCategory) : this(title)
        {
            ParentCategory = parentCategory;
        }

        public string Title { get; private set; }
        public Category ParentCategory { get; set; }
    }
}