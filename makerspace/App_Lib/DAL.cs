using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace makerspace.App_Lib
{
    public class DAL
    {
        public class Paging
        {
            public int Page { get; set; }
            public int Page_Size { get; set; }
        }

        public class Paged_Results
        {
            public Paging Paging { get; set; }
            public string Order_By { get;set;}
            public int Count  { get; set; }
            public List<dynamic> Results { get; set; }

            override public String ToString()
            {
                try
                {
                    string data_string = JsonConvert.SerializeObject(
                        Results.ToList(),
                        Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                            MaxDepth = 3
                        }
                    );

                    return String.Format(@"{{
                        ""paging"":{{
                            ""page"":{0},
                            ""page_size"":{1}
                        }},
                        ""count"":{2},
                        ""order_by"":""{3}"",
                        ""data"":{4}
                    }}", this.Paging.Page, this.Paging.Page_Size, this.Count, (this.Order_By ?? ""), data_string);
                }
                catch (Exception e) 
                {
                    //
                    //to-do: log real error somewhere.
                    //
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    return String.Format(@"{{
                        ""error"":""{0}""
                    }}", "Unable to get data.");
                }
            }
        }
    }
}