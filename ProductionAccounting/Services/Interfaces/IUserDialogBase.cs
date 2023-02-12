namespace ProductionAccounting.Services.Interfaces
{
    public interface IUserDialogBase
    {
        bool ConfirmOperation(string info, string caption);
    }
}
