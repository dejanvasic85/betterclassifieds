namespace Paramount.Betterclassifieds.Business
{
    public interface IBusinessRule<in TTarget, TResult>
    {
        RuleResult<TResult> IsSatisfiedBy(TTarget target);
    }

    public class RuleResult<TResult>
    {
        public bool IsSatisfied { get; set; }
        public TResult Result { get; set; }
    }
}
