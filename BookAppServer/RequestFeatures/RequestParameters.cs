namespace BookAppServer.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 40;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 9;
        public int PageSize 
        { 
            get { return _pageSize; } 
            set 
            { 
                _pageSize = value > maxPageSize ? maxPageSize : value <= 0? _pageSize: value;
            }
        }
    }
}
