using System;
using System.Text;
using System.Collections.Generic;
namespace Hotel.App.Model.SYS
{
    
    public class sys_function : IEntityBase
    {
        /// <summary>
        /// auto_increment
        /// </summary>		
        public int Id { get; set; }
        /// <summary>
        /// function_name
        /// </summary>		
        public string FunctionName { get; set; }
        /// <summary>
        /// function_addr
        /// </summary>		
        public string FunctionAddr { get; set; }
        /// <summary>
        /// component
        /// </summary>		
        public string Component { get; set; }
        /// <summary>
        /// menu_id
        /// </summary>		
        public int MenuId { get; set; }
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

