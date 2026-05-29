namespace Globomantics.Client.Models;
public class ConferenceModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public int AttendeeCount { get; set; }
}
