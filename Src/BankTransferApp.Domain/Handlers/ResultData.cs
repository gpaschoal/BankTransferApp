namespace BankTransferApp.Domain.Handlers;

public class ResultData
{
    private readonly IDictionary<string, IList<string>> _errors;

    public ResultData()
    {
        _errors = new Dictionary<string, IList<string>>();
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

public class ResultData<T>
    : ResultData where T : new()
{
    public ResultData()
    {
        Data = new T();
    }

    public ResultData(T data)
    {
        Data = data;
    }

    public T Data { get; }
}