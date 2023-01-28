using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchollECommerce.Shared.Dtos
{
    public class ResponseDto<T>
    {
        //kendimize özel api dönüşleri için Shared klasörünün altında özel dönüşlerimizi tasarlaadık
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }

        //bool değer ve entity birlikte dönünce
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }

        //sadece bool deger dönünce
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccessful = true
            };

        }

        //birden fazla hata dönünce

        public static ResponseDto<T> Fail(List<string> errors, int statusCode) 
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }


        //tek hata dönünce
        public static ResponseDto<T> Fail(string error,int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = new List<string>() {error},
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }



    }
}
