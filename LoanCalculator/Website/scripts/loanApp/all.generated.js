﻿// @hash v3-A7FC1B3A7671D2054168E8BDB6AA29D8F8E6F364
// Automatically generated by ReactJS.NET. Do not edit, your changes will be overridden.
// Version: 2.2.1 (build 5682fd4) with Babel 6.3.13
// Generated at: 17-07-2016 19:44:00
///////////////////////////////////////////////////////////////////////////////
var LoanHeader = React.createClass({
    displayName: "LoanHeader",

    render: function render() {
        return React.createElement(
            "h1",
            { className: "page-header" },
            "BeregnBoliglån.dk"
        );
    }
});

var LoanInformation = React.createClass({
    displayName: "LoanInformation",

    hasAllInformationBeenGiven: function hasAllInformationBeenGiven(loanInformation) {
        return loanInformation.company && loanInformation.amount && loanInformation.value && loanInformation.numberOfLoans && loanInformation.value >= loanInformation.amount;
    },
    onInformationChange: function onInformationChange(e) {
        e.preventDefault();
        var loanInformation = {
            company: React.findDOMNode(this.refs.companySelection).options[this.refs.companySelection.getDOMNode().selectedIndex].disabled ? undefined : React.findDOMNode(this.refs.companySelection).options[this.refs.companySelection.getDOMNode().selectedIndex].value,
            amount: parseInt(React.findDOMNode(this.refs.loanAmount).value),
            value: parseInt(React.findDOMNode(this.refs.valueOfHouse).value),
            numberOfLoans: React.findDOMNode(this.refs.numberOfLoans).options[this.refs.numberOfLoans.getDOMNode().selectedIndex].value,
            hasAllInformationBeenGiven: false
        };
        loanInformation.hasAllInformationBeenGiven = this.hasAllInformationBeenGiven(loanInformation);

        this.props.onInformationChange(loanInformation);
    },
    render: function render() {
        var companySelectionOptions = [];
        this.props.loanConfiguration.companies.forEach(function (company) {
            companySelectionOptions.push(React.createElement(
                "option",
                { value: company.id },
                company.name
            ));
        });

        return React.createElement(
            "div",
            { className: "row", id: "loanInformation" },
            React.createElement(
                "div",
                { className: "col-lg-8 col-md-10 col-md-offset-1 information-box" },
                React.createElement(
                    "div",
                    { className: "row" },
                    React.createElement(
                        "div",
                        { className: "col-md-offset-1 col-sm-11" },
                        React.createElement(
                            "h4",
                            null,
                            "Informationer om lån"
                        )
                    )
                ),
                React.createElement(
                    "div",
                    { className: "row" },
                    React.createElement(
                        "div",
                        { className: "form-group col-sm-4" },
                        React.createElement(
                            "label",
                            { "for": "companySelection" },
                            "Selskab"
                        ),
                        React.createElement(
                            "select",
                            { id: "companySelection", onChange: this.onInformationChange, className: "form-control", ref: "companySelection" },
                            React.createElement(
                                "option",
                                { disabled: true, selected: true },
                                " -- Vælg selskab -- "
                            ),
                            companySelectionOptions
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "form-group col-sm-3" },
                        React.createElement(
                            "label",
                            { "for": "loanAmount" },
                            "Lånebeløb"
                        ),
                        React.createElement(
                            "div",
                            { className: "input-group" },
                            React.createElement("input", { type: "number",
                                min: "100000",
                                step: "50000",
                                onChange: this.onInformationChange,
                                className: "form-control text-right",
                                id: "loanAmount",
                                placeholder: "1.500.000",
                                ref: "loanAmount" }),
                            React.createElement(
                                "span",
                                { className: "input-group-addon" },
                                "kr."
                            )
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "form-group col-sm-3" },
                        React.createElement(
                            "label",
                            { "for": "valueOfHouse" },
                            "Værdi af bolig"
                        ),
                        React.createElement(
                            "div",
                            { className: "input-group" },
                            React.createElement("input", { type: "number", min: "100000", step: "50000", onChange: this.onInformationChange, className: "form-control text-right", id: "valueOfHouse", placeholder: "2.000.000", ref: "valueOfHouse" }),
                            React.createElement(
                                "span",
                                { className: "input-group-addon" },
                                "kr."
                            )
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "form-group col-sm-2" },
                        React.createElement(
                            "label",
                            { "for": "numberOfLoans" },
                            "Antal lån"
                        ),
                        React.createElement(
                            "select",
                            { id: "numberOfLoans", className: "form-control", onChange: this.onInformationChange, ref: "numberOfLoans" },
                            React.createElement(
                                "option",
                                null,
                                "1"
                            ),
                            React.createElement(
                                "option",
                                null,
                                "2"
                            ),
                            React.createElement(
                                "option",
                                null,
                                "3"
                            )
                        )
                    )
                )
            )
        );
    }
});

