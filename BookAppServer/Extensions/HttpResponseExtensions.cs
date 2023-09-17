using Azure;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BookAppServer.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AddPagedMetadataHeaders(this HttpResponse response, MetaData metaData)
        {
            response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));
            response.Headers.Add("Access-Control-Allow-Headers", "X-Pagination");
        }
    }
}
