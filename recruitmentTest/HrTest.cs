using NUnit.Framework.Internal;
using recruitment;
using recruitment.Controllers;
using recruitment.Models;

namespace recruitmentTest;

public class HrTests
{
    [SetUp]
    public void Setup()
    {
        DB.Hrs = new();
    }

    [Test]
    public void CreateHr()
    {
        HrController hrController = new HrController();
        Hr newHr = new Hr()
        {
            id = 0,
            name = "Ivan Ivanov",
            department = "Analytics"
        };
        bool expected = true;
        bool actual = hrController.CreateHr(newHr);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CreateHrWithVacancy()
    {
        HrController hrController = new HrController();
        Hr newHr = new Hr()
        {
            id = 0,
            name = "Ivan Ivanov",
            department = "Analytics",
            
        };
        newHr.Vacancies.Add(new Vacancy()
        {
            id = 0,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        });
        bool expected = true;
        bool actual = hrController.CreateHr(newHr);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void CreateHrWithVacancyAndCandidate()
    {
        HrController hrController = new HrController();
        Hr newHr = new Hr()
        {
            id = 0,
            name = "Ivan Ivanov",
            department = "Analytics",
            
        };
        newHr.Vacancies.Add(new Vacancy()
        {
            id = 0,
            name = "Analyst",
            department = "Analytics",
            openingDate = new DateOnly(2023, 09, 12),
            closingDate = null,
            isClosed = false,
            testTask = true
        });
        Vacancy? vacancy = newHr.Vacancies.FirstOrDefault(x=>x.id == 0);
        if (vacancy != null)
        {
            vacancy.Candidates.Add(new Candidate()
            {
                id = 0,
                name = "Alex Alex",
                birthDate = new DateOnly(1990, 12, 12),
                phoneNumber = "+79990001122",
                resume = "ok",
                interviewStage = 0,
                testTaskCompleted = false
            });
        }
        bool expected = true;
        bool actual = hrController.CreateHr(newHr);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CreateHrWithSameId()
    {
        HrController hrController = new HrController();
        Hr newHr = new Hr()
        {
            id = 0,
            name = "Ivan Ivanov",
            department = "Analytics"
        };
        hrController.CreateHr(newHr);
        bool expected = false;
        bool actual = hrController.CreateHr(newHr);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetListOfHrs()
    {
        HrController hrController = new HrController();
        List<Hr> expected = DB.Hrs;
        List<Hr> actual = hrController.GetHrs();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetHr()
    {
        HrController hrController = new HrController();
        Hr newHr = new Hr()
        {
            id = 0,
            name = "Ivan Ivanov",
            department = "Analytics"
        };
        hrController.CreateHr(newHr);
        Hr expected = newHr;
        Hr? actual = hrController.GetHr(newHr.id);
        Assert.NotNull(actual);
        Assert.That(actual.id, Is.EqualTo(expected.id));
        Assert.That(actual.name, Is.EqualTo(expected.name));
        Assert.That(actual.department, Is.EqualTo(expected.department));
    }
    
    [Test]
    public void GetHrIfHrIsNull()
    {
        HrController hrController = new HrController();
        Hr? expected = null;
        Hr? actual = hrController.GetHr(1);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DeleteHr()
    {
        HrController hrController = new HrController();
        Hr newHr = new Hr()
        {
            id = 0,
            name = "Ivan Ivanov",
            department = "Analytics"
        };
        hrController.CreateHr(newHr);
        bool expected = true;
        bool actual = hrController.DeleteHr(newHr.id);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DeleteHrIfHrNotExists()
    {
        HrController hrController = new HrController();
        bool expected = false;
        bool actual = hrController.DeleteHr(2);
        Assert.That(actual, Is.EqualTo(expected));
    }
}