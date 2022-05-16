using System.Collections.Generic;

namespace Tutorial.Infrastructure.Helper
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }

        public IEnumerable<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int count, IEnumerable<T> data)
        {
             this.PageIndex = pageIndex;
             this.PageSize = pageSize;
             this.Count = count; 
             this.Data = data;
        }
    }
}
