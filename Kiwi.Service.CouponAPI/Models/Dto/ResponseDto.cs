namespace Kiwi.Service.CouponAPI.Models
{

    public class ResponseDto
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        protected ResponseDto(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ResponseDto Success()
        {
            return new ResponseDto(true, string.Empty);
        }

        public static ResponseDto Failure(string errorMessage)
        {
            return new ResponseDto(false, errorMessage);
        }
    }
    public class ResponseDto<T> : ResponseDto
    {
        public T Data { get; }

        private ResponseDto(T data, bool isSuccess, string errorMessage)
            : base(isSuccess, errorMessage)
        {
            Data = data;
        }

        public static ResponseDto<T> Success(T data)
        {
            return new ResponseDto<T>(data, true, string.Empty);
        }

        public static ResponseDto<T> Failure(string errorMessage)
        {
            return new ResponseDto<T>(default, false, errorMessage);
        }
    }
}
