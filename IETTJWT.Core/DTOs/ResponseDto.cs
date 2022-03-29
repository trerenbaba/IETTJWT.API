using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IETTJWT.Core.DTOs
{
    public class ResponseDto<T>
    {

        public T Data { get; set; }

        public List<string> Errors { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }


        public static ResponseDto<T> Success(T Data, int statusCode)
        {

            return new ResponseDto<T> { Data = Data, StatusCode = statusCode };
        }

        public static ResponseDto<T> Success(int statusCode)
        {

            return new ResponseDto<T> { Data = default(T), StatusCode = statusCode };
        }

        public static ResponseDto<T> Fail(List<String> errors, int statusCode)
        {

            return new ResponseDto<T> { Errors = errors, StatusCode = statusCode };
        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {

            return new ResponseDto<T> { Errors = new List<string>() { error }, StatusCode = statusCode };
        }


    }
}
