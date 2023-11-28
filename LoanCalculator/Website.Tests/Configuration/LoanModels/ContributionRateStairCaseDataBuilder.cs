using System;
using System.Collections.Generic;
using Website.Configuration.LoanModels;

namespace Website.Tests.Configuration.LoanModels
{
    public class ContributionRateStairCaseDataBuilder
    {
        private int _id;
        private List<ContributionRateStairCaseStepData> _steps = new List<ContributionRateStairCaseStepData>();

        public ContributionRateStairCaseDataBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ContributionRateStairCaseDataBuilder WithStep(Action<ContributionRateStairCaseStepDataBuilder> builderFunc)
        {
            var builder = new ContributionRateStairCaseStepDataBuilder();
            builderFunc(builder);
            _steps.Add(builder.Build());
            return this;
        }

        public ContributionRateStairCaseData Build()
        {
            return new ContributionRateStairCaseData
            {
                Id = _id,
                Steps = _steps
            };
        }
    }
}