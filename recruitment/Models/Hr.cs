namespace recruitment.Models;

public class Hr
{
    public int id { get; set; }
    public string name { get; set; }
    public string department { get; set; }
    public List<Vacancy>? Vacancies { get; set; } = new();
}