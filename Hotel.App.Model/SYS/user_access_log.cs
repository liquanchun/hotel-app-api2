using System;
namespace Hotel.App.Model.SYS
{
	public class user_access_log : IEntityBase
    {   		     
      	/// <summary>
		/// auto_increment
        /// </summary>		
		        public int Id { get; set; }     
		/// <summary>
		/// user_id
        /// </summary>		
                public string UserId{ get; set; }     
		/// <summary>
		/// menu_id
        /// </summary>		
                public int MenuId{ get; set; }     
		/// <summary>
		/// function_id
        /// </summary>		
                public int FunctionId{ get; set; }     
		/// <summary>
		/// createdAt
        /// </summary>		
                public DateTime CreatedAt{ get; set; }     
		/// <summary>
		/// updatedAt
        /// </summary>		
                public DateTime UpdatedAt{ get; set; }     
		/// <summary>
		/// desc
        /// </summary>		
                public string Desc{ get; set; }     
		   
	}
}

