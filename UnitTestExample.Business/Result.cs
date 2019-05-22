using System;
using System.Collections.Generic;

namespace UnitTestExample.Business
{
    /// <summary>
    /// Results for CRUD operations
    /// </summary>
    public class Result
    {
        public string Action { get; set; }

        public bool Success
        {
            get { return _validations == null || _validations.Count == 0; }
        }

        private List<string> _validations = new List<string>();
        public List<string> Validations
        {
            get { return _validations; }
        }

        public Result()
        {
        }
    }
}
