using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating
{
  public class LifePolicyHolder
  {
    public ConsoleLogger Logger { get; set; } = new ConsoleLogger();
    public int GetAge(Policy policy)
    {
      int age = DateTime.Today.Year - policy.DateOfBirth.Year;

      if (policy.DateOfBirth.Month == DateTime.Today.Month &&
          DateTime.Today.Day < policy.DateOfBirth.Day ||
          DateTime.Today.Month < policy.DateOfBirth.Month)
      {
        age--;
      }
      return age;  
    }
    public bool isValid(Policy policy)
    {
      var results = true;
      if (policy.DateOfBirth == DateTime.MinValue)
      {
        Logger.Log("Life policy must include Date of Birth.");
        results = false;
      }
      if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
      {
        Logger.Log("Centenarians are not eligible for coverage.");
        results = false;
      }
      if (policy.Amount == 0)
      {
        Logger.Log("Life policy must include an Amount.");
        results = false;
      }
      return results;
    }
    public decimal GetBaseRate(Policy policy, int age)
    {
      return policy.Amount * age / 200;

    }
    public decimal GetRating(Policy policy, decimal baseRate)
    {
      if (policy.IsSmoker)
      {
        return baseRate * 2;        
      }
      return  baseRate;
    }
  }
}
