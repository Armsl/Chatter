using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatterSite.Models
{
    public class Message
    {
        #region Constructor
        //The date is created automatically to control the flow of messages
        public Message()
        {
            CurrentTime = DateTime.Now;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CurrentTime { get; set; }
        public string UserID { get; set; }
        public virtual User UserV { get; set; }
        #endregion
    }
}
