using InventoryClassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO
{
    public class ResponseDTO<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; } 
        public DateTime? ExpireDate { get; set; } 
        public static SuccessResponseDTO<T> Success(T data, string message = "",DateTime? expireDate=null)
        {
            return new SuccessResponseDTO<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                ErrorCode = ErrorCode.None,
                ExpireDate = expireDate
            };
        }

        public static ErrorResponseDTO Error(ErrorCode errorCode, string message = "")
        {
            return new ErrorResponseDTO
            {
                Data = default,
                IsSuccess = false,
                Message = message,
                ErrorCode = errorCode,
            };
        }
    }

    public class SuccessResponseDTO<T> : ResponseDTO<T>
    {

    }

    public class ErrorResponseDTO : ResponseDTO<bool>
    {

    }
}
