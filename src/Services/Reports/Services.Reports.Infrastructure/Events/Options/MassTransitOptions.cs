namespace Services.Reports.Infrastructure.Events.Options;

public class MassTransitOptions
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}