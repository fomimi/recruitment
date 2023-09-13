using Microsoft.AspNetCore.Mvc;
using recruitment.Models;

namespace recruitment.Controllers;

[ApiController]
[Route("[controller]")]
public class HrController
{
    [HttpPost(template: "CreateHr")]
    public bool CreateHr(Hr newHr)
    {
        foreach (var hr in DB.Hrs)
        {
            if (hr.id == newHr.id) return false;
        }
        DB.Hrs.Add(newHr);
        return true;
    }

    [HttpGet(template: "GetHrs")]
    public List<Hr> GetHrs()
    {
        return DB.Hrs;
    }
    
    [HttpGet(template: "GetHr")]
    public Hr? GetHr(int id)
    {
        foreach (var hr in DB.Hrs)
        {
            if (hr.id == id) return hr;
        }
        return null;
    }

    [HttpPatch(template: "EditHr")]
    public void EditHr()
    {
    }
    
    [HttpDelete(template: "DeleteHr")]
    public bool DeleteHr(int id)
    {
        foreach (var hr in DB.Hrs)
        {
            if (hr.id == id)
            {
                DB.Hrs.Remove(hr);
                return true;
            }
        }
        return false;
    }
}