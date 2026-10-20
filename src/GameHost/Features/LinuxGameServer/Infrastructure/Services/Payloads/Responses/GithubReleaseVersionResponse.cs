using Octokit;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses;

internal sealed record GithubReleaseVersionResponse
{
    public Release Release { get; init; } = default!;
    public Version Version { get; init; } = default!;
}
