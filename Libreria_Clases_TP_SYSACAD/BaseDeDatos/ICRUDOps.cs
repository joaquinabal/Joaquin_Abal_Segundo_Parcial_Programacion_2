﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_Clases_TP_SYSACAD.BaseDeDatos
{
    public interface ICRUDOps<T> where T : class
    {
        bool Add();
        bool Delete();
        bool Delete(int id);

        bool Update();

        List<T> GetAll();
        T Map(IDataRecord reader);

    }
}
