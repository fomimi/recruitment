using recruitment;
using recruitment.Controllers;
using recruitment.Models;

namespace recruitmentTest;

public class VacancyTest
{
    [SetUp]
    public void Setup()
    {
        DB.Hrs = new();
        Hr newHr = new Hr()
        {
            id = 0,
            name = "Ivan Ivanov",
            department = "Analytics",
        };
        DB.Hrs.Add(newHr);
    }

    [Test]
    public void CreateVacancy()
    {
        VacancyController vacController = new VacancyController();
        Vacancy newVac = new Vacancy()
        {
            id = 1,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        };
        bool expected = true;
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        bool actual = vacController.CreateVacancy(hr.id,newVac);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CreateVacancyWithCandidate()
    {
        VacancyController vacController = new VacancyController();
        Vacancy newVac = new Vacancy()
        {
            id = 1,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        };
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        newVac.Candidates.Add(new Candidate()
        {
            id = 0,
            name = "Alex Alex",
            birthDate = new DateOnly(1990, 12, 12),
            phoneNumber = "+79990001122",
            resume = "ok",
            interviewStage = 0,
            testTaskCompleted = false
        });
        bool expected = true;
        bool actual = vacController.CreateVacancy(hr.id,newVac);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CreateVacancyWithSameId()
    {
        VacancyController vacController = new VacancyController();
        Vacancy newVac = new Vacancy()
        {
            id = 1,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        };
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        vacController.CreateVacancy(hr.id,newVac);
        bool expected = false;
        bool actual = vacController.CreateVacancy(hr.id,newVac);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetVacancy()
    {
        VacancyController vacController = new VacancyController();
        Vacancy newVac = new Vacancy()
        {
            id = 1,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        };
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        vacController.CreateVacancy(hr.id, newVac);
        Vacancy expected = newVac;
        Vacancy? actual = vacController.GetVacancy(hr.id, newVac.id);
        Assert.NotNull(actual);
        Assert.That(actual.id, Is.EqualTo(expected.id));
        Assert.That(actual.name, Is.EqualTo(expected.name));
        Assert.That(actual.department, Is.EqualTo(expected.department));
        Assert.That(actual.openingDate, Is.EqualTo(expected.openingDate));
        Assert.That(actual.closingDate, Is.EqualTo(expected.closingDate));
        Assert.That(actual.isClosed, Is.EqualTo(expected.isClosed));
        Assert.That(actual.testTask, Is.EqualTo(expected.testTask));
    }

    [Test]
    public void GetVacancyIfVacancyIsNull()
    {
        VacancyController vacController = new VacancyController();
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        Vacancy? expected = null;
        Vacancy? actual = vacController.GetVacancy(hr.id, 3);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetVacancyIfHrIsNull()
    {
        VacancyController vacController = new VacancyController();
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 1);
        Assert.Null(hr);
        Vacancy? expected = null;
        Vacancy? actual = vacController.GetVacancy(1, 0);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ClosedVacancy()
    {
        VacancyController vacController = new VacancyController();
        Vacancy newVac = new Vacancy()
        {
            id = 1,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        };
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        vacController.CreateVacancy(hr.id, newVac);
        vacController.SetIsCLosed(hr.id, newVac.id);
        bool expected = true;
        bool actual = false;
        foreach (var vac in hr.Vacancies)
        {
            if (vac.id == newVac.id) actual = vac.isClosed;
        }
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ClosedVacancyIfVacancyNotExists()
    {
        VacancyController vacController = new VacancyController();
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        bool expected = false;
        bool actual = vacController.SetIsCLosed(hr.id, 3);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ClosingDate()
    {
        VacancyController vacController = new VacancyController();
        Vacancy newVac = new Vacancy()
        {
            id = 1,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        };
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        vacController.CreateVacancy(hr.id, newVac);
        vacController.SetIsCLosed(hr.id, newVac.id);
        DateOnly expected = new DateOnly(2023, 09, 13);
        DateOnly? actual = new DateOnly();
        foreach (var vac in hr.Vacancies)
        {
            if (vac.id == newVac.id) actual = vac.closingDate;
        }
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void DeleteVacancy()
    {
        VacancyController vacController = new VacancyController();
        Vacancy newVac = new Vacancy()
        {
            id = 1,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        };
        Hr? hr = DB.Hrs.FirstOrDefault(x => x.id == 0);
        vacController.CreateVacancy(hr.id, newVac);
        bool expected = true;
        bool actual = vacController.DeleteVacancy(hr.id, newVac.id);
        Assert.That(actual, Is.EqualTo(expected));
    }
}