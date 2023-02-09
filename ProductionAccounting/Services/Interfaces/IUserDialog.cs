using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IUserDialog<T> where T : Model
    {
        bool Edit(T model);

        bool ConfirmOperation(string info, string caption);
    }
}
