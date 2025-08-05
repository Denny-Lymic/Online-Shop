using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.Core
{
    public static class ImageStub
    {
        public static IFormFile GetFormFile()
        {
            byte[] bytes = Encoding.UTF8.GetBytes("stub content");
            var ms = new MemoryStream(bytes);

            return new FormFile(ms,
                                0,
                                ms.Length,
                                "image",    
                                "test.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg",
                ContentDisposition = "form-data; name=\"image\"; filename=\"test.jpg\""
            };
        }
    }
}
