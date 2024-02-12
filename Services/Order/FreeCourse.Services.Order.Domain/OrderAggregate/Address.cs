using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Address : ValueObject
    {

        public string Province { get; private set; }
        public string Disctrict { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return Disctrict;
            yield return Street;
            yield return ZipCode;
            yield return Line;

        }


        public Address(string province, string disctrict, string street, string zipCode, string line)
        {
            Province = province;
            Disctrict = disctrict;
            Street = street;
            ZipCode = zipCode;
            Line = line;
        }
    }
}
