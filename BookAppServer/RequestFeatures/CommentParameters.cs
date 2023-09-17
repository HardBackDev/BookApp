namespace BookAppServer.RequestFeatures
{
    public class CommentParameters : RequestParameters
    {
        public CommentParameters() { PageSize = 6; }
        public string? SearchedText { get; set; } = "";
    }
}
