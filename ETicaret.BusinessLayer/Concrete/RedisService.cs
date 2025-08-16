using ETicaret.BusinessLayer.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Concrete
{

    public class RedisService 
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _redis;

        
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect()
        {
            var configOptions = new ConfigurationOptions
            {
                EndPoints = { $"{_host}:{_port}" },
                AbortOnConnectFail = false  // Bağlantı başarısız olsa bile uygulama çökmesin
            };

            _redis = ConnectionMultiplexer.Connect(configOptions);
        }


        public IDatabase GetDb(int db = 0) => _redis.GetDatabase(db);
    }
}


