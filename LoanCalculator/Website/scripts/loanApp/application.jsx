var LoanApplication = React.createClass({
    getInitialState: function () {
        return {
            information: {
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
    componentWillMount: function () {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "/Home/LoadData", true);
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.onload = function () {
            var receivedData = JSON.parse(xhr.responseText);
            this.setState({ configuration: receivedData });
        }.bind(this);
        xhr.send();
    },
    setSpecificationCalculation: function (loanInformation, loanSpecification) {
        if (loanInformation === undefined || loanSpecification === undefined || !loanInformation.hasAllInformationBeenGiven)
            return;

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
                amount: loanInformation.amount,
                value: loanInformation.value,
                numberOfLoans: loanInformation.numberOfLoans
            },
            specification: specification
        });

        var xhr = new XMLHttpRequest();
        xhr.open("post", "/Home/CalculateSpecification", true);
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.onload = function () {
            var receivedData = JSON.parse(xhr.responseText);
            this.setState({ specificationCalculation: receivedData });
        }.bind(this);
        xhr.send(sendData);
    },
    handleInformationChange: function (loanInformation) {
        this.setState({ information: loanInformation });

        this.setSpecificationCalculation(loanInformation, this.state.specification);
    },
    handleSpecificationChange: function (loanSpecification) {
        this.setState({ specification: loanSpecification });

        this.setSpecificationCalculation(this.state.information, loanSpecification);
    },
    render: function () {
        return (
            <div>
                <ResidenceInformation loanConfiguration={this.state.configuration} loanInformation={this.state.information} onInformationChange={this.handleInformationChange} />
                <OwnPaymentInformation specificationCalculation={this.state.specificationCalculation} />
                <BankLoanInformation specificationCalculation={this.state.specificationCalculation} />
                <LoanInput loanConfiguration={this.state.configuration} loanInformation={this.state.information} loanSpecification={this.state.specification} onSpecificationChange={this.handleSpecificationChange} />
                <LoanResultsOverview specificationCalculation={this.state.specificationCalculation} />
                <LoanDisclaimer />
            </div>
        );
    }
});