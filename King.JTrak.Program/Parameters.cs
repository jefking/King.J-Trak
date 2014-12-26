﻿namespace King.JTrak.Program
{
    using King.JTrak.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// Command Line Parameters
    /// </summary>
    public class Parameters
    {
        #region Members
        /// <summary>
        /// Arguments
        /// </summary>
        protected readonly IReadOnlyList<string> arguments;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="arguments">Arguments</param>
        public Parameters(IReadOnlyList<string> arguments)
        {
            if (null == arguments)
            {
                throw new ArgumentNullException("arguments");
            }
            if (!arguments.Any() || arguments.Count() != 2)
            {
                throw new ArgumentException("Invalid parameter count.");
            }

            this.arguments = arguments;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Process Configuration
        /// </summary>
        /// <returns>Configuration Values</returns>
        public virtual IConfigValues Process()
        {
            BlobStructure structure;
            Enum.TryParse(ConfigurationManager.AppSettings["Structure"], out structure);
            return new ConfigValues
            {
                FromConnectionString = this.arguments.ElementAt(0),
                FromTable = this.arguments.ElementAt(1),
                ToConnectionString = this.arguments.ElementAt(2),
                ToContainer = this.arguments.ElementAt(3),
                Structure = structure,
            };
        }
        #endregion
    }
}