using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Presentation.Views.Utils
{
    public class ApiError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }


    }
}
