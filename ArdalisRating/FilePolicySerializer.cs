using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating
{
  public class FilePolicySerializer
  {
    public Policy GetPolicyFromJsonString(string json)
    {
      return JsonConvert.DeserializeObject<Policy>(json,
          new StringEnumConverter());
    }

  }
}
