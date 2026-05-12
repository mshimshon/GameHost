namespace GameHost.Kernel.Abstractions.Dto;

public record InstallerErrorDto
{
    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
}
