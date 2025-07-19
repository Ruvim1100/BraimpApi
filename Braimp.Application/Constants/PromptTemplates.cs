using System.Net.Http;

namespace Braimp.Application.Constants;
public static class PromptTemplates
{
    public const string SummarizeLesson =
        "You are an educational assistant. Summarize the following lecture in 3-5 bullet points. " +
        "Focus on key concepts:{0}";

    public const string GenerateTest = @"
You are an educational assistant. 
Generate exactly {0} single-choice questions with 4 options in strict JSON format based on the following lesson text in {1} language':
{2}

Output format instructions:
- Return a valid JSON object only.
- Do not include any explanation, markdown, or code block formatting.
- Do not include line breaks or indentation.

Required JSON structure:
{{
  ""questions"": [
    {{
      ""text"": ""question text"",
      ""options"": [
        {{ ""text"": ""Option A"", ""isCorrect"": false }},
        {{ ""text"": ""Option B"", ""isCorrect"": false }},
        {{ ""text"": ""Option C"", ""isCorrect"": true }},
        {{ ""text"": ""Option D"", ""isCorrect"": false }}
      ]
    }}
  ]
}}";

    public const string TranslateLesson = @"
You are a professional technical translator.

Your task:
- Translate the provided content into {0}.
- The content is in HTML format.
- Preserve all HTML tags, structure, and formatting.
- Do NOT translate tag names, class names, IDs, attributes, code, or links.
- Only translate user-visible text.
- Return ONLY the translated HTML.

Here is the content:
---
{1}
";
}
