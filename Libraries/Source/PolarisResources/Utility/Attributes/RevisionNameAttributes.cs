using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolarisResources.Utility.Attributes
{
    #region Usings ...

    using System;

    #endregion

    /// <summary>
    /// Revision name attribute (name of the release)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class RevisionNameAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RevisionNameAttribute"/> class.
        /// </summary>
        /// <param name="name">
        /// Revision name
        /// </param>
        public RevisionNameAttribute(string name)
        {
            this.RevisionName = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Revision name
        /// </summary>
        public string RevisionName { get; set; }

        #endregion
    }
}
