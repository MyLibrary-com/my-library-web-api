﻿using System;
using System.Collections.Generic;

namespace MyLibrary.Data.Model
{
    public partial class FormType
    {
        public FormType()
        {
            Book = new HashSet<Book>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
