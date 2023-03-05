using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductionAccounting.Models;
using System;
using System.IO;

namespace ProductionAccounting.Services
{
    public class PdfCreator : IDisposable
    {
        private FileStream _stream;
        private Document _document;
        private PdfWriter _writer;
        private bool _disposed;

        public TabelModel TabelModel { get; set; }

        public PdfCreator(TabelModel tabelModel)
        {
            TabelModel = tabelModel;
        }

        public string CreatePdf()
        {
            try
            {
                string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var font = new Font(baseFont, 18, Font.NORMAL);

                _stream = GetNewPdfStream();
                _document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);

                _writer = PdfWriter.GetInstance(_document, _stream);
                _document.AddAuthor("Ilya B.");
                _document.AddCreator("Product Print");
                _document.AddTitle("The document title - PDF creation using iTextSharp");
                _document.Open();
                var paragraph = new Paragraph(TabelModel.Name, font);
                paragraph.Alignment = Element.ALIGN_CENTER;
                _document.Add(paragraph);
                var table = new Table(5);
                table.Width = 100;
                float[] widths = new float[] { 15f, 5f, 10f, 65f, 5f };
                table.Widths = widths;
                font.Size = 14;

                var cell = new Cell(new Phrase("Название операции", font));
                table.AddCell(cell);
                cell = new Cell(new Phrase("Коэффициент", font));
                table.AddCell(cell);
                cell = new Cell(new Phrase("Цена", font));
                table.AddCell(cell);
                cell = new Cell(new Phrase("Количество", font));
                table.AddCell(cell);
                cell = new Cell(new Phrase("Итого", font));
                table.AddCell(cell);

                foreach (var item in TabelModel.Operations)
                {
                    cell = new Cell(new Phrase(item.Name + '\n' + '\n' + '\n', font));
                    table.AddCell(cell);
                    cell = new Cell(new Phrase(item.Coefficient.CoefficientValue.ToString()));
                    table.AddCell(cell);
                    cell = new Cell(new Phrase(item.Price.ToString()));
                    table.AddCell(cell);
                    table.AddCell(new Cell());
                    table.AddCell(new Cell());
                }
                _document.Add(table);
                return _stream.Name;
            }
            catch (Exception)
            {
                Dispose();
                if (File.Exists(_stream.Name))
                    File.Delete(_stream.Name);
                throw;
            }
        }

        private FileStream GetNewPdfStream()
        {
            
            var fileName = $"Изделие '{TabelModel.Name}' Шаблон.pdf";
            IConfiguration config = App.Host.Services.GetRequiredService<IConfiguration>();
            var filePath = Path.Combine(config["SavePdfsFolder"], fileName);
            return new FileStream(
                filePath, FileMode.Create);
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _document.Dispose();
                    _writer.Dispose();
                    _stream.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
