﻿using Newtonsoft.Json;

namespace SSS.Infrastructure.Util.Json
{
    public static class Json
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        } 
    }
}
