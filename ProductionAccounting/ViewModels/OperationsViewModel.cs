using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;

namespace ProductionAccounting.ViewModels
{
    public class OperationsViewModel : ViewModel
    {
        private readonly IRepository<Operation> _operationRepository;
        private readonly IUserDialog<OperationModel> _userDialog;
        public OperationsViewModel(IRepository<Operation> repository, IUserDialog<OperationModel> userDialog)
        {
            _operationRepository = repository;
            _userDialog = userDialog;
        }

        public OperationsViewModel()
        { }
    }
}
