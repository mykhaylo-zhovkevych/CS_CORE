using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    public class Result
    {
        public bool Success { get; }
        public string Message { get; }

        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static Result Fail(string message) => new(false, message);
        public static Result Notify(string message) => new(true, message);

        public override string ToString()
        {
            return $"Status: {Success}, Message: {Message}";
        }
    } 

    public class Result<T> : Result
    {
        public T? Data { get; }
 
        public Result(bool success, string message, T? data) 
            : base (success, message)
        {
            Data = data;
        }

        // Factory methods not allowing creating inconsistent objects 
        public static Result<T> Ok(T data, string message = "") => new(true, message, data);

        public override string ToString()
        {
            return $"Status: {Success}, Message: {Message}, Data: {Data}";
        }
    }
}