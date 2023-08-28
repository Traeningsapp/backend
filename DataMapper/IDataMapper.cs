﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IDataMapper<T>
    {
        T? FromJson(string json);
    }
}
