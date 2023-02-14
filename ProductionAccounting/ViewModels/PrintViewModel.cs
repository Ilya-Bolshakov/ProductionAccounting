using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ProductionAccounting.ViewModels
{
    public class PrintViewModel : ViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
            }
        }

        private List<OperationModel> _operations;
        public List<OperationModel> Operations
        {
            get { return _operations; }
            set
            {
                Set(ref _operations, value);
            }
        }

        private OperationModel _selectedItem;
        public OperationModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
            }
        }

        public PrintViewModel(TabelModel model)
        {
            Operations = new List<OperationModel>(model.Operations);
            Name = model.Name;
        }

        #region Команда вывода на печать
        private ICommand _toPrint;

        public ICommand ToPrint => _toPrint ??= new LambdaCommand(ToPrintExecuted, ToPrintExecute);

        private bool ToPrintExecute() => true;

        private void ToPrintExecuted()
        {
            System.IO.FileStream fs = new FileStream(
                $"Изделие '{Name}' Шаблон.pdf", FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.NORMAL);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddAuthor("Ilya B.");
            document.AddCreator("Product Print");
            document.AddTitle("The document title - PDF creation using iTextSharp");
            document.Open();
            var paragraph = new Paragraph(Name, font);
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);
            var table = new Table(4);
            table.Width = 100;
            float[] widths = new float[] { 15f, 5f, 10f, 70f };
            table.Widths = widths;
            font.Size = 14;

            foreach (var item in Operations)
            {
                var cell = new Cell(new Phrase(item.Name + '\n' + '\n' + '\n', font));
                table.AddCell(cell);
                cell = new Cell(new Phrase(item.Coefficient.CoefficientValue.ToString()));
                table.AddCell(cell);
                cell = new Cell(new Phrase(item.Price.ToString()));
                table.AddCell(cell);
                table.AddCell(new Cell());
            }
            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }
        #endregion
    }
}
