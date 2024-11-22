 public class CommentCreateDto
    {
        public string Content { get; set; } = "";
        public int? UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }


    public class CommentUpdateDto
    {
        public string Content { get; set; } = "";
        public int? UserId { get; set; }
        public int PostId { get; set; }
    }