using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace AzureFunctionApp1
{
    public class ResizeImageOnBlobUpload
    {
        private readonly ILogger<ResizeImageOnBlobUpload> _logger;

        public ResizeImageOnBlobUpload(ILogger<ResizeImageOnBlobUpload> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ResizeImageOnBlobUpload))]
        //Container path name should be in lower case. It will be created automatically it does not exist
        [BlobOutput("app-storage-container-test-imgresized/{name}")]
        //public async Task Run([BlobTrigger("appstoragetest/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
        public async Task<byte[]> Run([BlobTrigger("app-storage-container-test/{name}", Connection = "AzureWebJobsStorage")] byte[] myBlobArray, string name)
        {
            //using var blobStreamReader = new StreamReader(stream);
            //var content = await blobStreamReader.ReadToEndAsync();
           using var memoryStream = new MemoryStream(myBlobArray);
            using var image=Image.Load(memoryStream);
            image.Mutate(x => x.Resize(100,100));
            using var outputStream = new MemoryStream();
            image.SaveAsJpeg(outputStream);
            outputStream.Position = 0;
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name}");
            return outputStream.ToArray();
        }
    }
}
