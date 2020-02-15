﻿using MyLibrary.Contracts.UnitOfWork;
using MyLibrary.DataLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Services.XUnitTestProject.MockClasses
{
    public class MockUserUnitOfWork : IUserUnitOfWork
    {
        public IUserDataLayer MockUserDataLayer { get; set; }

        public IUserDataLayer UserDataLayer
        {
            get
            {
                return MockUserDataLayer;
            }
        }

        public void Begin()
        {
            
        }

        public void Commit()
        {
            
        }

        public void Save()
        {
            
        }
    }
}
