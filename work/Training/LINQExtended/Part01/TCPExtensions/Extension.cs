using System;
using System.Collections.Generic;

namespace TCPExtensions
{
    public static class Extension
    {
        /// <summary>
        /// This method will return a list of objects of type <T> to the calling client code
        /// </summary>
        /// <param name="records"></param>
        /// <param name="func"></param>
        /// This keyword in this context indicates that relevant method can be called on a object of the data type of the relevant paramater
        /// In this scenario that is a generic List, which contains a type T placeholder
        public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
        {
            List<T> filteredList = new List<T>();

            foreach (T record in records)
            {
                if (func(record))
                {
                    filteredList.Add(record);
                }
            }
            return filteredList;
        }
    }
}
