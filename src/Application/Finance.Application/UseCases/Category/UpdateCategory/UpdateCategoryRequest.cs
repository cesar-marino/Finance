﻿using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Enums;
using MediatR;

namespace Finance.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryRequest(
        Guid userId,
        Guid categoryId,
        bool active,
        CategoryType categoryType,
        string name,
        string? icon,
        string? color) : IRequest<CategoryResponse>
    {
        public Guid UserId { get; } = userId;
        public Guid CategoryId { get; } = categoryId;
        public bool Active { get; } = active;
        public CategoryType CategoryType { get; } = categoryType;
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
    }
}
