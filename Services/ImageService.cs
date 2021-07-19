using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gallery.Constants;
using Gallery.Models;
using Imageflow.Fluent;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Gallery.Services
{
    /// <summary>
    /// Implements the operations specified in the <see cref="IImageService" />.
    /// </summary>
    public class ImageService : IImageService
    {
        /// <summary>
        /// A set of key/value application configuration properties.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// A service used for reading and writing data.
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">A set of key/value application configuration properties.</param>
        /// <param name="dataService">A service used for reading and writing data.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="configuration" /> is
        /// <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dataService" /> is
        /// <c>null</c>.</exception>
        public ImageService(IConfiguration configuration, IDataService dataService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        /// <inheritdoc />
        public async Task AddImageAsync(Image image)
        {
            _ = image ?? throw new ArgumentNullException(nameof(image));

            if (string.IsNullOrWhiteSpace(image.Name))
            {
                throw new ArgumentException("The image's name is null or white space.", nameof(image));
            }

            if (image.ImagePixelData.Length == 0)
            {
                throw new ArgumentException("The image's pixel data is empty.", nameof(image));
            }

            if (image.ThumbnailPixelData.Length == 0)
            {
                throw new ArgumentException("The image's thumbnail data is empty.", nameof(image));
            }

            await _dataService.AddEntityAsync(image);
        }

        /// <summary>
        /// Asynchronously generates an image thumbnail with the specified <paramref name="imagePixelData" />.
        /// </summary>
        /// <param name="imagePixelData">The image pixel data to generate a thumbnail for.</param>
        /// <returns>The corresponding thumbnail pixel data for the specified <paramref name="imagePixelData"
        /// />.</returns>
        private async Task<byte[]> GenerateThumbnail(byte[] imagePixelData)
        {
            using var imageJob = new ImageJob();
            var buildJobResult = await imageJob.Decode(imagePixelData)
                                               .Constrain(new Constraint(ConstraintMode.Fit_Crop,
                                                                         _configuration.GetValue<uint>(JsonConfigurationVariable.ThumbnailDefaultWidth),
                                                                         _configuration.GetValue<uint>(JsonConfigurationVariable.ThumbnailDefaultHeight)))
                                               .EncodeToBytes(new MozJpegEncoder(_configuration.GetValue<int>(JsonConfigurationVariable.ThumbnailDefaultQuality)))
                                               .Finish()
                                               .InProcessAsync();

            return buildJobResult.First.TryGetBytes().Value.Array;
        }

        /// <inheritdoc />
        public async Task<List<Image>> GetImagesAsync() => await _dataService.GetEntitiesAsync<Image>();

        /// <inheritdoc />
        public async Task<Image> GetImageAsync(long id) => await _dataService.GetEntityAsync<Image>(id);

        /// <inheritdoc />
        public async Task<Image> Parse(IFormFile formFile)
        {
            using var stream = formFile.OpenReadStream();

            var imagePixelData = new byte[formFile.Length];
            await stream.ReadAsync(imagePixelData.AsMemory(0, checked((int)formFile.Length)));

            var thumbnailPixelData = await GenerateThumbnail(imagePixelData);

            var image = new Image
            {
                Name = formFile.FileName,
                ImagePixelData = imagePixelData,
                ThumbnailPixelData = thumbnailPixelData,
            };

            return image;
        }

        /// <inheritdoc />
        public async Task RemoveImageAsync(long id) => await _dataService.RemoveEntityAsync<Image>(id);

        /// <inheritdoc />
        public async Task UpdateImageAsync(Image image)
        {
            _ = image ?? throw new ArgumentNullException(nameof(image));

            if (string.IsNullOrWhiteSpace(image.Name))
            {
                throw new ArgumentException("The image's name is null or white space.", nameof(image));
            }

            if (image.ImagePixelData.Length == 0)
            {
                throw new ArgumentException("The image's pixel data is empty.", nameof(image));
            }

            if (image.ThumbnailPixelData.Length == 0)
            {
                throw new ArgumentException("The image's thumbnail data is empty.", nameof(image));
            }

            await _dataService.UpdateEntityAsync(image);
        }
    }
}
