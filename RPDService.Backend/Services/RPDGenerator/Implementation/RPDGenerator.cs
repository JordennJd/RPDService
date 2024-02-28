using RPDSerice.Models;
using RPDSerice.RPDGenerator.Interfaces;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace RPDSerice.RPDGenerator.Implementation;

public class RPDGenerator : IRPDGenerator
{
    const string templateFilePath ="Services/RPDGenerator/template.docx";
	public Byte[] GetRPDPdfBytes(string JsonRPD)
	{
        JObject data = JObject.Parse(JsonRPD);

        // Открытие документа
        using (WordprocessingDocument doc =
                   WordprocessingDocument.Open(templateFilePath, true))
        {
            var body = doc.MainDocumentPart.Document.Body;

            // Замена плейсхолдеров в тексте
            foreach (var property in data.Properties())
            {
                if (property.Name.StartsWith("{{TEXT"))
                {
                    string placeholder = property.Name;
                    string text = property.Value.ToString();
                    ReplacePlaceholderInText(body, placeholder, text);
                }
                else if (property.Name.StartsWith("{{TABLE"))
                {
                    int underscoreIndex = property.Name.IndexOf('_');
                    if (underscoreIndex != -1 &&
                        underscoreIndex < property.Name.Length - 1)
                    {
                            string tableId = property.Name.Substring(underscoreIndex + 1);
                            JArray rowsData = (JArray)property.Value;
                            AddRowsToTable(body, tableId, rowsData);
                        
                    }
                }
                else if (property.Name.StartsWith("{{ENUM"))
                {
                    JArray itemsData = (JArray)property.Value;
                    InsertEnumerationItemsInText(body, property.Name, itemsData);
                }
            }
        }
        return new Byte[]{0};
    }

    private void ReplacePlaceholderInText(Body body, string placeholder,
                                          string text)
    {
        var paragraphs = body.Elements<Paragraph>();
        foreach (var paragraph in paragraphs)
        {
            foreach (var run in paragraph.Elements<Run>())
            {
                foreach (var textElement in run.Elements<Text>())
                {
                    if (textElement.Text.Contains(placeholder))
                    {
                        textElement.Text = textElement.Text.Replace(placeholder, text);
                    }
                }
            }
        }
    }

    private void AddRowsToTable(Body body, string tableId, JArray rowsData)
    {
        // Находим таблицу с указанным идентификатором
        var table = body.Elements<Table>().FirstOrDefault(
            t => t.Descendants<TableProperties>().Any(
                prop =>
                    prop.TableCaption != null && prop.TableCaption.Val == tableId));
        if (table != null)
        {
            foreach (var rowData in rowsData)
            {
                var row = new TableRow();
                foreach (var cellData in rowData)
                {
                    var cell = new TableCell(
                        new Paragraph(new Run(new Text(cellData.ToString()))));
                    row.Append(cell);
                }
                table.Append(row);
            }
        }
    }

    private void InsertEnumerationItemsInText(Body body, string holder,
                                              JArray itemsData)
    {
        var paragraphs = body.Elements<Paragraph>();
        foreach (var paragraph in paragraphs)
        {
            foreach (var run in paragraph.Elements<Run>())
            {
                foreach (var textElement in run.Elements<Text>())
                {
                    if (textElement.Text.Contains(holder))
                    {
                        var items =
                            string.Join(",\n- ", itemsData.Select(item => item.ToString()));

                        // Заменяем плейсхолдер {{enum}} в тексте на сформированный список
                        // элементов
                        textElement.Text = textElement.Text.Replace(holder, "- " + items);
                    }
                }
            }
        }
       
    }

	}

