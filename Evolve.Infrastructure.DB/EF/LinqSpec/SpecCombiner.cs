using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.LinqSpec
{
    static class SpecCombiner
    {
        /// <summary>
        /// Safely combines two specs with AND operator, writes the result into <see cref="spec1"/>.
        /// </summary>
        /// <typeparam name="T">Spec generic type.</typeparam>
        /// <param name="spec1">First spec.</param>
        /// <param name="spec2">Second spec.</param>
        public static void AndSpecs<T>(ref Specification<T> spec1, Specification<T> spec2)
        {
            spec1 = spec1 == null ? spec2 : spec1 && spec2;
        }

        /// <summary>
        /// Safely combines two specs with OR operator, writes the result into <see cref="spec1"/>.
        /// </summary>
        /// <typeparam name="T">Spec generic type.</typeparam>
        /// <param name="spec1">First spec.</param>
        /// <param name="spec2">Second spec.</param>
        public static void OrSpecs<T>(ref Specification<T> spec1, Specification<T> spec2)
        {
            spec1 = spec1 == null ? spec2 : spec1 || spec2;
        }
    }
}
