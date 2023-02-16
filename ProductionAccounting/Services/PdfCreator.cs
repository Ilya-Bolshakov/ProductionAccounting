﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using ProductionAccounting.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services
{
    public class PdfCreator : IDisposable
    {
        private FileStream _stream;
        private Document _document;
        private PdfWriter _writer;
        private bool _disposed;

        public string Name { get; set; }

        public PdfCreator(string name)
        {
            Name = name;
            var fileName = $"Изделие '{Name}' Шаблон.pdf";
            _stream = new FileStream(
                fileName, FileMode.Create);
            _document = new Document(PageSize.A4, 25, 25, 30, 30);
        }

        public void CreatePdf(List<OperationModel> operations)
        {
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new Font(baseFont, 18, Font.NORMAL);

            _writer = PdfWriter.GetInstance(_document, _stream);
            _document.AddAuthor("Ilya B.");
            _document.AddCreator("Product Print");
            _document.AddTitle("The document title - PDF creation using iTextSharp");
            _document.Open();
            var paragraph = new Paragraph(Name, font);
            paragraph.Alignment = Element.ALIGN_CENTER;
            _document.Add(paragraph);
            var table = new Table(4);
            table.Width = 100;
            float[] widths = new float[] { 15f, 5f, 10f, 70f };
            table.Widths = widths;
            font.Size = 14;

            foreach (var item in operations)
            {
                var cell = new Cell(new Phrase(item.Name + '\n' + '\n' + '\n', font));
                table.AddCell(cell);
                cell = new Cell(new Phrase(item.Coefficient.CoefficientValue.ToString()));
                table.AddCell(cell);
                cell = new Cell(new Phrase(item.Price.ToString()));
                table.AddCell(cell);
                table.AddCell(new Cell());
            }
            _document.Add(table);
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (File.Exists(_stream.Name))
                        File.Delete(_stream.Name);
                    _document.Dispose();
                    _writer.Dispose();
                    _stream.Dispose();
                }

                _disposed = true;
            }
        }
    }
}