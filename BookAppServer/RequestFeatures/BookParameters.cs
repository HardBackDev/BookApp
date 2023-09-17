namespace BookAppServer.RequestFeatures
{
    public class BookParameters : RequestParameters
    {
        public BookParameters() { PageSize = 6; }
        public string? TitleFilter { get; set; } = "";

    }
}
