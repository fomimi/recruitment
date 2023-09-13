namespace recruitment.Models;
public enum interviewStages
{
    HrInterview,
    TestTask,
    TechInterview,
    Offer,
    TestPeriod
}
public class Vacancy
{
    public int id { get; set; }
    public string name { get; set; }
    public string department { get; set; }
    public DateOnly openingDate { get; set; }
    public DateOnly? closingDate { get; set; }

    public bool testTask { get; set; }  //предполагается ли тестовое задание в вакансии или нет 
    public bool isClosed { get; set; }  //закрыта вакансия или нет
    public List<Candidate>? Candidates { get; set; } = new();

}