namespace BankTransferApp.Domain.Queries;

public record SortProperty(string Property, ESortType? SortType = ESortType.Descending);
