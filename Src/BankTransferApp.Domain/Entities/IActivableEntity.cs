namespace BankTransferApp.Domain.Entities;

public interface IActivableEntity
{
    bool IsActive { get; set; }
}

public static class ActivableEntityExtensions
{
    public static void Activate<T>(this T activableEntity)
        where T : IActivableEntity
    {
        activableEntity.IsActive = true;
    }
    public static void Deactivate<T>(this T activableEntity)
        where T : IActivableEntity
    {
        activableEntity.IsActive = false;
    }
}