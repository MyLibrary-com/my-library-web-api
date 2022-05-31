﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApollosLibrary.Application.Interfaces
{
    public interface IUserService
    {
        Guid GetUserId();

        string GetUserEmail();
    }
}
