﻿using Finance.Domain.Entities;
using Finance.Domain.Enums;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task<bool> CheckAccountAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CategoryEntity>> FindSubcategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default);

        Task<SearchResult<CategoryEntity>> SearchAsync(
            bool? active,
            CategoryType? categoryType,
            string? name,
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder? order,
            CancellationToken cancellationToken = default);
    }
}
