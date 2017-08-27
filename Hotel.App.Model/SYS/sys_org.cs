using System;
using System.Collections.Generic;

namespace Hotel.App.Model.SYS
{
    public class sys_org : IEntityBase
    {
        /// <summary>
        /// auto_increment
        /// </summary>		
        public int Id { get; set; }
        /// <summary>
        /// dept_name
        /// </summary>		
        public string DeptName { get; set; }
        /// <summary>
        /// parent_org_id
        /// </summary>		
        public int ParentId { get; set; }
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

