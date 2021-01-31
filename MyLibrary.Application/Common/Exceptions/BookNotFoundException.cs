﻿using MyLibrary.Application.Common.Enums;
using MyLibrary.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Application.Common.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(string message) : base(ErrorCodeEnum.BookNotFound, message)
        {
        }
    }
}
