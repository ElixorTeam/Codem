﻿using System.ComponentModel.DataAnnotations;
using Сodem.Shared.Enums;

namespace WebClient.Models;

public sealed class CodeFileModel
{ 
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    
    [RegularExpression(@"^[\w\-.]+(\.\w+)$")]
    public string Title { get; set; } = "new_file.txt";
    public ProgrammingLanguage Language { get; set; } = ProgrammingLanguage.Markdown;
}