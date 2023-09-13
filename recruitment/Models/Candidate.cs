namespace recruitment.Models;

public class Candidate
{
    public int id { get; set; }
    public string name { get; set; }
    public DateOnly birthDate { get; set; }
    public string phoneNumber { get; set; }
    public string resume { get; set; }
    public int interviewStage { get; set; }
    public bool testTaskCompleted { get; set; }
}