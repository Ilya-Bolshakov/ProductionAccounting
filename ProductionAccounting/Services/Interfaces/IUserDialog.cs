using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IUserDialog<T> : IUserDialogBase where T : Model
    {
        bool Edit(T model);
    }
}
