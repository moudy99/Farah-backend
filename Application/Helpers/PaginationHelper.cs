using Application.DTOS;

namespace Application.Helpers
{
    public static class PaginationHelper
    {

        public static PaginatedList<T> Paginate<T>(List<T> items, int page, int pageSize)
        {
            var totalItems = items.Count;
            var paginatedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(paginatedItems, totalItems, page, pageSize);
        }

        public static PaginationInfoDTO GetPaginationInfo<T>(PaginatedList<T> paginatedList)
        {
            return new PaginationInfoDTO
            {
                TotalItems = paginatedList.TotalItems,
                TotalPages = paginatedList.TotalPages,
                CurrentPage = paginatedList.CurrentPage,
                StartPage = paginatedList.StartPage,
                EndPage = paginatedList.EndPage
            };
        }
    }
}
