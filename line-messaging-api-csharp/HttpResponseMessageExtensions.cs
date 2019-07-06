using LineDC.Messaging.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace LineDC.Messaging
{
    internal static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Validate the response status.
        /// </summary>
        /// <param name="response">HttpResponseMessage</param>
        /// <returns>HttpResponseMessage</returns>
        internal static async Task<HttpResponseMessage> EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                ErrorResponseMessage errorMessage;
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var jsonSerializerSettings = new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    jsonSerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
                    errorMessage = JsonConvert.DeserializeObject<ErrorResponseMessage>(content, jsonSerializerSettings);
                }
                catch
                {
                    errorMessage = new ErrorResponseMessage() { Message = content, Details = new ErrorDetail[0] };
                }
                throw new LineResponseException(errorMessage.Message) { StatusCode = response.StatusCode, ResponseMessage = errorMessage };

            }
        }
    }
}
