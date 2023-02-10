using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;

namespace ProductionAccounting.Services
{
    public class OperationUserDialogService : IUserDialog<OperationModel>
    {
        public bool ConfirmOperation(string info, string caption)
        {
            return false;
        }

        public bool Edit(OperationModel model)
        {
            return false;
        }
    }
}
