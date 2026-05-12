namespace GameHost.Kernel.Abstractions.Dto;

public record InstallerResultBaseDto
{
    public InstallerErrorDto? Error { get; set; }
}
