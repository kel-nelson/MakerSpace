using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace makerspace.Models
{
    public class Generic_Title
    {
        public string Title { get; set; }
    }

    public class Area_User_Memberships
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<App_User_Profiles> Members { get; set; }
    }

    public class AreaJoinAsMemberModel
    {
        public int area_id { get; set; }
    }

    public class AreaJoinModel
    {
        public int user_id { get; set; }
        public int area_id { get; set; }
        public int member_type_id { get; set; }
    }
}