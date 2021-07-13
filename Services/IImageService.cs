using System.Collections.Generic;
using System.Threading.Tasks;
using Gallery.Models;
using Microsoft.AspNetCore.Http;

namespace Gallery.Services
{
    /// <summary>
    /// An interface that specifies the operations an image service should implement.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Asynchronously adds the specified <paramref name="image" /> to the backing data store.
        /// </summary>
        /// <param name="image">The image to add to the backing data store.</param>
        /// <returns>A task wrapping this asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" />'s name is <c>null</c> or white
        /// space.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" />'s pixel data is
        /// empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" />'s thumbnail data is
        /// empty.</exception>
        Task AddImageAsync(Image image);

        /// <summary>
        /// Asynchronously gets all images from the backing data store.
        /// </summary>
        /// <returns>A task wrapping a list of all images.</returns>
        Task<List<Image>> GetImagesAsync();

        /// <summary>
        /// Asynchronously gets the image with the specified <paramref name="id" /> from the backing data store if it
        /// exists.
        /// </summary>
        /// <param name="id">The ID of the image to get.</param>
        /// <returns>A task wrapping the image with the specified <paramref name="id" />.</returns>
        Task<Image> GetImageAsync(long id);

        /// <summary>
        /// Asynchronously parses the specified <paramref name="formFile" /> into an <see cref="Image" />.
        /// <para>
        /// The image's pixel data is used to generate a thumbnail.
        /// </para>
        /// </summary>
        /// <param name="formFile">The file sent with the HTTP request.</param>
        /// <returns>A task wrapping the parsed image.</returns>
        Task<Image> Parse(IFormFile formFile);

        /// <summary>
        /// Asynchronously removes the image with the specified <paramref name="id" /> from the backing data store if it
        /// exists.
        /// </summary>
        /// <param name="id">The ID of the image to remove.</param>
        /// <returns>A task wrapping this asynchronous operation.</returns>
        Task RemoveImageAsync(long id);

        /// <summary>
        /// Asynchronously updates the specified <paramref name="image" /> in the backing data store.
        /// </summary>
        /// <param name="image">The image to update in the backing data store.</param>
        /// <returns>A task wrapping this asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" />'s name is <c>null</c> or white
        /// space.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" />'s pixel data is
        /// empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image" />'s thumbnail data is
        /// empty.</exception>
        Task UpdateImageAsync(Image image);
    }
}
