var LoanInput = React.createClass({
    numberToStringConversion: function (number) {
        switch (number) {
            case 1:
                return "Første";
            case 2:
                return "Andet";
            case 3:
                return "Tredje";
            default:
                throw new Exception();
        }
    },
    calculateLoanSplitPercentage: function (loanNumber, numberOfLoans) {
        var splitPercentage = 100 / numberOfLoans;
        if (loanNumber != numberOfLoans) {
            return Math.floor(splitPercentage);
        } else {
            var splitPercentageDecimals = splitPercentage % 1;
            return Math.round(splitPercentage + ((numberOfLoans - 1) * splitPercentageDecimals));
        }
    },
    componentWillUpdate: function (nextProps, nextState) {
        var nextNumberOfLoans = nextProps.loanInformation.numberOfLoans;
        var previousNumberOfLoans = this.props.loanSpecification.length;

        // Do nothing if the same number of loans are selected
        if (nextNumberOfLoans == previousNumberOfLoans || !nextProps.loanInformation.hasAllInformationBeenGiven)
            return;

        var specification = [];
        for (var loanIndex = 0; loanIndex < nextNumberOfLoans; loanIndex++) {
            var defaultSettings = this.props.loanConfiguration.defaultSettings[loanIndex];
            var type = defaultSettings.productName;
            var period = defaultSettings.period;
            var interestRate = defaultSettings.interestRate;

            // If the specification for the given loan index existed in the previous state and
            // then reuse the information from that specification, so that the user does not
            // need to reinput this information
            if (this.props.loanSpecification.length > loanIndex) {
                var previousLoanSpecification = this.props.loanSpecification[loanIndex];
                type = previousLoanSpecification.type;
                period = previousLoanSpecification.period;
                interestRate = previousLoanSpecification.interestRate;
            }

            specification.push({
                percentage: this.calculateLoanSplitPercentage(loanIndex + 1, nextNumberOfLoans),
                type: type,
                period: period,
                interestRate: interestRate,
            });
        }

        this.props.onSpecificationChange(specification);
    },
    correctPercentagesToATotalOfOneHundred: function (oldSpecification, newSpecification) {
        var percentageSum = 0;
        newSpecification.forEach(function (spec) { percentageSum += spec.percentage });

        if (percentageSum > 100) {
            var specificationWithTooHighPercentage = undefined;
            var percentageForOkSpecifications = 0;
            for (var loanIndex = 0; loanIndex < newSpecification.length; loanIndex++) {
                if (newSpecification[loanIndex].percentage > 100)
                    specificationWithTooHighPercentage = newSpecification[loanIndex];
                else
                    percentageForOkSpecifications += newSpecification[loanIndex].percentage;
            }

            if (specificationWithTooHighPercentage !== undefined) {
                specificationWithTooHighPercentage.percentage = 100 - percentageForOkSpecifications;
                percentageSum = 100;
            }
        }

        if (percentageSum === 100)
            return;

        var diffPercentage = 100 - percentageSum;
        if (newSpecification[newSpecification.length - 1].percentage == oldSpecification[oldSpecification.length - 1].percentage) {
            newSpecification[newSpecification.length - 1].percentage += diffPercentage;
        } else {
            newSpecification[0].percentage += diffPercentage;
        }
    },
    onSpecificationChange: function (e) {
        e.preventDefault();

        // If the loan type changes, then the period and interest rate must be defaulted
        // If the period changed, then the interest rate must be defaulted
        // That should solve the problem with the bad input provided to the specification

        var loanSpecifications = [];
        for (var loanNumber = 1; loanNumber < this.props.loanSpecification.length + 1; loanNumber++) {
            var previousLoanSpecification = this.props.loanSpecification[loanNumber - 1];
            var interestRateNode = ReactDOM.findDOMNode(this.refs["loanInterestRate" + loanNumber]);
            var loanSpecification = {
                percentage: parseInt(ReactDOM.findDOMNode(this.refs["loanPercentage" + loanNumber]).value),
                type: ReactDOM.findDOMNode(this.refs["loanType" + loanNumber]).options[this.refs["loanType" + loanNumber].selectedIndex].value,
                period: parseInt(ReactDOM.findDOMNode(this.refs["loanPeriod" + loanNumber]).options[this.refs["loanPeriod" + loanNumber].selectedIndex].value),
                interestRate: interestRateNode.options.length > 0 ? parseFloat(interestRateNode.options[this.refs["loanInterestRate" + loanNumber].selectedIndex].value) : 0
            };

            if (isNaN(loanSpecification.percentage))
                loanSpecification.percentage = 1;

            // If the loan type has changed, then period and interest rate must be defaulted
            if (loanSpecification.type != previousLoanSpecification.type) {
                var selectedLoanType = undefined;
                for (var index = 0; index < this.props.loanConfiguration.products.length; index++) {
                    if (this.props.loanConfiguration.products[index].name == loanSpecification.type)
                        selectedLoanType = this.props.loanConfiguration.products[index];
                }

                // TODO: Consider adding default settings for all products, so that these can be set intelligently
                loanSpecification.period = selectedLoanType.periods[0].period;
                loanSpecification.interestRate = selectedLoanType.periods[0].interestRate[0].interestRate;
            }

            // If the period has changed, then interest rate must be defaulted
            if (loanSpecification.period != previousLoanSpecification.period) {
                var selectedLoanType = undefined;
                for (var index = 0; index < this.props.loanConfiguration.products.length; index++) {
                    if (this.props.loanConfiguration.products[index].name == loanSpecification.type)
                        selectedLoanType = this.props.loanConfiguration.products[index];
                }

                var selectedLoanPeriod = undefined;
                for (var index = 0; index < selectedLoanType.periods.length; index++) {
                    if (selectedLoanType.periods[index].period == loanSpecification.period)
                        selectedLoanPeriod = selectedLoanType.periods[index];
                }

                loanSpecification.interestRate = selectedLoanPeriod.interestRate[0].interestRate;
            }

            loanSpecifications.push(loanSpecification);
        }

        this.correctPercentagesToATotalOfOneHundred(this.props.loanSpecification, loanSpecifications);
        this.props.onSpecificationChange(loanSpecifications);
    },
    render: function () {
        if (this.props.loanConfiguration.products == undefined)
            return (<div />);

        var loanTypeSelectionOptions = [];
        this.props.loanConfiguration.products.forEach(function (loanType) {
            loanTypeSelectionOptions.push(<option key={loanType.name} value={loanType.name }>{loanType.name}</option>);
        });

        var loanNodes = [];
        var numberOfLoans = this.props.loanSpecification.length;
        for (var loanNumber = 1; loanNumber < numberOfLoans + 1; loanNumber++) {
            var loan = this.props.loanSpecification[loanNumber - 1];

            var selectedLoanType = undefined;
            for (var index = 0; index < this.props.loanConfiguration.products.length; index++) {
                if (this.props.loanConfiguration.products[index].name == loan.type)
                    selectedLoanType = this.props.loanConfiguration.products[index];
            }
            var loanPeriodSelectionOptions = [];
            if (selectedLoanType != undefined) {
                selectedLoanType.periods.forEach(function(loanPeriod) {
                    loanPeriodSelectionOptions.push(<option key={loanPeriod.period} value={loanPeriod.period }>{loanPeriod.period}</option>);
                });
            }

            var selectedLoanPeriod = undefined;
            if (selectedLoanType != undefined) {
                for (var index = 0; index < selectedLoanType.periods.length; index++) {
                    if (selectedLoanType.periods[index].period == loan.period)
                        selectedLoanPeriod = selectedLoanType.periods[index];
                }
            }
            var loanInterestRateSelectionOptions = [];
                if (selectedLoanPeriod != undefined) {
                    selectedLoanPeriod.interestRate
                        .filter(function(interestRate) {
                            return interestRate.showInterestRate;
                        })
                        .forEach(function (interestRate) {
                        loanInterestRateSelectionOptions.push(<option key={interestRate.interestRate} value={interestRate.interestRate }>{interestRate.interestRate}%</option>);
                    });
                    }

                    loanNodes = loanNodes.concat(
                        <div key={ loanNumber } className="row" id={"loan" + loanNumber }>
                            <div className={"col-lg-9 col-md-11 col-md-offset-1 information-box " + (this.props.loanInformation.hasAllInformationBeenGiven ? "" : "information-box-disabled") }>
                                <div className="row">
                                    <div className="col-md-12 information-box-header">
                                        <h4>{this.numberToStringConversion(loanNumber)} lån</h4>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="form-group col-sm-2 col-xs-5">
                                        <label htmlFor={"loanPercentage" + loanNumber }>Procentdel</label>
                                        <div className="input-group">
                                            <input type="number"
                                                   value={loan.percentage}
                                                   min="0"
                                                   max="100"
                                                   step="1"
                                                   className="form-control text-right"
                                                   id={"loanPercentage" + loanNumber}
                                                   placeholder="0 - 100"
                                                   disabled={ this.props.loanInformation.hasAllInformationBeenGiven ? false : "disabled"}
                                                   onChange={this.onSpecificationChange}
                                                   ref={"loanPercentage" + loanNumber} />
                                            <span className="input-group-addon">%</span>
                                        </div>
                                    </div>
                                    <div className="form-group col-sm-4 col-xs-7">
                                        <label htmlFor={"loanType" + loanNumber }>Lånetype</label>
                                        <select type="text"
                                                className="form-control"
                                                value={loan.type}
                                                id={"loanType" + loanNumber}
                                                disabled={ this.props.loanInformation.hasAllInformationBeenGiven ? false : "disabled"}
                                                onChange={this.onSpecificationChange}
                                                ref={"loanType" + loanNumber}>
                                            {loanTypeSelectionOptions}
                                        </select>
                                    </div>
                                    <div className="form-group col-sm-3 col-xs-5">
                                        <label htmlFor={"loanPeriod" + loanNumber }>Låneperiode</label>
                                        <select type="text"
                                                className="form-control"
                                                value={loan.period}
                                                id={"loanPeriod" + loanNumber}
                                                disabled={ this.props.loanInformation.hasAllInformationBeenGiven ? false : "disabled"}
                                                onChange={this.onSpecificationChange}
                                                ref={"loanPeriod" + loanNumber}>
                                            {loanPeriodSelectionOptions}
                                        </select>
                                    </div>
                                    <div className="form-group col-sm-3 col-xs-7">
                                        <label htmlFor={"loanInterestRate" + loanNumber }>Rente</label>
                                        <select id={"loanInterestRate" + loanNumber}
                                                className="form-control"
                                                value={ loan.interestRate }
                                                disabled={ this.props.loanInformation.hasAllInformationBeenGiven && loanInterestRateSelectionOptions.length > 0 ? false : "disabled"}
                                                onChange={this.onSpecificationChange}
                                                ref={"loanInterestRate" + loanNumber}>
                                            {loanInterestRateSelectionOptions}
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>);
        }

        return (
            <div>
                {loanNodes}
            </div>);
    }
});