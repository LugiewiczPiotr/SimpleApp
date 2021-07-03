﻿using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface ICategoryLogic : ILogic
    {
        Result<IEnumerable<Category>> GetAllActive();

        Result<Category> GetById(Guid id);

        Result<Category> Add(Category category);

        Result<Category> Update(Category category);

        Result Delete(Category category);
    }
}
