﻿namespace King.JTrak
{
    using King.Azure.Data;
    using King.JTrak.Models;
    using Microsoft.WindowsAzure.Storage.Table;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Linq;
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

        /// <summary>
        /// Blob Structure
        /// </summary>
        protected readonly BlobStructure structure = BlobStructure.MultipleBlobs;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="config">Configuration</param>
        public Synchronizer(IConfigValues config)
        {
            if (null == config)
            {
                throw new ArgumentNullException("config");
            }

            this.from = new TableStorage(config.FromTable, config.FromConnectionString);
            this.to = new Container(config.ToContainer, config.ToConnectionString);
            this.structure = config.Structure;
        }

        /// <summary>
        /// Mockable Constructor
        /// </summary>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        public Synchronizer(ITableStorage from, IContainer to, BlobStructure structure = BlobStructure.MultipleBlobs)
        {
            if (null == from)
            {
                throw new ArgumentNullException("from");
            }
            if (null == to)
            {
                throw new ArgumentNullException("to");
            }

            this.from = from;
            this.to = to;
            this.structure = structure;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run Synchronization
        /// </summary>
        /// <returns>Task</returns>
        public async Task Run()
        {
            Trace.TraceInformation("Initializing: {0}.", this.to.Name);
            await this.to.CreateIfNotExists();

            Trace.TraceInformation("Loading data from {0}.", this.from.Name);
            var entities = await this.from.Query(new TableQuery());
            if (null != entities && entities.Any())
            {
                Trace.TraceInformation("Storing data to {0}.", this.to.Name);
                switch(structure)
                {
                    case BlobStructure.SingleBlob:
                        await this.to.Save(string.Format("{0}.json", from.Name), JsonConvert.SerializeObject(entities));
                        break;
                    default:
                        foreach (var e in entities)
                        {
                            var name = string.Format("{0}-{1}.json", e[TableStorage.PartitionKey], e[TableStorage.RowKey]);
                            await this.to.Save(name, e);
                        }
                        break;
                }
            }
            else
            {
                Trace.TraceInformation("No entities in {0}.", this.from.Name);
            }
        }
        #endregion
    }
}