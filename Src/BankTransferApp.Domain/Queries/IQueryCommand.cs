namespace BankTransferApp.Domain.Queries;

public class IQueryCommand
{
    public IQueryCommand()
    {
        if (PageSize <= 0) PageSize = 10;

        if (PageSize >= 50) PageSize = 50;

        if (Page <= 0) Page = 1;
    }

    public int Page { get; init; }
    public int PageSize { get; set; }
    public SortProperty[] Sort { get; set; } = [];
    public SearchProperty[] Search { get; set; } = [];
}
