namespace BankTransferApp.Domain.Handlers;

public class Result
{
    private readonly IDictionary<string, IList<string>> _errors;

    public Result()
    {
        _errors = new Dictionary<string, IList<string>>();
    }

    public Result(string key, string errorMessage) : this()
    {
        AddError(key, errorMessage);
    }

    public bool IsValid => !_errors.Any();

    public IReadOnlyDictionary<string, IList<string>> Errors => _errors.ToDictionary(kv => kv.Key, kv => kv.Value);

    public void AddError(string key, string errorMessage)
    {
        if (!_errors.ContainsKey(key))
            _errors.Add(key, []);

        _errors[key].Add(errorMessage);
    }
}

public class Result<T>
    : Result where T : new()
{
    public Result() : base()
    {
        Data = new T();
    }

    public Result(T data) : base()
    {
        Data = data;
    }

    public Result(string key, string errorMessage) : base(key, errorMessage)
    { }

    public T Data { get; }
}