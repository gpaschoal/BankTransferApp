namespace BankTransferApp.Domain.Queries;

public class QueryCommand
{
    public QueryCommand()
    {
        if (PageSize <= 0) PageSize = 10;

        if (PageSize >= 50) PageSize = 50;

        if (Page <= 0) Page = 1;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public SortProperty[] Sort { get; set; } = [];
    public SearchProperty[] Search { get; set; } = [];
}
