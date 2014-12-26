namespace King.JTrak.Models
{
    /// <summary>
    /// Configuration Values Interfaces
    /// </summary>
    public interface IConfigValues
    {
        #region Properties
        /// <summary>
        /// From Table Name
        /// </summary>
        string FromTable
        {
            get;
        }

        /// <summary>
        /// To Container Name
        /// </summary>
        string ToContainer
        {
            get;
        }

        /// <summary>
        /// From Connection String
        /// </summary>
        string FromConnectionString
        {
            get;
        }

        /// <summary>
        /// To Connection String
        /// </summary>
        string ToConnectionString
        {
            get;
        }
        #endregion
    }
}