using System;
using System.Collections.Generic;

namespace Hotel.App.Model.SYS
{
    public class SysStaffDto
    {
        public int Id { get; set; }
        /// <summary>
        /// user_id
        /// </summary>		
        public string EmployeeNo { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Name { get; set; }
        /// <summary>
        /// mobile
        /// </summary>		
        public string Mobile { get; set; }
        public string Tel { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Title { get; set; }
        public string Address { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string IDCard { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string WebChat { get; set; }
        /// <summary>
        /// org_id
        /// </summary>		
        public int OrgId { get; set; }

        public string OrgIdTxt { get; set; }
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
    }
}

