using System;
using System.Collections.Generic;

namespace Hotel.App.Model.SYS
{
    public class SysUserDto
    {
        public int Id { get; set; }
        /// <summary>
        /// user_id
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// user_name
        /// </summary>		
        public string UserName { get; set; }
        /// <summary>
        /// mobile
        /// </summary>		
        public string Mobile { get; set; }
        public string WebChat { get; set; }
       
        /// <summary>
        /// pwd
        /// </summary>		
        public string Pwd { get; set; }
        /// <summary>
        /// last_login_time
        /// </summary>		
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// updatedAt
        /// </summary>		
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// createdAt
        /// </summary>		
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// isvalid
        /// </summary>		
        public bool IsValid { get; set; }
        public bool IsDelete { get; set; }
        public string RoleIds { get; set; }

        public string RoleNames { get; set; }
    }
}

