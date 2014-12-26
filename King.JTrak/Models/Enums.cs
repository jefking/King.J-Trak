namespace King.JTrak.Models
{
    /// <summary>
    /// Blob Structures
    /// </summary>
    public enum BlobStructure : byte
    {
        /// <summary>
        /// Blob per entity
        /// </summary>
        MultipleBlobs = 0,
        /// <summary>
        /// Blob per table
        /// </summary>
        SingleBlob = 1,
    }
}