using System;

namespace Paramount.Betterclassifieds.Business
{
    public interface IBusinessRule<in TTarget, TResult>
    {
        RuleResult<TResult> IsSatisfiedBy(TTarget request);
    }

    public interface IBusinessRule<in TTarget>
    {
        bool IsSatisfiedBy(TTarget target);
    }

    public class RuleResult<TResult>
    {
        public bool IsSatisfied { get; set; }
        public TResult Result { get; set; }

        public static implicit operator bool(RuleResult<TResult> result)
        {
            return result.IsSatisfied;
        }
    }

    public class AndRule<TTarget> : IBusinessRule<TTarget>
    {
        private readonly IBusinessRule<TTarget> _firstRule;
        private readonly IBusinessRule<TTarget> _secondRule;

        public AndRule(IBusinessRule<TTarget> firstRule, IBusinessRule<TTarget> secondRule)
        {
            _firstRule = firstRule;
            _secondRule = secondRule;
        }
        
        public bool IsSatisfiedBy(TTarget target)
        {
            return _firstRule.IsSatisfiedBy(target) && _secondRule.IsSatisfiedBy(target);
        }
    }

    public static class BusinessRuleExtensions
    {
        public static AndRule<TTarget> And<TTarget>(this IBusinessRule<TTarget> firstRule,
            IBusinessRule<TTarget> secondRule)
        {
            return new AndRule<TTarget>(firstRule, secondRule);
        }
    }
}
