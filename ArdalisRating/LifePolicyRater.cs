using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating
{
  public class LifePolicyRater : Rater
  {


    public LifePolicyRater(RatingEngine engine, ConsoleLogger logger)
      :base(engine, logger)
    {

    }
    public override void Rate(Policy policy)
    {
      _logger.Log("Rating LIFE policy...");
      _logger.Log("Validating policy.");
      if (isValid(policy))
      {
        var age = GetAge(policy);
        decimal baseRate = GetBaseRate(policy, age);
        _engine.Rating = GetRating(policy, baseRate);
      }
    }
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
        _logger.Log("Life policy must include Date of Birth.");
        results = false;
      }
      if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
      {
        _logger.Log("Centenarians are not eligible for coverage.");
        results = false;
      }
      if (policy.Amount == 0)
      {
        _logger.Log("Life policy must include an Amount.");
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
