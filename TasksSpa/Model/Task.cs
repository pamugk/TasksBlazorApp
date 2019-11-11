namespace TasksSpa.Model
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFinished{ get; set; }
    }
}
