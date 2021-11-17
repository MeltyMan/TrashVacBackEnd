using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace TrashVac.Entity
{
    public class ClaimTypeCollection
    {
        public ClaimTypeCollection()
        {
            ClaimTypes = new List<ClaimTypeValuePair>();
        }
        public IList<ClaimTypeValuePair> ClaimTypes { get; set; }

        public T GetValue<T>(string key)
        {
            var index = -1;
            var valueString = string.Empty;

            for (var i = 0; i < ClaimTypes.Count; i++)
            {
                if (ClaimTypes[i].Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    valueString = ClaimTypes[index].Value;
                    
                    break;
                }
            }

            if (index >= 0)
            {
                var t = typeof(T).Name.ToLower();
                Enums.UserLevel minUserLevel = Enums.UserLevel.Undefined;
                switch (t)
                {
                    case "boolean":
                        return (T)(object)Convert.ToBoolean(valueString);
                        break;
                    case "userlevel":

                        if (Enum.TryParse(valueString, true, out minUserLevel))
                        {
                            return (T)(object)minUserLevel;
                        }
                        else
                        {
                            return default(T);
                        }
                        
                }

            }

            return default(T);
        }

    }
}
