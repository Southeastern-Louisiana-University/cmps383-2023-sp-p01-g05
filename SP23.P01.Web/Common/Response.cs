using System;
using System.Collections.Generic;
using SP23.P01.Web.Common;


namespace SP23.P01.Web.Common
{
    
        public class Response
        {
            public object Data { get; set; }
            public List<Error> Errors { get; set; } = new List<Error>();
            public bool HasErrors => Errors.Count > 0;

            public void AddError(string property, string message)
            {
                Errors.Add(new Error(property, message));
            }
        }
    }

