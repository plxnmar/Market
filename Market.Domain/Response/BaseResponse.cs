using Market.Domain.Enum;

namespace Market.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        //описание ошибки/предупреждения
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }

    }

    public interface IBaseResponse<T>
    {
        //код ошибки
        public StatusCode StatusCode { get; set; }
        public string Description { get; set; }
        public T Data { get; set; }
    }
}
