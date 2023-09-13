using Microsoft.AspNetCore.Mvc;
using recruitment.Models;

namespace recruitment.Controllers;

[ApiController]
[Route("[controller]")]
public class CandidateController
{
    private Vacancy? getVacancy(int hrId, int vacancyId)
    {
        foreach (var hr in DB.Hrs)
        {
            if (hr.id == hrId)
            {
                foreach (var vacancy in hr.Vacancies)
                {
                    if (vacancy.id == vacancyId) return vacancy;
                }
            }
        }
        return null;
    }
    
    [HttpPost(template: "CreateCandidate")] 
    public bool CreateCandidate(int hrId, int vacancyId, Candidate newCandidate)
    {
        Vacancy? vacancy = getVacancy(hrId, vacancyId);
        if (vacancy == null) return false;
        foreach (var candidate in vacancy.Candidates)
        {
            if (candidate.id == newCandidate.id) return false;
        }
        vacancy.Candidates.Add(newCandidate);
        return true;
    }

    [HttpGet(template: "GetCandidate")]
    public Candidate? GetCandidate(int hrId, int vacancyId, int candidateId)
    {
        Vacancy? vacancy = getVacancy(hrId, vacancyId);
        if (vacancy == null) return null;
        foreach (var candidate in vacancy.Candidates)
        {
            if (candidate.id == candidateId) return candidate;
        }
        return null;
    }
    
    [HttpPatch(template: "SetInterviewStage")]
    public void SetInterviewStage(int hrId, int vacancyId, int candidateId, int stageId)
    {
        Vacancy? vacancy = getVacancy(hrId, vacancyId);
        if (vacancy == null) return;
        foreach (var candidate in vacancy.Candidates)
        {
            if (candidate.id == candidateId) candidate.interviewStage = stageId;
        }
    }

    [HttpPatch(template: "SetTaskCompleted")]
    public bool SetTaskCompleted(int hrId, int vacancyId, int candidateId)
    {
        Vacancy? vacancy = getVacancy(hrId, vacancyId);
        if (vacancy == null) return false;
        foreach (var candidate in vacancy.Candidates)
        {
            if (candidate.id == candidateId)
            {
                candidate.testTaskCompleted = true;
                return true;
            }
        }
        return false;
    }

    [HttpDelete(template: "DeleteCandidate")]
    public bool DeleteCandidate(int hrId, int vacancyId, int candidateId)
    {
        Vacancy? vacancy = getVacancy(hrId, vacancyId);
        if (vacancy == null) return false;
        foreach (var candidate in vacancy.Candidates)
        {
            if (candidate.id == candidateId)
            {
                vacancy.Candidates.Remove(candidate);
                return true;
            }
        }
        return false;
    }
}