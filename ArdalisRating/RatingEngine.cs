using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace ArdalisRating
{
  /// <summary>
  /// The RatingEngine reads the policy application details from a file and produces a numeric 
  /// rating value based on the details.
  /// </summary>
  public class RatingEngine
  {
    public ConsoleLogger Logger { get; set; } = new ConsoleLogger();
    public FilePolicySource PolicySource { get; set; } = new FilePolicySource();
    public FilePolicySerializer PolicySerializer { get; set; } = new FilePolicySerializer();
    public LifePolicyRater PolicyHolder { get; set; } 
    public decimal Rating { get; set; }
    public void Rate()
    {
      Logger.Log("Starting rate.");

      Logger.Log("Loading policy.");

      // load policy - open file policy.json
      string policyJson = PolicySource.GetPolicyFromSource();
      var policy = PolicySerializer.GetPolicyFromJsonString(policyJson);

      switch (policy.Type)
      {
        case PolicyType.Auto:
          Logger.Log("Rating AUTO policy...");
          Logger.Log("Validating policy.");
          if (String.IsNullOrEmpty(policy.Make))
          {
            Logger.Log("Auto policy must specify Make");
            return;
          }
          if (policy.Make == "BMW")
          {
            if (policy.Deductible < 500)
            {
              Rating = 1000m;
            }
            Rating = 900m;
          }
          break;

        case PolicyType.Land:
          Logger.Log("Rating LAND policy...");
          Logger.Log("Validating policy.");
          if (policy.BondAmount == 0 || policy.Valuation == 0)
          {
            Logger.Log("Land policy must specify Bond Amount and Valuation.");
            return;
          }
          if (policy.BondAmount < 0.8m * policy.Valuation)
          {
            Logger.Log("Insufficient bond amount.");
            return;
          }
          Rating = policy.BondAmount * 0.05m;
          break;

        case PolicyType.Life:
          var lifeRater = new LifePolicyRater(this, this.Logger);
          lifeRater.Rate(policy);
          break;

        default:
          Logger.Log("Unknown policy type");
          break;
      }

      Logger.Log("Rating completed.");
    }
  }
}
