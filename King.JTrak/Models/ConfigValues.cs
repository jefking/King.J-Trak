namespace King.JTrak.Models
{
    /// <summary>
    /// Configuration Values
    /// </summary>
    public class ConfigValues : IConfigValues
    {
        #region Properties
        /// <summary>
        /// From Table Name
        /// </summary>
        public virtual string FromTable
        {
            get;
            set;
        }

        /// <summary>
        /// To Container Name
        /// </summary>
        public virtual string ToContainer
        {
            get;
            set;
        }

        /// <summary>
        /// From Connection String
        /// </summary>
        public virtual string FromConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// To Connection String
        /// </summary>
        public virtual string ToConnectionString
        {
            get;
            set;
        }
        #endregion
    }
}
