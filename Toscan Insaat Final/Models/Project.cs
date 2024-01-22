namespace Toscan_Insaat_Final.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //public static implicit operator Project(Project v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
