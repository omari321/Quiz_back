using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Paging
{
    public class PageReturnDto<T>
    {
        public IEnumerable<T> items { get; set; }
        public PageInfo pageInfo { get; set; }
        public PageReturnDto()
        {

        }
        public PageReturnDto(IEnumerable<T> items, int count, int pageNumber, int postsPerPage)
        {
            pageInfo = new PageInfo
            {
                CurrentPage = pageNumber,
                ItemsPerPage = postsPerPage,
                TotalPages = (int)Math.Ceiling(count / (double)postsPerPage),
                TotalPosts = count
            };
            this.items = items;
            this.pageInfo = pageInfo;
        }
    }
}
