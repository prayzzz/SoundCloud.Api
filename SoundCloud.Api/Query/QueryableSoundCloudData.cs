using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharpSound.SoundCloud.Query
{
    public class QueryableSoundCloudData<T> : IOrderedQueryable<T>
    {
        /// <summary> 
        /// This constructor is called by the client to create the data source. 
        /// </summary> 
        public QueryableSoundCloudData()
        {
            this.Provider = new SoundCloudQueryProvider();
            this.Expression = Expression.Constant(this);
        }

        /// <summary> 
        /// This constructor is called by Provider.CreateQuery(). 
        /// </summary> 
        /// <param name="expression"></param>
        public QueryableSoundCloudData(SoundCloudQueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            this.Provider = provider;
            this.Expression = expression;
        }

        /// <summary>
        /// Gibt einen Enumerator zurück, der die Auflistung durchläuft.
        /// </summary>
        /// <returns>
        /// Ein <see cref="T:System.Collections.Generic.IEnumerator`1"/>, der zum Durchlaufen der Auflistung verwendet werden kann.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return (this.Provider.Execute<IEnumerable<T>>(this.Expression)).GetEnumerator();
        }

        /// <summary>
        /// Gibt einen Enumerator zurück, der eine Auflistung durchläuft.
        /// </summary>
        /// <returns>
        /// Ein <see cref="T:System.Collections.IEnumerator"/>-Objekt, das zum Durchlaufen der Auflistung verwendet werden kann.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.Provider.Execute<IEnumerable>(this.Expression)).GetEnumerator();
        }

        /// <summary>
        /// Ruft die Ausdrucksbaumstruktur ab, die mit der Instanz von <see cref="T:System.Linq.IQueryable"/> verknüpft ist.
        /// </summary>
        /// <returns>
        /// Der <see cref="T:System.Linq.Expressions.Expression"/>, der mit dieser Instanz von <see cref="T:System.Linq.IQueryable"/> verknüpft ist.
        /// </returns>
        public Expression Expression { get; private set; }

        /// <summary>
        /// Ruft den Typ der Elemente ab, die zurückgegeben werden, wenn die Ausdrucksbaumstruktur ausgeführt wird, die mit dieser Instanz von <see cref="T:System.Linq.IQueryable"/> verknüpft ist.
        /// </summary>
        /// <returns>
        /// Ein <see cref="T:System.Type"/>, der den Typ der Elemente darstellt, die zurückgegeben werden, wenn die Ausdrucksbaumstruktur ausgeführt wird, die mit diesem Objekt verknüpft ist.
        /// </returns>
        public Type ElementType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// Ruft den Abfrageanbieter ab, der dieser Datenquelle zugeordnet ist.
        /// </summary>
        /// <returns>
        /// Der <see cref="T:System.Linq.IQueryProvider"/>, der dieser Datenquelle zugeordnet ist.
        /// </returns>
        public IQueryProvider Provider { get; private set; }
    }
}