
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RPDSerice.RPDGenerator.DocumentHelper
{
    public static class WordDocumentExtensions
    {
        public static void ReplacePlaceholders(WordprocessingDocument doc, string placeholder, string replacement)
        {
            var body = doc.MainDocumentPart.Document.Body;

            // Обработка всех параграфов
            foreach (var paragraph in body.Elements<Paragraph>())
            {
                ReplacePlaceholderInParagraph(paragraph, placeholder, replacement);
            }

            // Обработка всех заголовков
            foreach (var headerPart in doc.MainDocumentPart.HeaderParts)
            {
                foreach (var paragraph in headerPart.RootElement.Elements<Paragraph>())
                {
                    ReplacePlaceholderInParagraph(paragraph, placeholder, replacement);
                }
            }

            // Обработка всех подвалов
            foreach (var footerPart in doc.MainDocumentPart.FooterParts)
            {
                foreach (var paragraph in footerPart.RootElement.Elements<Paragraph>())
                {
                    ReplacePlaceholderInParagraph(paragraph, placeholder, replacement);
                }
            }
        }

        private static void ReplacePlaceholderInParagraph(Paragraph paragraph, string placeholder, string replacement)
        {
            // Собираем весь текст параграфа, чтобы проверить, содержит ли он плейсхолдер
            string fullText = paragraph.InnerText;

            if (fullText.Contains(placeholder))
            {
                // Если плейсхолдер присутствует в параграфе, начинаем замену
                var runs = paragraph.Elements<Run>().ToList();
                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    var textElements = run.Elements<Text>().ToList();
                    foreach (var textElement in textElements)
                    {
                        // Проверяем, содержит ли Text элемент часть или весь плейсхолдер
                        if (textElement.Text.Contains(placeholder))
                        {
                            // Простая замена внутри одного Text элемента
                            textElement.Text = textElement.Text.Replace(placeholder, replacement);
                        }
                        else
                        {
                            // Сложная замена, если плейсхолдер разделен на несколько элементов
                            // Реконструируем текст с учетом следующих Run элементов
                            if (fullText.Substring(textElement.Text.Length).Contains(placeholder))
                            {
                                string tempText = textElement.Text;
                                int j = i + 1;
                                while (j < runs.Count && !tempText.Contains(placeholder))
                                {
                                    var nextRun = runs[j];
                                    var nextTextElement = nextRun.Elements<Text>().FirstOrDefault();
                                    if (nextTextElement != null)
                                    {
                                        tempText += nextTextElement.Text;
                                        j++;
                                    }
                                }

                                // Замена в собранном тексте
                                string replacedText = tempText.Replace(placeholder, replacement);

                                // Обновляем текущий и следующие Text элементы
                                textElement.Text = replacedText.Substring(0, textElement.Text.Length);
                                int replacedIndex = textElement.Text.Length;
                                for (int k = i + 1; k < j; k++)
                                {
                                    var nextRun = runs[k];
                                    var nextTextElements = nextRun.Elements<Text>().ToList();
                                    foreach (var nextTextElement in nextTextElements)
                                    {
                                        int length = nextTextElement.Text.Length;
                                        nextTextElement.Text = replacedIndex + length <= replacedText.Length
                                            ? replacedText.Substring(replacedIndex, length)
                                            : string.Empty;
                                        replacedIndex += length;
                                    }
                                }

                                // После замены выходим из цикла
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

