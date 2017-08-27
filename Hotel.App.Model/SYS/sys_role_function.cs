namespace Hotel.App.Model.SYS
{
	public class sys_role_function : IEntityBase
    {   		     
      	/// <summary>
		/// auto_increment
        /// </summary>		
		        public int Id { get; set; }     
		/// <summary>
		/// role_id
        /// </summary>		
                public int RoleId{ get; set; }     
		/// <summary>
		/// function_id
        /// </summary>		
                public int FunctionId{ get; set; }     
		   
	}
}

