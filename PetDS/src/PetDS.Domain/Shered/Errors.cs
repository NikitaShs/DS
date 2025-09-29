using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Shered
{
    public class Errors : IEnumerable<Error>
    {
        private readonly List<Error> _errors;

        public Errors(List<Error> errors)
        {
            _errors = [..errors];
        }


        public static implicit operator Errors(List<Error> errors) => new(errors);

        public static implicit operator Errors(Error errors) => new([errors]);

        public IEnumerator<Error> GetEnumerator() => _errors.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
