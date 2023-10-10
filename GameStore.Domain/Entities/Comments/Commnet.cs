namespace GameStore.Domain.Entities.Comments
{
    internal class Commnet
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public Guid CommentOn { get; set; }//GameId or CommentId ?
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
