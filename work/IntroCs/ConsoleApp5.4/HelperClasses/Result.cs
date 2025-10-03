using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    public class Result<T>
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        // Data because it will be used in different methods 
        public T? Data { get; }
        private Result(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static Result<T> Ok(T data, string message = "") => new(true, message, data);
        public static Result<T> Fail(string message) => new(false, message, default);

    }
}
