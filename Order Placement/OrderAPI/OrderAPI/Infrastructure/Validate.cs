using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Infrastructure
{
    public class Validate
    {
        public static List<Tuple<string,string>> ValidateObject<T>(T obj)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            List<Tuple<string, string>> tuples = new List<Tuple<string, string>>();

            ValidationContext validationContext = new ValidationContext(obj, null, null);

            Validator.TryValidateObject(obj, validationContext, results, true);

            foreach (var item in results)
            {
                tuples.Add(new Tuple<string, string>(item.MemberNames.FirstOrDefault(), item.ErrorMessage));
            }
            return tuples;
        }
    }
}
