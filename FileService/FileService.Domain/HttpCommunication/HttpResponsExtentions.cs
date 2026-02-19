using CSharpFunctionalExtensions;
using SharedKernel.Exseption;
using System.Net.Http.Json;


namespace FileService.Domain.HttpCommunication
{
    public static class HttpResponsExtentions
    {
        public static async Task<Result<TResponse, Error>> HandlerResponseAsync<TResponse>(this HttpResponseMessage respose, CancellationToken cancellationToken)
        {

            try
            {
                if (!respose.IsSuccessStatusCode)
                    return Error.Unknown("fail.test", "ошибкка в edpoint");

                var data = await respose.Content.ReadFromJsonAsync<TResponse>(cancellationToken);


                if (data is null)
                    return Error.Unknown("fail.test", "ошибкка при чтении ответа");

                return data;
            }
            catch
            {
                return Error.Unknown("fail.test", "ошибкка при чтении ответа");
            }

        }
    }
}
