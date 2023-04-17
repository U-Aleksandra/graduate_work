using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NameService> nameServices { get; private set; }
    }
}
