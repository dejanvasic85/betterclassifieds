using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Base
{
    public sealed class ContextData<T> where T : class, new()
    {
        public ContextData()
        {
            this.Set(new T());
        }

        public T Get()
        {
            return ScenarioContext.Current[Key] as T;
        }

        public void Set(T data)
        {
            ScenarioContext.Current[Key] = data;
        }

        private string Key
        {
            get { return typeof (T).FullName; }
        }
    }
}