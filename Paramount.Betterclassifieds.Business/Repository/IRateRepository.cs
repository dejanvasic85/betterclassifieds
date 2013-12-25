namespace BetterClassified.Repository
{
    using System;
    using Models;

    public interface IRateRepository
    {
        RateModel GetRatecard(int rateId);
    }
}