using System;
using System.Collections.Generic;

namespace Hotel.App.Model.SYS
{
    public class sys_role : IEntityBase
    {
        /// <summary>
        /// auto_increment
        /// </summary>		
        public int Id { get; set; }
        /// <summary>
        /// role_name
        /// </summary>		
        public string RoleName { get; set; }
        /// <summary>
        /// role_desc
        /// </summary>		
        public string RoleDesc { get; set; }
        /// <summary>
        /// createdAt
        /// </summary>		
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// updatedAt
        /// </summary>		
        public DateTime UpdatedAt { get; set; }

        public bool IsValid { get; set; }
        public string CreatedBy { get; set; }

    }
}

