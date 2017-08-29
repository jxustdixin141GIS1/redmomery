﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
    public partial class GroupUserDAL
    {
        public GroupUser getGroupUserBy(string UID, string GroupID)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GroupUser WHERE (UID = @UID and GroupID =@GroupID )", new SqlParameter("@UID", UID), new SqlParameter("@GroupID",GroupID));
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            GroupUser model = ToModel(row);
            return model;
        }
        public List<GroupUser> getGusers(string GID)
        {
            List<GroupUser> list = new List<GroupUser>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GroupUser   where  GroupID =@GroupID", new SqlParameter("@GroupID", GID));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
    }
}
