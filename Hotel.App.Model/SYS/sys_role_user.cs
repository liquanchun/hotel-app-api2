namespace Hotel.App.Model.SYS
{
    public class sys_role_user : IEntityBase
    {
        /// <summary>
        /// auto_increment
        /// </summary>		
        public int Id { get; set; }
        /// <summary>
        /// role_id
        /// </summary>		
        public int RoleId { get; set; }
        /// <summary>
        /// user_id
        /// </summary>		
        public int UserId { get; set; }

    }
}

