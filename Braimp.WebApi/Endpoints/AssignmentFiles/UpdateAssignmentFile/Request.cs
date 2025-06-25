namespace Braimp.WebApi.Endpoints.AssignmentFiles.UpdateAssignmentFile;
using System.ComponentModel.DataAnnotations;

public record Request([Required, MaxLength(100)] string Name);