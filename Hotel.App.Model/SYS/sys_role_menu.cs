namespace Hotel.App.Model.SYS
{
	public class sys_role_menu : IEntityBase
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
		/// menu_id
        /// </summary>		
                public int MenuId{ get; set; }     
		   
	}
}

