namespace GameHost.Kernel.Abstractions.Dto;

public record InstallerResultDataDto<TData> : InstallerResultBaseDto
        where TData : notnull

{
    public TData? Data { get; set; }
}
