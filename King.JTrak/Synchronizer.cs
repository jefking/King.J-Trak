namespace King.JTrak
{
    using King.Azure.Data;
    using King.JTrak.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Aszure Table Synchronizer
    /// </summary>
    public class Synchronizer : ISynchronizer
    {
        #region Members
        /// <summary>
        /// From Azure Table Storage
        /// </summary>
        protected readonly ITableStorage from = null;

        /// <summary>
        /// To Azure Blob Storage
        /// </summary>
        protected readonly IContainer to = null;
        #endregion

        #region Constructors
        public Synchronizer(ConfigValues config)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run Synchronization
        /// </summary>
        /// <returns>Task</returns>
        public async Task Run()
        {
            await new TaskFactory().StartNew(() => { });
        }
        #endregion
    }
}