using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class FILE_TABLEDAL
    {
        public int addNew(FILE_TABLE model)
        {
            return AddNew(model);
        }

        public bool delete(int id)
        {
            return Delete(id);
        }

        public bool update(FILE_TABLE model)
        {
            return Update(model);
        }

        public FILE_TABLE get(int id)
        {
            return Get(id);
        }

      
        public IEnumerable<FILE_TABLE> Listall()
        {
            return ListAll();
        }
	}
}
