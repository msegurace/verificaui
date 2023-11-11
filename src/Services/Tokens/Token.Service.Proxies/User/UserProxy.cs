using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Token.Service.Proxies.User
{
    public interface IUserProxy
    {
        //https://www.youtube.com/watch?v=JhP5GHVSoxY&list=PLNBxBZe3xfYzjwnUgCkht9CNQnwPoRsHp&index=32
    }

    public  class UserProxy: IUserProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _httpClient;

        public UserProxy(IOptions<ApiUrls> urls, HttpClient httpClient)
        {
            _urls = urls.Value;
            _httpClient = httpClient;

        }
    }
}
