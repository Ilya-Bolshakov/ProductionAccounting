using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductionAccounting.Models
{
    public class ProductModel : Model
    {
        private List<OperationModel> _operations;
        public List<OperationModel> Operations
        {
            get { return _operations; }
            set { _operations = value; OnPropertyChanged(nameof(Operations)); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public ProductModel()
        {
        }

        public ProductModel(Product orm)
        {
            Operations = orm.Operations.Select(o => new OperationModel(o)).ToList();
            Name = orm.Name;
            Id = orm.Id;
        }

        public Product MapToOrm()
        {
            return new Product()
            {
                Name = Name,
                Id = Id,
                Operations = Operations.Select(o => o.MapToOrm()).ToList()
            };
        }

        public override object Clone()
        {
            var model = new ProductModel();
            model.Name = Name;
            Operations = new List<OperationModel>(Operations);
            return model;
        }
    }
}
