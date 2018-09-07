using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.LinqSpec
{
    public static class Specification
    {
        /// <summary>
        /// Creates a custom ad-hoc <see cref="Specification"/> for the given <typeparamref name="T"/>.
        /// </summary>
        public static Specification<T> For<T>(Expression<Func<T, bool>> specification)
        {
            return specification;
        }

        /// <summary>
        /// Converts the given expression to a linq query specification. Typically 
        /// not needed as the expression can be converted implicitly to a linq 
        /// specification by just assigning it or passing it as such to another method.
        /// </summary>
        public static Specification<T> Spec<T>(this Expression<Func<T, bool>> specification)
        {
            return specification;
        }
    }

    /// <summary>
    /// Base class for query specifications that can be combined using logical And and Or 
    /// operators. For custom ad-hoc queries, use the static <see cref="Specification.For{T}"/> method.
    /// </summary>
    public class Specification<T>
    {
        /// <summary>
        /// Gets the expression that defines this query. Typically accessing 
        /// this property is not needed as the query spec can be converted 
        /// implicitly to an expression by just assigning it or passing it as 
        /// such to another method.
        /// </summary>
        public virtual Expression<Func<T, bool>> Expression { get; private set; }

        public Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// In case of overriding Expression.
        /// </summary>
        protected Specification()
        {
        }

        /// <summary>
        /// Allows to combine two query specifications using a logical And operation.
        /// </summary>
        public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
        {
            return new AndSpec(spec1, spec2);
        }

        public static bool operator false(Specification<T> spec1)
        {
            return false; // no-op. & and && do exactly the same thing.
        }

        public static bool operator true(Specification<T> spec1)
        {
            return false; // no - op. & and && do exactly the same thing.
        }

        /// <summary>
        /// Allows to combine two query specifications using a logical Or operation.
        /// </summary>
        public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
        {
            return new OrSpec(spec1, spec2);
        }

        /// <summary>
        /// Negates the given expression.
        /// </summary>
        public static Specification<T> operator !(Specification<T> spec1)
        {
            return new NegateSpec<T>(spec1);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Specification"/> to a linq expression.
        /// </summary>
        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
        {
            return spec.Expression;
        }

        /// <summary>
        /// Performs an implicit conversion from a linq expression to <see cref="Specification"/>.
        /// </summary>
        public static implicit operator Specification<T>(Expression<Func<T, bool>> expression)
        {
            return new AdHocSpec(expression);
        }

        /// <summary>
        /// The <c>And</c> specification.
        /// </summary>
        private class AndSpec : Specification<T>, IEquatable<AndSpec>
        {
            private readonly Expression<Func<T, bool>> expression;
            private readonly Specification<T> spec1;
            private readonly Specification<T> spec2;

            public AndSpec(Specification<T> spec1, Specification<T> spec2)
            {
                this.spec1 = spec1;
                this.spec2 = spec2;

                // combines the expressions without the need for Expression.Invoke which fails on EntityFramework
                expression = spec1.Expression.And(spec2.Expression);
            }

            public override Expression<Func<T, bool>> Expression { get { return expression; } }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;

                return Equals((AndSpec)obj);
            }

            public override int GetHashCode()
            {
                return spec1.GetHashCode() ^ spec2.GetHashCode();
            }

            public bool Equals(AndSpec other)
            {
                return spec1.Equals(other.spec1) &&
                       spec2.Equals(other.spec2);
            }
        }

        /// <summary>
        /// The <c>Or</c> specification.
        /// </summary>
        private class OrSpec : Specification<T>, IEquatable<OrSpec>
        {
            private readonly Expression<Func<T, bool>> expression;
            private readonly Specification<T> spec1;
            private readonly Specification<T> spec2;

            public OrSpec(Specification<T> spec1, Specification<T> spec2)
            {
                this.spec1 = spec1;
                this.spec2 = spec2;
                expression = spec1.Expression.Or(spec2.Expression);
            }

            public override Expression<Func<T, bool>> Expression { get { return expression; } }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;

                return Equals((OrSpec)obj);
            }

            public override int GetHashCode()
            {
                return spec1.GetHashCode() ^ spec2.GetHashCode();
            }

            public bool Equals(OrSpec other)
            {
                return spec1.Equals(other.spec1) &&
                       spec2.Equals(other.spec2);
            }

        }

        /// <summary>
        /// Negates the given query specification.
        /// </summary>
        private class NegateSpec<TArg> : Specification<TArg>, IEquatable<NegateSpec<TArg>>
        {
            private readonly Expression<Func<TArg, bool>> expression;
            private readonly Specification<TArg> spec;

            public NegateSpec(Specification<TArg> spec)
            {
                this.spec = spec;
                expression = spec.Expression;
                expression = System.Linq.Expressions.Expression.Lambda<Func<TArg, bool>>(
                    System.Linq.Expressions.Expression.Not(expression.Body), expression.Parameters);
            }

            public override Expression<Func<TArg, bool>> Expression { get { return expression; } }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;

                return Equals((Specification<T>.NegateSpec<TArg>)obj);
            }

            public override int GetHashCode()
            {
                return spec.GetHashCode();
            }

            public bool Equals(Specification<T>.NegateSpec<TArg> other)
            {
                return spec.Equals(other.spec);
            }
        }

        private class AdHocSpec : Specification<T>, IEquatable<AdHocSpec>
        {
            private readonly Expression<Func<T, bool>> specification;

            public AdHocSpec(Expression<Func<T, bool>> specification)
            {
                this.specification = specification;
            }

            public override Expression<Func<T, bool>> Expression { get { return specification; } }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;

                return Equals((AdHocSpec)obj);
            }

            public override int GetHashCode()
            {
                return specification.GetHashCode();
            }

            public bool Equals(AdHocSpec other)
            {
                return specification.Equals(other.specification);
            }
        }
    }
}
