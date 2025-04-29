using System.IO;

namespace Braimp.Application.Constants;
public static class PromptTemplates
{
    public const string Summarize =
        "You are an educational assistant. Summarize the following lecture in 3-5 bullet points. " +
        "Focus on key concepts:{0}";

    public const string GenerateTest = "You are an educational assistant. " +
    "Generate exactly 5 multiple-choice questions with 3 option in strict JSON format based on the following lesson:" +
    "{0}" +    "Required JSON structure:" +    "{{" +    "  \"questions\": [" +    "    {{" +    "      \"text\": \"question text\"," +    "      \"options\": [" +    "        {{ \"text\": \"option1\", \"isCorrect\": false }}," +    "        {{ \"text\": \"option2\", \"isCorrect\": true }}" +    "      ]" +    "    }}" +    "  ]" +    "}}" +    "Rules:" +    "1. Each question must have exactly one correct option (\"isCorrect\": true)." +    "2. Do not use markdown formatting (e.g., ```json)." +    "3. The output must be strictly valid JSON with no extra text before or after." +    "4. Do not wrap the JSON in a string; output the JSON object directly." +
    "5. Optionally, you may output JSON in compact form (no newlines or indentation).";
}
