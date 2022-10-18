using Market.Domain.Enum;

namespace Market.Domain.Response
{
    public class BaseResponse<T> : IBasePesponse<T>
    {
        //описание ошибки/предупреждения
        public string Desciption { get; set; }

        //код ошибки
        public StatusCode StatusCode { get; set; }
        
        public T Data { get; }
    }

    public interface IBasePesponse<T>
    {
        T Data { get; }
    }
}
