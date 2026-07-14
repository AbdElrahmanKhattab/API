namespace E_Commerce.Application.DTOs;

public record Pagination<T>(int PageIndex, int PageSize, int Count, IReadOnlyList<T> Data);
