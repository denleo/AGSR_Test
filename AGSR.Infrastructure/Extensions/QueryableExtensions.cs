using System.Globalization;
using System.Linq.Expressions;
using AGSR.Domain.Entities;

namespace AGSR.Infrastructure.Extensions;

public static class QueryableExtensions
{
    private static readonly string[] Formats = {
        "yyyy",
        "yyyy-MM",
        "yyyy-MM-dd",
        "yyyy-MM-ddTHH",
        "yyyy-MM-ddTHH:mm",
        "yyyy-MM-ddTHH:mm:ss"
    };

    /// <summary>
    /// Provide filter by date ranges using FHIR date search standard 
    /// </summary>
    /// <param name="queryable">Source query</param>
    /// <param name="selector">Field selector</param>
    /// <param name="filter">FHIR filter string param</param>
    /// <typeparam name="TEntity">Base entity type</typeparam>
    /// <see>
    ///     <cref>https://www.hl7.org/fhir/search.html#date</cref>
    /// </see>
    /// <returns>Source query</returns>
    public static IQueryable<TEntity> ApplyFhirDateFilter<TEntity>(
        this IQueryable<TEntity> queryable,
        Expression<Func<TEntity, DateTime>> selector,
        string filter) where TEntity: BaseEntity
    {
        var prefix = filter.AsSpan(0, 2);
        var value = filter.AsSpan(2);

        if (!TryParseRange(value, out var range))
        {
            return queryable;
        }
        
        Expression compareExpression = prefix.ToString().ToLower() switch
        {
            "eq" => Expression.And(
                Expression.GreaterThanOrEqual(selector.Body, Expression.Constant(range.Low)),
                Expression.LessThanOrEqual(selector.Body, Expression.Constant(range.High))),
            "ne" => Expression.Or(
                Expression.LessThan(selector.Body, Expression.Constant(range.Low)),
                Expression.GreaterThan(selector.Body, Expression.Constant(range.High))),
            "gt" => Expression.GreaterThan(selector.Body, Expression.Constant(range.High)),
            "lt" => Expression.LessThan(selector.Body, Expression.Constant(range.Low)),
            "ge" => Expression.GreaterThanOrEqual(selector.Body, Expression.Constant(range.Low)),
            "le" => Expression.LessThanOrEqual(selector.Body, Expression.Constant(range.High)),
            "sa" => Expression.GreaterThan(selector.Body, Expression.Constant(range.High)),
            "eb" => Expression.LessThan(selector.Body, Expression.Constant(range.Low)),
            "ap" => GetApproximateExpression(selector.Body, range),
            _ => Expression.Constant(true)
        };
        
        var lambda = Expression.Lambda<Func<TEntity, bool>>(compareExpression, selector.Parameters[0]);

        return queryable.Where(lambda);
    }

    private static BinaryExpression GetApproximateExpression(Expression selector, (DateTime Low, DateTime High) range)
    {
        var delta = range.Low == range.High
            ? DateTime.UtcNow - range.Low
            : range.High - range.Low;
        
        var approximation = (long)(Math.Abs(delta.Ticks) * 0.1);
        
        var low = range.Low.AddTicks(-approximation);
        var high = range.Low.AddTicks(approximation);

        return Expression.And(
            Expression.GreaterThanOrEqual(selector, Expression.Constant(low)),
            Expression.LessThanOrEqual(selector, Expression.Constant(high))
        );
    }
    
    private static bool TryParseRange(ReadOnlySpan<char> input, out (DateTime Low, DateTime High) range)
    {
        if (!DateTime.TryParseExact(input, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            range = (default, default);
            return false;
        }

        parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
        
        switch (input.Length)
        {
            case 4: // yyyy
                range.Low = new DateTime(parsedDate.Year, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                range.High = range.Low.AddYears(1).AddSeconds(-1);
                break;
            case 7: // yyyy-MM
                range.Low = new DateTime(parsedDate.Year, parsedDate.Month, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                range.High = range.Low.AddMonths(1).AddSeconds(-1);
                break;
            case 10: // yyyy-MM-dd
                range.Low = parsedDate;
                range.High = parsedDate.AddDays(1).AddSeconds(-1);
                break;
            case > 10: // yyyy-MM-ddTHH:mm:ss
                range = (parsedDate, parsedDate);
                break;
            default:
                range = (default, default);
                return false;
        }
        
        return true;
    }
}