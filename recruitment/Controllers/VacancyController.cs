using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using recruitment.Models;

namespace recruitment.Controllers;

[ApiController]
[Route("[controller]")]
public class VacancyController
{
    private Hr? GetHrById(int hrId)
    {
        foreach (var hr in DB.Hrs)
        {
            if (hr.id == hrId) return hr;
        }
        return null;
    }

    private DateOnly GetTodayDateTime()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        return today;
    }
    
    [HttpPost(template: "CreateVacancy")] 
    public bool CreateVacancy(int hrId, Vacancy newVacancy)
    {
        Hr? existingHr = GetHrById(hrId);
        if (existingHr == null) return false;
        foreach (var vacancy in existingHr.Vacancies)
        {
            if (vacancy.id == newVacancy.id) return false;
        }
        existingHr.Vacancies.Add(newVacancy);
        return true;
    }

    [HttpGet(template: "GetVacancy")]
    public Vacancy? GetVacancy(int hrId, int vacancyId)
    {
        Hr? existingHr = GetHrById(hrId);
        if (existingHr == null) return null;
        foreach (var vacancy in existingHr.Vacancies)
        {
            if (vacancy.id == vacancyId) return vacancy;
        }
        return null;
    }

    [HttpPatch(template: "SetIsClosed")]
    public bool SetIsCLosed(int hrId, int vacancyId)
    {
        Hr? existingHr = GetHrById(hrId);
        if (existingHr == null) return false;
        foreach (var vacancy in existingHr.Vacancies)
        {
            if (vacancy.id == vacancyId)
            {
                vacancy.isClosed = true;
                SetClosingDate(existingHr.id, vacancy.id);
                return true;
            }
        }
        return false;
    }
    
    [HttpPatch(template: "SetClosingDate")]
    public void SetClosingDate(int hrId, int vacancyId)
    {
        Hr? existingHr = GetHrById(hrId);
        if (existingHr == null) return;
        foreach (var vacancy in existingHr.Vacancies)
        {
            if (vacancy.id == vacancyId)
            {
                if (vacancy.isClosed) vacancy.closingDate = GetTodayDateTime();
            }
        }
    }
    
    [HttpDelete(template: "DeleteVacancy")]
    public bool DeleteVacancy(int hrId, int vacancyId)
    {
        foreach (var hr in DB.Hrs)
        {
            if (hr.id == hrId)
            {
                foreach (var vacancy in hr.Vacancies)
                {
                    if (vacancy.id == vacancyId)
                    {
                        hr.Vacancies.Remove(vacancy);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}