using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.Helpers
{
    /// <summary>
    /// Generic Container to ease the data validation and pass informative messages
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataContainer<T>
    {

        public DataContainer()
        {
            isValidData = false;
            Message = String.Empty;
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool isValidData { get; set; }
    }
}
