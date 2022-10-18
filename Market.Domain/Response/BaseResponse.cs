using Market.Domain.Enum;

namespace Market.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        //описание ошибки/предупреждения
        public string Desciption { get; set; }

        //код ошибки
        public StatusCode StatusCode { get; set; }
        
        public T Data { get; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; }
    }
}
