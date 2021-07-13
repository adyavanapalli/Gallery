using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Models
{
    /// <summary>
    /// A model representing an image.
    /// </summary>
    [Table("images")]
    public class Image
    {
        /// <summary>
        /// The primary identifier for this image.
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// The name of this image.
        /// </summary>
        [Required]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// The underlying pixel data for this image.
        /// </summary>
        [Required]
        [Column("image_pixel_data")]
        public byte[] ImagePixelData { get; set; }

        /// <summary>
        /// The underlying pixel data for this image's thumbnail.
        /// </summary>
        [Required]
        [Column("thumbnail_pixel_data")]
        public byte[] ThumbnailPixelData { get; set; }
    }
}
