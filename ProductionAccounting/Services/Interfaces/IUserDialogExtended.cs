using ProductionAccounting.DAL.Entities.Base;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IUserDialogWithRepository<T, TRepo> : IUserDialogBase where T : Model where TRepo : Entity, new()
    {
        public bool Edit(T model, IRepository<TRepo> repository);
    }
}