var LoanInput = React.createClass({
    displayName: "LoanInput",

    numberToStringConversion: function numberToStringConversion(number) {
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
    calculateLoanSplitPercentage: function calculateLoanSplitPercentage(loanNumber, numberOfLoans) {
        var splitPercentage = 100 / numberOfLoans;
        if (loanNumber != numberOfLoans) {
            return Math.floor(splitPercentage);
        } else {
            var splitPercentageDecimals = splitPercentage % 1;
            return Math.round(splitPercentage + (numberOfLoans - 1) * splitPercentageDecimals);
        }
    },
    componentWillUpdate: function componentWillUpdate(nextProps, nextState) {
        var nextNumberOfLoans = nextProps.loanInformation.numberOfLoans;
        var previousNumberOfLoans = this.props.loanSpecification.length;

        // Do nothing if the same number of loans are selected and same company
        if (nextNumberOfLoans == previousNumberOfLoans && nextProps.loanInformation.company == this.props.loanInformation.company) return;
        if (nextProps.loanInformation.company == undefined) return;

        var selectedCompany = nextProps.loanInformation.company;
        var selectedCompanyDefaultSettings = undefined;
        for (var index = 0; index < this.props.loanConfiguration.defaultSettings.length; index++) {
            var companyDefaultSettings = this.props.loanConfiguration.defaultSettings[index];
            if (selectedCompany == companyDefaultSettings.companyId) selectedCompanyDefaultSettings = companyDefaultSettings;
        }

        var specification = [];
        for (var loanIndex = 0; loanIndex < nextNumberOfLoans; loanIndex++) {
            var type = selectedCompanyDefaultSettings.settings[loanIndex].loanTypeId;
            var period = selectedCompanyDefaultSettings.settings[loanIndex].loanPeriodId;
            var interestRate = selectedCompanyDefaultSettings.settings[loanIndex].loanInterestRateId;

            // If the specification for the given loan index existed in the previous state,
            // then reuse the information from that specification, so that the user does not
            // need to reinput this information
            if (this.props.loanSpecification.length > loanIndex) {
                type = this.props.loanSpecification[0].type;
                period = this.props.loanSpecification[0].period;
                interestRate = this.props.loanSpecification[0].interestRate;
            }

            specification.push({
                percentage: this.calculateLoanSplitPercentage(loanIndex + 1, nextNumberOfLoans),
                type: type,
                period: period,
                interestRate: interestRate
            });
        }

        this.props.onSpecificationChange(specification);
    },
    correctPercentagesToATotalOfOneHundred: function correctPercentagesToATotalOfOneHundred(oldSpecification, newSpecification) {
        var percentageSum = 0;
        newSpecification.forEach(function (spec) {
            percentageSum += spec.percentage;
        });

        if (percentageSum === 100) return;

        var diffPercentage = 100 - percentageSum;
        if (newSpecification[newSpecification.length - 1].percentage == oldSpecification[oldSpecification.length - 1].percentage) {
            newSpecification[newSpecification.length - 1].percentage += diffPercentage;
        } else {
            newSpecification[0].percentage += diffPercentage;
        }
    },
    onSpecificationChange: function onSpecificationChange(e) {
        e.preventDefault();
        var loanSpecifications = [];
        for (var loanNumber = 1; loanNumber < this.props.loanSpecification.length + 1; loanNumber++) {
            var loanSpecification = {
                percentage: React.findDOMNode(this.refs["loanPercentage" + loanNumber]).valueAsNumber,
                type: React.findDOMNode(this.refs["loanType" + loanNumber]).options[this.refs["loanType" + loanNumber].getDOMNode().selectedIndex].value,
                period: parseInt(React.findDOMNode(this.refs["loanPeriod" + loanNumber]).options[this.refs["loanPeriod" + loanNumber].getDOMNode().selectedIndex].value),
                interestRate: parseFloat(React.findDOMNode(this.refs["loanInterestRate" + loanNumber]).options[this.refs["loanInterestRate" + loanNumber].getDOMNode().selectedIndex].value)
            };
            loanSpecifications.push(loanSpecification);
        }

        this.correctPercentagesToATotalOfOneHundred(this.props.loanSpecification, loanSpecifications);
        this.props.onSpecificationChange(loanSpecifications);
    },
    render: function render() {
        var selectedCompanyConfiguration = undefined;
        for (var index = 0; index < this.props.loanConfiguration.companies.length; index++) {
            if (this.props.loanConfiguration.companies[index].id == this.props.loanInformation.company) selectedCompanyConfiguration = this.props.loanConfiguration.companies[index];
        }
        var loanTypeSelectionOptions = [];
        if (selectedCompanyConfiguration != undefined) {
            selectedCompanyConfiguration.loanTypes.forEach(function (loanType) {
                loanTypeSelectionOptions.push(React.createElement(
                    "option",
                    { value: loanType.id },
                    loanType.name
                ));
            });
        }

        var loanNodes = [];
        var numberOfLoans = this.props.loanSpecification.length;
        for (var loanNumber = 1; loanNumber < numberOfLoans + 1; loanNumber++) {
            var loan = this.props.loanSpecification[loanNumber - 1];

            var selectedLoanType = undefined;
            if (selectedCompanyConfiguration != undefined) {
                for (var index = 0; index < selectedCompanyConfiguration.loanTypes.length; index++) {
                    if (selectedCompanyConfiguration.loanTypes[index].id == loan.type) selectedLoanType = selectedCompanyConfiguration.loanTypes[index];
                }
            }
            var loanPeriodSelectionOptions = [];
            if (selectedLoanType != undefined) {
                selectedLoanType.periods.forEach(function (loanPeriod) {
                    loanPeriodSelectionOptions.push(React.createElement(
                        "option",
                        { value: loanPeriod.id },
                        loanPeriod.period
                    ));
                });
            }

            var selectedLoanPeriod = undefined;
            if (selectedLoanType != undefined) {
                for (var index = 0; index < selectedLoanType.periods.length; index++) {
                    if (selectedLoanType.periods[index].id == loan.period) selectedLoanPeriod = selectedLoanType.periods[index];
                }
            }
            var loanInterestRateSelectionOptions = [];
            if (selectedLoanPeriod != undefined) {
                selectedLoanPeriod.interestRate.forEach(function (interestRate) {
                    loanInterestRateSelectionOptions.push(React.createElement(
                        "option",
                        { value: interestRate.id },
                        interestRate.interestRate,
                        "%"
                    ));
                });
            }

            loanNodes = loanNodes.concat(React.createElement(
                "div",
                { className: "row", id: "loan" + loanNumber },
                React.createElement(
                    "div",
                    { className: "col-lg-8 col-md-10 col-md-offset-1 information-box " + (this.props.loanInformation.hasAllInformationBeenGiven ? "" : "information-box-disabled") },
                    React.createElement(
                        "div",
                        { className: "row" },
                        React.createElement(
                            "div",
                            { className: "col-md-offset-1 col-sm-11" },
                            React.createElement(
                                "h4",
                                null,
                                this.numberToStringConversion(loanNumber),
                                " lån"
                            )
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "row" },
                        React.createElement(
                            "div",
                            { className: "form-group col-sm-2 col-xs-5" },
                            React.createElement(
                                "label",
                                { "for": "loanPercentage" + loanNumber },
                                "Procentdel"
                            ),
                            React.createElement(
                                "div",
                                { className: "input-group" },
                                React.createElement("input", { type: "number",
                                    value: loan.percentage,
                                    min: "0",
                                    max: "99",
                                    step: "1",
                                    className: "form-control text-right",
                                    id: "loanPercentage" + loanNumber,
                                    placeholder: "0 - 100",
                                    disabled: this.props.loanInformation.hasAllInformationBeenGiven ? false : "disabled",
                                    onChange: this.onSpecificationChange,
                                    ref: "loanPercentage" + loanNumber }),
                                React.createElement(
                                    "span",
                                    { className: "input-group-addon" },
                                    "%"
                                )
                            )
                        ),
                        React.createElement(
                            "div",
                            { className: "form-group col-sm-4 col-xs-7" },
                            React.createElement(
                                "label",
                                { "for": "loanType" + loanNumber },
                                "Lånetype"
                            ),
                            React.createElement(
                                "select",
                                { type: "text",
                                    className: "form-control",
                                    value: loan.type,
                                    id: "loanType" + loanNumber,
                                    disabled: this.props.loanInformation.hasAllInformationBeenGiven ? false : "disabled",
                                    onChange: this.onSpecificationChange,
                                    ref: "loanType" + loanNumber },
                                loanTypeSelectionOptions
                            )
                        ),
                        React.createElement(
                            "div",
                            { className: "form-group col-sm-3 col-xs-5" },
                            React.createElement(
                                "label",
                                { "for": "loanPeriod" + loanNumber },
                                "Låneperiode"
                            ),
                            React.createElement(
                                "select",
                                { type: "text",
                                    className: "form-control",
                                    value: loan.period,
                                    id: "loanPeriod" + loanNumber,
                                    disabled: this.props.loanInformation.hasAllInformationBeenGiven ? false : "disabled",
                                    onChange: this.onSpecificationChange,
                                    ref: "loanPeriod" + loanNumber },
                                loanPeriodSelectionOptions
                            )
                        ),
                        React.createElement(
                            "div",
                            { className: "form-group col-sm-3 col-xs-7" },
                            React.createElement(
                                "label",
                                { "for": "loanInterestRate" + loanNumber },
                                "Rente"
                            ),
                            React.createElement(
                                "select",
                                { id: "loanInterestRate" + loanNumber,
                                    className: "form-control",
                                    value: loan.interestRate,
                                    disabled: this.props.loanInformation.hasAllInformationBeenGiven ? false : "disabled",
                                    onChange: this.onSpecificationChange,
                                    ref: "loanInterestRate" + loanNumber },
                                loanInterestRateSelectionOptions
                            )
                        )
                    )
                )
            ));
        }

        return React.createElement(
            "div",
            null,
            loanNodes
        );
    }
});

var LoanResultsOverview = React.createClass({
    displayName: "LoanResultsOverview",

    render: function render() {
        return React.createElement(
            "div",
            { className: "row", id: "loanOverviewResults" },
            React.createElement(
                "div",
                { className: "col-lg-8 col-md-10 col-md-offset-1 information-box " + (this.props.specificationCalculation === undefined ? "information-box-disabled" : "") },
                React.createElement(
                    "div",
                    { className: "row" },
                    React.createElement(
                        "div",
                        { className: "col-md-offset-1 col-md-11" },
                        React.createElement(
                            "h4",
                            null,
                            "Oversigtsresultat"
                        )
                    )
                ),
                React.createElement(
                    "div",
                    { className: "row" },
                    React.createElement(
                        "div",
                        { className: "col-sm-3 col-xs-6 text-center" },
                        React.createElement(
                            "label",
                            null,
                            "Ydelse 1.år"
                        ),
                        React.createElement(
                            "p",
                            null,
                            this.props.specificationCalculation === undefined ? "0" : this.props.specificationCalculation.overview.firstYearPayment.toLocaleString(),
                            " kr."
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "col-sm-3 col-xs-6 text-center" },
                        React.createElement(
                            "label",
                            null,
                            "Afdrag 1.år"
                        ),
                        React.createElement(
                            "p",
                            null,
                            this.props.specificationCalculation === undefined ? "0" : this.props.specificationCalculation.overview.firstYearRepayment.toLocaleString(),
                            " kr."
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "col-sm-3 col-xs-6 text-center" },
                        React.createElement(
                            "label",
                            null,
                            "Rente 1.år"
                        ),
                        React.createElement(
                            "p",
                            null,
                            this.props.specificationCalculation === undefined ? "0" : this.props.specificationCalculation.overview.firstYearInterest.toLocaleString(),
                            " kr."
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "col-sm-3 col-xs-6 text-center" },
                        React.createElement(
                            "label",
                            null,
                            "Bidrag 1.år"
                        ),
                        React.createElement(
                            "p",
                            null,
                            this.props.specificationCalculation === undefined ? "0" : this.props.specificationCalculation.overview.firstYearContribution.toLocaleString(),
                            " kr."
                        )
                    )
                )
            )
        );
    }
});

var LoanResultsDetailed = React.createClass({
    displayName: "LoanResultsDetailed",

    render: function render() {
        var detailedResultRows = [];
        if (this.props.specificationCalculation !== undefined) {
            this.props.specificationCalculation.paymentPlan.forEach(function (paymentYear) {
                detailedResultRows.push(React.createElement(
                    "tr",
                    null,
                    React.createElement(
                        "td",
                        null,
                        paymentYear.year
                    ),
                    React.createElement(
                        "td",
                        null,
                        paymentYear.payment.toLocaleString(),
                        " kr."
                    ),
                    React.createElement(
                        "td",
                        null,
                        paymentYear.repayment.toLocaleString(),
                        " kr."
                    ),
                    React.createElement(
                        "td",
                        { className: "hidden-xs" },
                        paymentYear.interestRate.toLocaleString(),
                        " kr."
                    ),
                    React.createElement(
                        "td",
                        { className: "hidden-xs" },
                        paymentYear.contribution.toLocaleString(),
                        " kr."
                    ),
                    React.createElement(
                        "td",
                        { className: "visible-xs" },
                        (paymentYear.interestRate + paymentYear.contribution).toLocaleString(),
                        " kr."
                    ),
                    React.createElement(
                        "td",
                        null,
                        paymentYear.loanLeft.toLocaleString(),
                        " kr."
                    )
                ));
            });
        }

        return React.createElement(
            "div",
            { className: "row", id: "loanDetailedResults" },
            React.createElement(
                "div",
                { className: "col-lg-8 col-md-10 col-md-offset-1 information-box " + (this.props.specificationCalculation === undefined ? "information-box-disabled" : "") },
                React.createElement(
                    "div",
                    { className: "row" },
                    React.createElement(
                        "div",
                        { className: "col-md-offset-1 col-md-11" },
                        React.createElement(
                            "h4",
                            null,
                            "Detaljeret resultat"
                        )
                    )
                ),
                React.createElement(
                    "div",
                    { className: "row" },
                    React.createElement(
                        "div",
                        { className: "col-sm-12" },
                        React.createElement(
                            "table",
                            { className: "table table-striped table-condensed table-responsive" },
                            React.createElement(
                                "thead",
                                null,
                                React.createElement(
                                    "tr",
                                    null,
                                    React.createElement(
                                        "th",
                                        null,
                                        "År"
                                    ),
                                    React.createElement(
                                        "th",
                                        null,
                                        "Ydelse"
                                    ),
                                    React.createElement(
                                        "th",
                                        null,
                                        "Afdrag"
                                    ),
                                    React.createElement(
                                        "th",
                                        { className: "hidden-xs" },
                                        "Rente"
                                    ),
                                    React.createElement(
                                        "th",
                                        { className: "hidden-xs" },
                                        "Bidrag"
                                    ),
                                    React.createElement(
                                        "th",
                                        { className: "visible-xs" },
                                        "Rente+Bidrag"
                                    ),
                                    React.createElement(
                                        "th",
                                        null,
                                        "Restgæld"
                                    )
                                )
                            ),
                            React.createElement(
                                "tbody",
                                null,
                                detailedResultRows
                            )
                        )
                    )
                )
            )
        );
    }
});

var LoanApplication = React.createClass({
    displayName: "LoanApplication",

    getInitialState: function getInitialState() {
        return {
            information: {
                company: undefined,
                amount: undefined,
                value: undefined,
                numberOfLoans: 1,
                hasAllInformationBeenGiven: false
            },
            specification: [],
            configuration: {
                defaultSettings: [],
                companies: []
            }
        };
    },
    componentWillMount: function componentWillMount() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "/Home/LoadData", true);
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.onload = (function () {
            var receivedData = JSON.parse(xhr.responseText);
            this.setState({ configuration: receivedData });
        }).bind(this);
        xhr.send();
    },
    setSpecificationCalculation: function setSpecificationCalculation(loanInformation, loanSpecification) {
        if (loanInformation === undefined || loanSpecification === undefined || !loanInformation.hasAllInformationBeenGiven) return;

        var specification = [];
        for (var index = 0; index < loanSpecification.length; index++) {
            specification.push({
                percentage: loanSpecification[index].percentage,
                type: loanSpecification[index].type,
                period: loanSpecification[index].period,
                interestRate: loanSpecification[index].interestRate
            });
        }

        var sendData = JSON.stringify({
            information: {
                company: loanInformation.company,
                amount: loanInformation.amount,
                value: loanInformation.value,
                numberOfLoans: loanInformation.numberOfLoans
            },
            specification: specification
        });

        var xhr = new XMLHttpRequest();
        xhr.open("post", "/Home/CalculateSpecification", true);
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.onload = (function () {
            var receivedData = JSON.parse(xhr.responseText);
            this.setState({ specificationCalculation: receivedData });
        }).bind(this);
        xhr.send(sendData);
    },
    handleInformationChange: function handleInformationChange(loanInformation) {
        this.setState({ information: loanInformation });

        this.setSpecificationCalculation(loanInformation, this.state.specification);
    },
    handleSpecificationChange: function handleSpecificationChange(loanSpecification) {
        this.setState({ specification: loanSpecification });

        this.setSpecificationCalculation(this.state.information, loanSpecification);
    },
    render: function render() {
        return React.createElement(
            "div",
            null,
            React.createElement(LoanHeader, null),
            React.createElement(LoanInformation, { loanConfiguration: this.state.configuration, loanInformation: this.state.information, onInformationChange: this.handleInformationChange }),
            React.createElement(LoanInput, { loanConfiguration: this.state.configuration, loanInformation: this.state.information, loanSpecification: this.state.specification, onSpecificationChange: this.handleSpecificationChange }),
            React.createElement(LoanResultsOverview, { specificationCalculation: this.state.specificationCalculation }),
            React.createElement(LoanResultsDetailed, { specificationCalculation: this.state.specificationCalculation })
        );
    }
});

ReactDOM.render(React.createElement(LoanApplication, null), document.getElementById("content"));