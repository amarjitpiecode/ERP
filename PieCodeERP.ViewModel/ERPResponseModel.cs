using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieCodeERP.ViewModel
{
    public class ERPResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value><c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }
        /// <summary>
        /// Gets or sets the List Count.
        /// </summary>
        /// <value>The model.</value>
        public int ListCount { get; set; }
        public List<ErrorDet> Errors { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        /// <value>The Status.</value>
        public int Status { get; set; }
    }

    public class ErrorDet
    {
        public string ErrorField { get; set; }
        public string ErrorDescription { get; set; }
    }
}
