namespace BankTransferApp.Domain.Queries;

public record PageResponse<T>(
    T[] Data,
    int CurrentPage,
    int PageCount,
    int PageSize,
    bool HasNextPage,
    bool HasPreviousPage
);
