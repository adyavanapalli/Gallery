using System;
using System.Linq;
using System.Threading.Tasks;
using Gallery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.Controllers
{
    /// <summary>
    /// A controller for processing images.
    /// </summary>
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ImagesController : ControllerBase
    {
        /// <summary>
        /// A service used for reading and writing image data.
        /// </summary>
        private readonly IImageService _imageService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="imageService">A service used for reading and writing image data.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="imageService" /> is
        /// <c>null</c>.</exception>
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        }

        /// <summary>
        /// An action that removes the image with the specified <paramref name="id" /> from the backing data store if it
        /// exists. 
        /// </summary>
        /// <param name="id">The ID of the image to remove from the backing data store.</param>
        /// <returns>A task wrapping a 204 HTTP status code.</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteImageAsync([FromRoute] long id)
        {
            await _imageService.RemoveImageAsync(id);

            return NoContent();
        }

        /// <summary>
        /// An action that gets the image with the specified <paramref name="id" /> from the backing data store if it
        /// exists.
        /// </summary>
        /// <param name="id">The ID of the image to get.</param>
        /// <returns>A task wrapping a 200 HTTP status code with the image in the body.</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetImage([FromRoute] long id)
        {
            var image = await _imageService.GetImageAsync(id);

            return Ok(image);
        }

        /// <summary>
        /// An action that gets all images from the backing data store.
        /// </summary>
        /// <param name="thumbnailsOnly">A query parameter used to filter the results to include only the image
        /// thumbnails.</param>
        /// <returns>A task wrapping a 200 HTTP status code with images in the body.</returns>
        [HttpGet]
        public async Task<IActionResult> GetImagesAsync([FromQuery] bool thumbnailsOnly = true)
        {
            var images = await _imageService.GetImagesAsync();

            if (thumbnailsOnly)
            {
                return Ok(images.Select(image => new
                {
                    image.Id,
                    image.Name,
                    image.ThumbnailPixelData
                }));
            }

            return Ok(images);
        }

        /// <summary>
        /// An action that patches the image with the specified <paramref name="id" /> using the specified <paramref
        /// name="imagePatchModel" />.
        /// </summary>
        /// <param name="id">The ID of the image to patch.</param>
        /// <param name="imagePatchModel">The image patch model used to update the image.</param>
        /// <returns>A task wrapping a 200 HTTP status code with an empty body.</returns>
        [HttpPatch("{id:long}")]
        public async Task<IActionResult> PatchImageAsync([FromRoute] long id,
                                                         [FromBody] ImagePatchModel imagePatchModel)
        {
            if (imagePatchModel == null)
            {
                return BadRequest($"The image patch model is null.");
            }

            if (string.IsNullOrWhiteSpace(imagePatchModel.Name))
            {
                return BadRequest("The image's name is null or white space.");
            }

            var image = await _imageService.GetImageAsync(id);
            if (image == null)
            {
                return NotFound("The image to patch does not exist.");
            }

            image.Name = imagePatchModel.Name;
            await _imageService.UpdateImageAsync(image);

            return Ok();
        }

        /// <summary>
        /// A model that is passed into a HTTP PATCH method.
        /// </summary>
        public class ImagePatchModel
        {
            /// <summary>
            /// The name used to update an existing <see cref="Image" /> model.
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// An action that posts the image data specified in <paramref name="formFile" />.
        /// </summary>
        /// <param name="formFile">The form file containing the image data.</param>
        /// <returns>A task wrapping a 201 HTTP status code containing the route the image was created and the image
        /// data in the body.</returns>
        [HttpPost]
        public async Task<IActionResult> PostImageAsync([FromForm] IFormFile formFile)
        {
            if (string.IsNullOrWhiteSpace(formFile.FileName))
            {
                throw new ArgumentException($"The {nameof(formFile.FileName)} is null or whitespace.",
                                            nameof(formFile));
            }

            if (formFile.Length == 0)
            {
                throw new ArgumentException($"The {nameof(formFile.Length)} is 0.", nameof(formFile));
            }

            var image = await _imageService.Parse(formFile);

            await _imageService.AddImageAsync(image);

            return Created(new Uri(Request.GetDisplayUrl()), image);
        }
    }
}
